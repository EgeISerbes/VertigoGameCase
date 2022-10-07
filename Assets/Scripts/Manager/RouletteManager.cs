using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteManager : MonoBehaviour
{
    [SerializeField] private List<RouletteValues> _rouletteValuesList = new List<RouletteValues>();
    [SerializeField] private UIManager _uiManager;
    private int _activeRoulette = 0;
   public void Init()
    {
        foreach (RouletteValues rouletteValues in _rouletteValuesList)
        {
            rouletteValues.roulette.gameObject.SetActive(false);
            rouletteValues.roulette.Init();
        }
        OrderLevelDivisionLevelsB_to_S();
        _activeRoulette = _rouletteValuesList.Count - 1;
        _rouletteValuesList[_activeRoulette].roulette.gameObject.SetActive(true);
    }
    void OrderLevelDivisionLevelsB_to_S()
    {
        var tempList = _rouletteValuesList;
        for (int i = 0; i < _rouletteValuesList.Count;i++)
        {
            for (int j =0; j <_rouletteValuesList.Count-1 ; j++)
            {
                if (tempList[j].levelDivision > tempList[j + 1].levelDivision) continue;
                Swap(tempList[j], tempList[j + 1]);
            }
        }
    }

    void Swap(RouletteValues a, RouletteValues b)
    {
        var tempVal = a;
        a = b;
        b = a;
    }
    public void ActivateRoullette(int level)
    {
        if (level == 0) return;
        for (int i = 0; i < _rouletteValuesList.Count; i++)
        {
            if(level % _rouletteValuesList[i].levelDivision == 0)
            {
                _rouletteValuesList[i].roulette.gameObject.SetActive(true);
                _rouletteValuesList[_activeRoulette].roulette.gameObject.SetActive(false);
                _activeRoulette = i;
            }
        }
        
    }
    [System.Serializable]
    public struct RouletteValues
    {
        public Roulette roulette;
        public int levelDivision;
        public string roulletteName;

    }
}
