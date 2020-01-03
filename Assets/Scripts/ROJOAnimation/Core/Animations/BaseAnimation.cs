using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class BaseAnimation
{
    public float startDelay;
    public float duration;
    public float TotalDuration => startDelay + duration;
    public AnimationType AnimationType = AnimationType.Punch;

    public bool Enabled;

    public Vector3 from;

    public Vector3 By;

    public int Vibrato;

    public float Elasticity;

    public Ease ease;

    public void Reset(AnimationType animationType)
    {
        AnimationType = animationType;
        Enabled = false;
        from = Vector3.zero;
        By = Vector3.zero;
        Vibrato = 10;
        Elasticity = 1;
        ease = Ease.Linear;
        startDelay = 0;
        duration = 1;
    }
    public BaseAnimation(AnimationType animationType,
        bool enabled,
        Vector3 from, Vector3 by,
        int vibrato, float elasticity,
         Ease ease,
        float startDelay, float duration)
    {
        this.duration = duration;
        this.startDelay = startDelay;
        AnimationType = animationType;
        Enabled = enabled;
        this.from = from;
        By = by;
        Vibrato = vibrato;
        Elasticity = elasticity;
        this.ease = ease;
    }
}
