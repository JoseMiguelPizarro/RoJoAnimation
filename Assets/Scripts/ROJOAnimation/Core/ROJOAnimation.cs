using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ROJOAnimation
{
    public bool Enabled => Move.Enabled || Scale.Enabled || Rotate.Enabled || animatedProperties.Enabled;

    public float StartDelay
    {
        get
        {
            if (!Enabled)
            {
                return 0;
            }

            return Mathf.Min(Move.Enabled ? Move.startDelay : 10000,
                Rotate.Enabled ? Rotate.startDelay : 10000,
                Scale.Enabled ? Scale.startDelay : 10000,
                animatedProperties.Enabled ? animatedProperties.StartDelay : 10000);
        }
    }

    public float TotalDuration
    {
        get
        {
            return Mathf.Max(Move.Enabled ? Move.TotalDuration : 0,
                Rotate.Enabled ? Rotate.TotalDuration : 0,
                Scale.Enabled ? Scale.TotalDuration : 0,
                animatedProperties.Enabled ? animatedProperties.TotalDuration : 0);
        }
    }

    public void Init(Material material)
    {
        animatedProperties.Init(material);
    }

    public AnimationType AnimationType = AnimationType.Punch;

    public Move Move;
    public Rotate Rotate;
    public Scale Scale;
    public AnimatedProperty animatedProperties;

    public ROJOAnimation(Move move, Rotate rotate, Scale scale, AnimatedProperty property)
    {
        this.Move = move;
        this.Rotate = rotate;
        this.Scale = scale;
        this.animatedProperties = property;
    }
}
