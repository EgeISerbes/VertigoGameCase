using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private RouletteManager _rouletteManager;
    [SerializeField] private LevelManager _levelManager;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        _levelManager.Init();
        _levelManager.getLevelData();
        if(_levelManager.DataFile.list != null) _uiManager.itemList.CopyItems(_levelManager.DataFile.list);
        _uiManager.SetCash(_levelManager.DataFile.cash);
        _uiManager.SetGold(_levelManager.DataFile.gold);
        _uiManager.Init(GameOver, LevelPassed);
        _uiManager.setLevel(_levelManager.currentLevel);
        _rouletteManager.ActivateRoullette(_levelManager.currentLevel);
        _rouletteManager.Init();
        

        
        var tempVar = _rouletteManager.GetActiveRoulette();
        tempVar.SetStartPos(_levelManager.DataFile.rotationStartPos);
        tempVar.Init();
    }


    public void GameOver(bool hasWon)
    {
        if (hasWon)
        {
            var arr = _uiManager.GetUIVariables();
            _uiManager.SetUIVariables();
            _levelManager.DataFile.rotationStartPos = _rouletteManager.GetActiveRoulette().GetStartPos();
            _levelManager.DataFile.cash = arr[0];
            _levelManager.DataFile.gold = arr[1];
            _levelManager.DataFile.list = _uiManager.itemList;
            _levelManager.SaveData();
        }
        else
        {
            _levelManager.currentLevel = 1;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void ReceiveDataAlways()
    {
        _levelManager.DataFile.rotationStartPos = _rouletteManager.GetActiveRoulette().GetStartPos();
        _levelManager.DataFile.list.CopyItems(_uiManager.itemList);
        _levelManager.DataFile.itemLists = _levelManager.DataFile.list.ItemLists;
        _levelManager.SaveData();
    }
    public void LevelPassed()
    {
        _levelManager.currentLevel += 1;
        _uiManager.setLevel(_levelManager.currentLevel);
        _rouletteManager.ActivateRoullette(_levelManager.currentLevel);
    }
    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            ReceiveDataAlways();
        }
    }
    private void OnApplicationQuit()
    {
        _levelManager.currentLevel = 1;
        ReceiveDataAlways();
    }
}
