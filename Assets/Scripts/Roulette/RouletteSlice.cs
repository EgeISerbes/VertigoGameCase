using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RouletteSlice : MonoBehaviour
{
    [SerializeField] private RouletteItem _rouletteItem;
    [SerializeField] protected int _value;
    public Sprite _sprite;
    protected Image image;
    [Tooltip("Choose between 0 and 100")]
    [Range(0, 100)] [SerializeField] protected float _spawnChance;
    [SerializeField] protected string _name;
    [HideInInspector] public float degree;

    public RouletteItem Type
    {
        get => _rouletteItem;
    }
    public int Value
    {
        get => _value;
    }
    public float SpawnChance
    {
        get => _spawnChance;
    }
    public Image Image
    {
        get => image;
    }
    public virtual void Init()
    {
        image = GetComponent<Image>();
        image.sprite = _sprite;
    }

}
