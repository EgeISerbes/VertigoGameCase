using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int cash;
    public int gold;
    public int level;
    public float rotationStartPos;
    public ItemList list;
    public List<Item> itemLists;
    
}
[CreateAssetMenu(fileName = "LevelManager", menuName = "ScriptableObjects/LevelManager", order = 1)]
public class LevelManager : ScriptableObject
{
   
    public int currentLevel = 1;
    public SaveData _saveData;
    private string _dataPath;
    private string _dataText = string.Empty;
    public SaveData DataFile
    {
        get => _saveData;
        set => _saveData = value;
    }
    public void Init()
    {
        _dataPath = Application.persistentDataPath + "/save.json";
    }
    public void getLevelData()
    {
        _dataText = string.Empty;
        if (!CheckFileAvailability()) return;
        _dataText = File.ReadAllText(_dataPath);
        JsonUtility.FromJsonOverwrite(_dataText, _saveData);
    }
    public void SaveData()
    {
        CheckFileAvailability();
        _dataText = JsonUtility.ToJson(_saveData);
        File.WriteAllText(_dataPath, _dataText);
    }

    private bool CheckFileAvailability()
    {
        if (File.Exists(_dataPath)) return true;
        else File.Create(_dataPath); return false;
           
       
    }
    public void LoadData(SaveData data)
    {
        _saveData.cash = data.cash;
        _saveData.gold = data.gold;
        _saveData.rotationStartPos = data.rotationStartPos;
    }
}
