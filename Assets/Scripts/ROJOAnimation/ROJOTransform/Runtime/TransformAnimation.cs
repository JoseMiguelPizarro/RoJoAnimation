using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;
[System.Serializable]
public class TransformAnimation : IDisposable
{
    public Move move;
    public Scale scale;
    public Rotate rotate;

    public AnimatedProperty animatedProperty;
    public AnimatedProperty animatedProperty2;
    public AnimatedProperty animatedProperty3;

    public UnityEvent OnStart, OnComplete;

    public Vector3 startPosition;
    public Vector3 startRotation;
    public Vector3 startScale;


    float _duration;
    WaitForSeconds _waitDuration;

    public void Dispose()
    {
        animatedProperty.Dispose();
        animatedProperty2.Dispose();
        animatedProperty3.Dispose();
    }

    public void Init(Transform target, Material material)
    {
        startPosition = target.localPosition;
        startRotation = target.localRotation.eulerAngles;
        startScale = target.localScale;

        animatedProperty.Init(material);
        animatedProperty2.Init(material);
        animatedProperty3.Init(material);

        CalculateTotalDuration();
        _waitDuration = new WaitForSeconds(_duration);

    }

    internal void Restore(TransformAnimationComponent target)
    {

#if UNITY_EDITOR
        Debug.Log("restoring!!");
#endif

        if (move.Enabled)
        {
            target.transform.DOMove(startPosition, move.duration).SetEase(move.ease);
        }
        if (rotate.Enabled)
        {
            target.transform.DORotate(startRotation, rotate.duration).SetEase(rotate.ease);
        }
        if (scale.Enabled)
        {
            target.transform.DOScale(startScale, scale.duration).SetEase(scale.ease);
        }

        RestoreProperty(animatedProperty);
        RestoreProperty(animatedProperty2);
        RestoreProperty(animatedProperty3);
    }

    public void PlayAnimation(TransformAnimationComponent target)
    {
        OnStart?.Invoke();
        Coroutiner.Wait(_waitDuration, () => OnComplete?.Invoke());

        if (move.Enabled)
        {
            target.transform.DOMove(startPosition + move.By, move.duration).SetEase(move.ease);
        }
        if (rotate.Enabled)
        {
            target.transform.DORotate(startRotation + rotate.By, rotate.duration).SetEase(rotate.ease);
        }
        if (scale.Enabled)
        {
            target.transform.DOScale(startScale + scale.By, scale.duration).SetEase(scale.ease);
        }

        PlayAnimatedProperty(animatedProperty);
        PlayAnimatedProperty(animatedProperty2);
        PlayAnimatedProperty(animatedProperty3);
    }

    private void CalculateTotalDuration()
    {
        _duration = Mathf.Max(
            move.TotalDuration,
            scale.TotalDuration,
            rotate.TotalDuration,
            animatedProperty.TotalDuration,
            animatedProperty2.TotalDuration,
            animatedProperty3.TotalDuration);
        _waitDuration = new WaitForSeconds(_duration);
    }

    private void PlayAnimatedProperty(AnimatedProperty property)
    {
        if (property.Enabled)
        {
            if (property.material != null)
            {
                ROJOAnimator.PropertyState(property.material, property);
            }
        }
    }

    private void RestoreProperty(AnimatedProperty property)
    {
        if (property.Enabled)
        {
            if (property.material != null)
            {
                ROJOAnimator.ResetPropertyState(property.material, property);
            }
        }
    }
}
