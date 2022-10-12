using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Item : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI uiText;
    private RouletteItem _itemType;
    public int count = 0;
    public RouletteItem ItemType
    {
        get => _itemType;
        set => _itemType = value;
    }
}
