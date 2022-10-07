using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RouletteSliceWithText : RouletteSlice
{
    [SerializeField] private TextMeshProUGUI _uiText;
    public override void Init()
    {
        base.Init();
        _uiText.SetText(_value.ToString());
    }
}
