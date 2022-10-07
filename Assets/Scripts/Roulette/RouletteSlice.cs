using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RouletteSlice : MonoBehaviour
{
    [SerializeField] protected int _value;
    public Sprite _sprite;
    [SerializeField] protected Image image;
    [Tooltip("Choose between 0 and 100")]
    [SerializeField] protected float _spawnChance;
    [SerializeField] protected string _name;
    [HideInInspector] protected float degree;

    public float SpawnChance
    {
        get => _spawnChance;
    }
    public virtual void Init()
    {
        image.sprite = _sprite;
    }

}
