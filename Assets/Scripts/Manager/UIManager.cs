using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public ItemList itemList;

    [Header("Level Settings")]
    [SerializeField] private TextMeshProUGUI _levelText;

    [Header("Result Panel Settings")]
    [SerializeField] private Transform _resultPanel;
    [SerializeField] private Image _resultImage;
    [SerializeField] private TextMeshProUGUI _resultValue;


    [Header("Game Over Panel Settings")]
    [SerializeField] private Transform _gameOverPanel;

    [Header("Game Won Panel Settings")]
    [SerializeField] private Transform _gameWonPanel;


    [Header("Button Settings")]
    [SerializeField] private Button _gameOverRestartButton;
    [SerializeField] private Button _gameWonRestartButton;
    [SerializeField] private Button _resultYesButton;
    [SerializeField] private Button _resultNoButton;
    public Button _spinButton;

    [Header("Text Settings")]
    [SerializeField] private TextMeshProUGUI _cashValue;
    [SerializeField] private TextMeshProUGUI _goldValue;

    private System.Action<bool> OnGameOver;
    private System.Action OnNextLevel;

    [Header("UI Variables Type Settings")]
    [SerializeField] private RouletteItem _goldType;
    [SerializeField] private RouletteItem _cashType;
    private int _goldIndex = -1, _cashIndex = -1;

    public void SetResultPanel(RouletteSlice slice)
    {
        _resultPanel.gameObject.SetActive(true);
        _resultImage.sprite = slice.Image.sprite;
        _resultValue.SetText(slice.Value.ToString());
        var targetIndex = itemList.ArrangeSpaceForItem(slice);
        if (slice.Type == _goldType)
        {
            _goldIndex = targetIndex;
        }
        else if (slice.Type == _cashType)
        {
            _cashIndex = targetIndex;
        }
    }
    public void SetGameOverPanel()
    {
        _spinButton.enabled = false;
        _gameOverPanel.gameObject.SetActive(true);
    }

    public void SetCash(int value)
    {
        _cashValue.SetText(value.ToString());
    }
    public void SetGold(int value)
    {
        _goldValue.SetText(value.ToString());

    }
    public void Init(System.Action<bool> GameOver, System.Action OnNextLevel)
    {
        _gameOverRestartButton.onClick.AddListener(GameLost);
        _gameWonRestartButton.onClick.AddListener(GameWon);
        _resultYesButton.onClick.AddListener(Result_Screen_On_Click_Yes);
        _resultNoButton.onClick.AddListener(Result_Screen_On_Click_No);


        OnGameOver = GameOver;
        this.OnNextLevel = OnNextLevel;
    }

    void Result_Screen_On_Click_Yes()
    {
        _resultPanel.gameObject.SetActive(false);
        OnNextLevel();
        _spinButton.enabled = true;
    }
    void Result_Screen_On_Click_No()
    {
        _resultPanel.gameObject.SetActive(false);
        _gameWonPanel.gameObject.SetActive(true);
        var itemlist = _gameWonPanel.gameObject.GetComponent<ItemList>();
        itemlist.CopyItems(this.itemList);
    }

    void GameLost()
    {
        OnGameOver(false);
    }
    void GameWon()
    {
        OnGameOver(true);
    }

    public void setLevel(int level)
    {
        var strList = _levelText.text.Split(' ');
        strList[1] = level.ToString();
        _levelText.SetText(strList[0] + ' ' + strList[1]);

    }

    public int[] GetUIVariables()
    {
        return new int[] { itemList.ItemLists[_cashIndex].count, itemList.ItemLists[_goldIndex].count };
    }
    public void SetUIVariables()
    {
        if (_goldIndex != -1) _goldValue.SetText(itemList.ItemLists[_goldIndex].count.ToString());
        if (_cashIndex != -1) _cashValue.SetText(itemList.ItemLists[_cashIndex].count.ToString());
    }

}
