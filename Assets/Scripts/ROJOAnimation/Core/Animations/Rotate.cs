using UnityEngine;
using DG.Tweening;
using System;

[Serializable]
public class Rotate : BaseAnimation
{
    public RotateMode rotateMode = RotateMode.Fast;
    public Rotate(AnimationType animationType, bool enabled, Vector3 from, Vector3 by, int vibrato, float elasticity, Ease ease, float startDelay, float duration)
        : base(animationType, enabled, from, by, vibrato, elasticity, ease, startDelay, duration)
    {
    }
}
