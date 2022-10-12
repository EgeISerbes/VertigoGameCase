using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RouletteSettings", menuName = "ScriptableObjects/RouletteSettings", order = 1)]
public class RouletteSettings : ScriptableObject
{
    [Header("Spin Seconds Settings")]
    public float _spinForSeconds;
    public int totalRotationCount;

    [Header("Spin Degree Settings")]
    public float _rotationIncreaseValue;
    public float _rotationApproachRate;

    [Header("Spin Wiggle Settings")]
    public float _wiggleIncreaseRate;
    public float _wiggleWaitForSeconds;
    public float _targetWiggleDegree;

    [Header("Developer Settings")]
    public float _targetRotationValue;
    public float _targetRotationDecreaseValue;
    public int divisionAmount;
}
