using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RouletteItem", order = 1)]
[System.Serializable]
public class RouletteItem : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
}
