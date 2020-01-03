using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;

[Serializable]
public class AnimatedProperty : IDisposable
{
    #region Properties
    public Material material;

    public PropertyState property;
    /// <summary> Returns the maximum duration (including StartDelay) of the animation </summary>
    public float TotalDuration
    {
        get { return StartDelay + property.duration; }
    }

    #endregion

    #region Public Variables

    public bool Enabled;

    public float From;

    /// <summary> End value for the animation </summary>
    public float To;

    public Color ToColor;

    /// <summary> By value for the animation (used to perform relative changes to the current values) </summary>
    public float By;

    /// <summary> Allows the usage of custom from and to values for the Fade animation </summary>
    public bool UseCustomFromAndTo;

    /// <summary>
    ///     The number of loops this animation performs until it stops. If set to -1 it will perform infinite loops
    ///     <para> (used only by loop animations) </para>
    /// </summary>
    public int NumberOfLoops;

    /// <summary>
    ///     The loop type
    ///     <para> (used only by loop animations) </para>
    /// </summary>
    public LoopType LoopType;

    /// <summary> Determines if the animation should use an Ease or an AnimationCurve in order to calculate the rate of change over time </summary>
    public EaseType EaseType;

    /// <summary> Sets the ease of the tween. Easing functions specify the rate of change of a parameter over time. To see how default ease curves look, check out easings.net. Enabled only if EaseType is set to EaseType.Ease </summary>
    public Ease Ease;

    /// <summary> AnimationCurve used to calculate the rate of change of the animation over time. Enabled only if EaseType is set to EaseType.AnimationCurve </summary>
    public AnimationCurve AnimationCurve;

    /// <summary> Start delay duration for the animation </summary>
    public float StartDelay;

    /// <summary> Length of time for the animation (does not include the StartDelay) </summary>
    public float Duration;

    #endregion

    #region Constructors


    public AnimatedProperty(AnimationType animationType) { Reset(animationType); }


    public AnimatedProperty(
                bool enabled,
                float from, float to, float by,
                bool useCustomFromAndTo,
                int numberOfLoops, LoopType loopType,
                EaseType easeType, Ease ease, AnimationCurve animationCurve,
                float startDelay, float duration) : this(AnimationType.Punch)
    {
        Enabled = enabled;
        From = from;
        To = to;
        By = by;
        UseCustomFromAndTo = useCustomFromAndTo;
        NumberOfLoops = numberOfLoops;
        LoopType = loopType;
        EaseType = easeType;
        Ease = ease;
        AnimationCurve = new AnimationCurve(animationCurve.keys);
        StartDelay = startDelay;
        Duration = duration;
    }

    #endregion

    #region Public Methods

    /// <summary> Resets this instance to the default values </summary>
    /// <param name="animationType"> The animation type that determines the behavior of this animation </param>
    public void Reset(AnimationType animationType)
    {
        Enabled = false;
        From = 0f;
        To = 0f;
        By = 0.5f;
        UseCustomFromAndTo = false;
        NumberOfLoops = -1;
        LoopType = LoopType.Yoyo;
        EaseType = EaseType.Ease;
        Ease = Ease.Linear;
        AnimationCurve = new AnimationCurve();
        StartDelay = 0;
        Duration = 1;
    }

    /// <summary> Returns a deep copy </summary>
    public AnimatedProperty Copy()
    {
        return new AnimatedProperty(AnimationType.Punch)
        {
            Enabled = Enabled,
            From = From,
            To = To,
            By = By,
            UseCustomFromAndTo = UseCustomFromAndTo,
            NumberOfLoops = NumberOfLoops,
            LoopType = LoopType,
            EaseType = EaseType,
            Ease = Ease,
            AnimationCurve = new AnimationCurve(AnimationCurve.keys),
            StartDelay = StartDelay,
            Duration = Duration
        };
    }

    public void Init(Material material)
    {
        this.material = material;
        InitProperty(ref property, material);
    }

    private void InitProperty(ref PropertyState propertyState, Material material)
    {
        if (string.IsNullOrEmpty(propertyState.propertyName))
        {
            return;
        }
        int propertyId = Shader.PropertyToID(propertyState.propertyName);
        propertyState.propertyID = propertyId;
        switch (propertyState.propertyType)
        {
            case UnityEngine.Rendering.ShaderPropertyType.Color:
                propertyState.startColor = material.GetColor(propertyId);
                break;
            case UnityEngine.Rendering.ShaderPropertyType.Vector:
                break;
            case UnityEngine.Rendering.ShaderPropertyType.Float:
                propertyState.startValue = material.GetFloat(propertyId);
                break;
            case UnityEngine.Rendering.ShaderPropertyType.Range:
                propertyState.startValue = material.GetFloat(propertyId);
                break;
            case UnityEngine.Rendering.ShaderPropertyType.Texture:
                break;
            default:
                break;
        }

        SetPropertyStartValue(ref propertyState, material);
    }

    private void SetPropertyStartValue(ref PropertyState propertyState, Material material)
    {
        switch (propertyState.propertyType)
        {
            case UnityEngine.Rendering.ShaderPropertyType.Color:
                propertyState.startColor = material.GetColor(propertyState.propertyID);
                break;
            case UnityEngine.Rendering.ShaderPropertyType.Vector:
                break;
            case UnityEngine.Rendering.ShaderPropertyType.Float:
                propertyState.startValue = material.GetFloat(propertyState.propertyID);
                break;
            case UnityEngine.Rendering.ShaderPropertyType.Range:
                break;
            case UnityEngine.Rendering.ShaderPropertyType.Texture:
                break;
            default:
                break;
        }
    }

    public void Dispose()
    {
        switch (property.propertyType)
        {
            case UnityEngine.Rendering.ShaderPropertyType.Color:
                material.SetColor(property.propertyID, property.startColor);
                break;
            case UnityEngine.Rendering.ShaderPropertyType.Vector:
                break;
            case UnityEngine.Rendering.ShaderPropertyType.Float:
                material.SetFloat(property.propertyID, property.startValue);
                break;
            case UnityEngine.Rendering.ShaderPropertyType.Range:
                material.SetFloat(property.propertyID, property.startValue);
                break;
            case UnityEngine.Rendering.ShaderPropertyType.Texture:
                break;
            default:
                break;
        }
    }

    #endregion
}