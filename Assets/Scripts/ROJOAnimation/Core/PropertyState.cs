using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;
using UnityEngine.Rendering;
[System.Serializable]
public struct PropertyState
{
    public float duration;
    public string propertyName;
    public float floatValue;
    public Vector4 vectorValue;
    public Color colorValue;
    public Ease ease;
    public ShaderPropertyType propertyType;
    public AnimationType AnimationType;

    [HideInInspector]
    public int propertyID;
    [HideInInspector]
    public float startValue;
    [HideInInspector]
    public Color startColor;
}
