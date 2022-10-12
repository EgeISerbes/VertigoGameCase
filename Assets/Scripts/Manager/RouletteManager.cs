using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteManager : MonoBehaviour
{
    [SerializeField] private List<RouletteValues> _rouletteValuesList = new List<RouletteValues>();
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private RouletteSettings _rSettings;
    private int _activeRoulette = 0;

    [SerializeField] private RouletteItem _deathType;

   public void Init()
    {
        _uiManager._spinButton.onClick.AddListener(StartSpinning);
        foreach (RouletteValues rouletteValues in _rouletteValuesList)
        {
            rouletteValues.roulette.gameObject.SetActive(false);
            rouletteValues.rouletteIndicator.gameObject.SetActive(false);
            rouletteValues.roulette.Init(ReceivedTheSlice,_rSettings);
        }
        OrderLevelDivisionLevelsB_to_S();
        _activeRoulette = _rouletteValuesList.Count - 1;
        _rouletteValuesList[_activeRoulette].roulette.gameObject.SetActive(true);
        _rouletteValuesList[_activeRoulette].rouletteIndicator.gameObject.SetActive(true);
    }
    void OrderLevelDivisionLevelsB_to_S()
    {
        var tempList = _rouletteValuesList;
        for (int i = 0; i < _rouletteValuesList.Count;i++)
        {
            for (int j =0; j <_rouletteValuesList.Count-1 ; j++)
            {
                if (tempList[j].levelDivision > tempList[j + 1].levelDivision) continue;
                Swap(j,j+1);
            }
        }
    }

    public Roulette GetActiveRoulette()
    {
        return _rouletteValuesList[_activeRoulette].roulette;
    }
    void Swap( int i, int j)
    {
        var tempVal = _rouletteValuesList[i];
        _rouletteValuesList[i] = _rouletteValuesList[j];
        _rouletteValuesList[j] = tempVal;
    }
    public void ActivateRoullette(int level)
    {
        if (level == 0) return;
        for (int i = 0; i < _rouletteValuesList.Count; i++)
        {
            if(level % _rouletteValuesList[i].levelDivision == 0)
            {
                _rouletteValuesList[_activeRoulette].roulette.gameObject.SetActive(false);
                _rouletteValuesList[_activeRoulette].rouletteIndicator.gameObject.SetActive(false);
                _rouletteValuesList[i].roulette.gameObject.SetActive(true);
                _rouletteValuesList[i].rouletteIndicator.gameObject.SetActive(true);
                _activeRoulette = i;
                break;
            }
        }
        
    }

    private void StartSpinning()
    {
        _uiManager._spinButton.enabled = false;
        _rouletteValuesList[_activeRoulette].roulette.Init();
       StartCoroutine(_rouletteValuesList[_activeRoulette].roulette.Spin());
    }
    public void ReceivedTheSlice(RouletteSlice slice)
    {
        if (slice.Type != _deathType)
        {
            _uiManager.SetResultPanel(slice);
        }
        else
        {
            _uiManager.SetGameOverPanel();
        }
    }
    [System.Serializable]
    public struct RouletteValues
    {
        public Roulette roulette;
        public Transform rouletteIndicator;
        public int levelDivision;
        public string roulletteName;

    }
}
