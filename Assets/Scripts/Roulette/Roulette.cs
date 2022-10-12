using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour
{
    [SerializeField] private Transform _listTR;
    private RouletteSettings _rSettings;

    [Header("Roulette and Slices Settings")]
    private List<RouletteSlice> _rouletteSlices = new List<RouletteSlice>();
    private RouletteSlice _targetSlice;



    [Header("Spin Degree Settings")]
   [SerializeField] public float _startDegree = 0f;
    private float _currentDegree = 0f;
    private float _degreeRotated = 0f;
    private float _degreeBetweenEachSlice = 0f;
    public float _targetDegree = 0f;
    private float _targetRotationDegree = 0f;
    private float _currentRotationIncreaseValue = 0f;
    private bool _canRotate = false;
    private bool _canDecrease = false;
    private bool _canApproachTarget = false;

    

    [Header("Spin Chance Settings")]
    private float _totalChance;
    private const int CHANCEDIVIDER = 100; //We divide the chances by the given chance divider value
    private float _divisionRate; //if total chances don't add up to 1 or overflows 1 , we calculate the amount that rounds it to 1
    private float _targetChance;
    private float _chanceRateToAdd;

    private System.Action<RouletteSlice> OnReceiveSlice;

    

    public void Init(System.Action<RouletteSlice> OnReceiveSlice, RouletteSettings rSettings)

    {
        this.OnReceiveSlice = OnReceiveSlice;
        _rSettings = rSettings;

        Init();
        _degreeBetweenEachSlice = 360 / (_listTR.childCount);
        foreach (RouletteSlice slice in _listTR.GetComponentsInChildren<RouletteSlice>())
        {
            slice.Init();
            _rouletteSlices.Add(slice);
            slice.degree = _currentDegree;
            _currentDegree += _degreeBetweenEachSlice;
            _totalChance += slice.SpawnChance;
        }
        _divisionRate = _totalChance / CHANCEDIVIDER;
    }
    public void Init()
    {
        _currentRotationIncreaseValue = _rSettings._rotationIncreaseValue;
        transform.eulerAngles = new Vector3(0, 0, _startDegree);

    }
    public void SetStartPos(float startDegree)
    {
        _startDegree = startDegree;
    }   
    public float GetStartPos()
    {
        return _startDegree;
    }
    // Update is called once per frame
    void Update()
    {
        if (_canRotate)
        {
            transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, new Vector3(0, 0, _targetRotationDegree), _currentRotationIncreaseValue * Time.deltaTime);
            _degreeRotated += _currentRotationIncreaseValue * Time.deltaTime;

        }
        else if (_canApproachTarget)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, _targetDegree), _rSettings._rotationApproachRate);
        }
        
    }

    private void SelectTargetSlice()
    {
        var chanceRate = 0f;
        _targetChance = Random.Range(0f, 1f);
        for (int i = 0; i < _rouletteSlices.Count; i++)
        {
            _chanceRateToAdd = _rouletteSlices[i].SpawnChance / (CHANCEDIVIDER * _divisionRate);
            chanceRate += _chanceRateToAdd;
            if (_targetChance >= 0 && _targetChance <= chanceRate)
            {
                _targetSlice = _rouletteSlices[i];
                _targetDegree = _rouletteSlices[i].degree;
                break;
            }
        }
    }
    public IEnumerator Spin()
    {
        SelectTargetSlice();
        _targetRotationDegree =360 * _rSettings.totalRotationCount;

        _canRotate = true;
        while (_canRotate)
        {
            yield return new WaitUntil(() => _degreeRotated >= _targetRotationDegree);
            _degreeRotated = 0;
            break;
            
        }

        _canDecrease = true;
        
        int secondRotationAmount = Mathf.FloorToInt(_rSettings.totalRotationCount / _rSettings.divisionAmount);
        float decreaseDivisionAmount = 0;
        _targetRotationDegree =_targetDegree + 360 *secondRotationAmount -_startDegree ;
        decreaseDivisionAmount = _targetRotationDegree / 360;
        while (_canDecrease)
        {
            _currentRotationIncreaseValue = (_currentRotationIncreaseValue -_rSettings._targetRotationDecreaseValue*Time.deltaTime/decreaseDivisionAmount <= _rSettings._targetRotationValue) ? _rSettings._targetRotationValue :
            _currentRotationIncreaseValue - _rSettings._targetRotationDecreaseValue *Time.deltaTime / decreaseDivisionAmount;
            yield return new WaitForEndOfFrame();
            if (_degreeRotated >= _targetRotationDegree) break;
        }
        _degreeRotated = 0;
        _canRotate = false;
        _canApproachTarget = true;
        //_targetRotationDegree = (_targetDegree >= 180) ? (_targetDegree - 180) * (-1) : _targetDegree;
        yield return new WaitUntil(() => Mathf.FloorToInt(transform.eulerAngles.z) == _targetDegree);
        Debug.Log("burada");
        _canApproachTarget = false;
        
        _canRotate = true;
        _targetRotationDegree = _targetDegree + _rSettings._targetWiggleDegree;
        for (int j = 0; j < 1; j++)
        {
            Debug.Log("burada " + j);

            yield return new WaitUntil(() => (_degreeRotated>=_rSettings._targetWiggleDegree));
            _degreeRotated = 0;
        }
        
        _canRotate = false;
        _canApproachTarget = true;
        yield return new WaitUntil(() => Mathf.FloorToInt(transform.eulerAngles.z) == _targetDegree);
        _canApproachTarget = false;
        _startDegree = _targetDegree;
        OnReceiveSlice(_targetSlice);
    }
}
