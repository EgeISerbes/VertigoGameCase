using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour
{
    [SerializeField] private Transform _listTR;
    [SerializeField] private RouletteSettings _rSettings;

    [Header("Roulette and Slices Settings")]
    private List<RouletteSlice> _rouletteSlices = new List<RouletteSlice>();
    private RouletteSlice _targetSlice;

    [Header("Spin Seconds Settings")]
    private float _currentSpinForSeconds;
    private float _timer = 0f;


    [Header("Spin Degree Settings")]
    private float _currentDegree = 0f;
    private float _degreeBetweenEachSlice = 0f;
    private float _targetDegree = 0f;
    private float _currentRotationIncreaseValue = 0f;
    private bool _canRotate = false;
    private bool _canApproachTarget = false;

    [Header("Spin Wiggle Settings")]
    private bool _canWiggle = false;

    [Header("Spin Chance Settings")]
    private float _totalChance;
    private const int CHANCEDIVIDER = 100; //We divide the chances by the given chance divider value
    private float _divisionRate; //if total chances don't add up to 1 or overflows 1 , we calculate the amount that rounds it to 1
    private float _targetChance;
    private float _chanceRateToAdd;

    private void Awake()
    {
        _currentRotationIncreaseValue = _rSettings._rotationIncreaseValue;
        _currentSpinForSeconds = _rSettings._spinForSeconds;
        _degreeBetweenEachSlice = 360 / (_listTR.childCount);
        foreach (RouletteSlice slice in _listTR.GetComponentsInChildren<RouletteSlice>())
        {
            _rouletteSlices.Add(slice);
            slice.degree = _currentDegree;
            _currentDegree += _degreeBetweenEachSlice;
            _totalChance += slice.SpawnChance;
        }
        _divisionRate = _totalChance / CHANCEDIVIDER;
    }

    public void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_canRotate)
        {
            transform.eulerAngles += new Vector3(0, 0, _currentRotationIncreaseValue * Time.deltaTime);
        }
        else if(_canApproachTarget)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, new Vector3(0, 0, _targetDegree), _rSettings._rotationApproachRate);
            if (transform.eulerAngles.z == _targetDegree) _canApproachTarget = false;
        }
        else if (_canWiggle)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, new Vector3(0, 0, _rSettings._targetWiggleDegree), _rSettings._wiggleApproachRate);
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
        _canRotate = true;
        for (float i = _rSettings._spinForSeconds; i>= _rSettings._targetAmountForDivisionEnd; i/= _rSettings._divisionAmounSD)
        {   
            yield return new WaitForSeconds(_currentSpinForSeconds);
            _currentSpinForSeconds /= _rSettings._divisionAmounSD;
            _currentRotationIncreaseValue /= _rSettings._divisionAmounSD;
        }
        _canRotate = false;
        _canApproachTarget = true;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z % 360);
        while (_canApproachTarget) continue;
        _canWiggle = true;
        for (int i = 0; i <=2 ; i++)
        {
            yield return new WaitForSeconds(_rSettings._wiggleWaitForSeconds * (i + 1));
            _rSettings._targetWiggleDegree *= -1;
        }
        _canWiggle = false;
        _canApproachTarget = true;
        while (_canApproachTarget) continue;

    }
}
