using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Settings = UISettings;
using Utils = ROJOAnimatorUtils;
using UnityEngine.Events;
using System;
using UnityEngine.Rendering;

public static partial class ROJOAnimator
{
    public const float DefaultDurationOnComplete = 0.05f;

    static ROJOAnimator()
    {
        DOTween.defaultRecyclable = true;
    }
    public static void PropertyPunch(Material target, AnimatedProperty animatedProperties, UnityAction onStartCallback = null, UnityAction onCompleteCallback = null)
    {
        var p = animatedProperties.property;

        if (p.propertyType == ShaderPropertyType.Color)
        {
            target.DOColor(p.colorValue, p.propertyID, p.duration / 2)
                .SetId(Utils.GetTweenId(target, AnimationType.Punch, AnimationAction.Property))
                .SetEase(p.ease)
                .OnComplete(() => target.DOColor(p.startColor, p.propertyID, p.duration / 2).SetEase(p.ease));
        }
        else
        {
            target.DOFloat(p.floatValue, p.propertyID, p.duration / 2)
                .SetId(Utils.GetTweenId(target, AnimationType.Punch, AnimationAction.Property))
                .SetEase(p.ease)
                .OnComplete(() => target.DOFloat(p.startValue, p.propertyID, p.duration / 2).SetEase(p.ease));
        }
    }

    public static Tween MoveTween(RectTransform target, ROJOAnimation animation, Vector3 startValue, Vector3 endValue)
    {
        target.anchoredPosition3D = startValue;
        Tween tween = target.DOAnchorPos3D(endValue,
            animation.Move.duration).SetDelay(animation.Move.startDelay).SetUpdate(true).SetSpeedBased(false);
        tween.SetEase(animation.Move.ease);
        return tween;
    }

    public static Tween MovePunchTween(RectTransform target, Move moveAnimation)
    {
        return target.DOPunchAnchorPos(moveAnimation.By, moveAnimation.duration, moveAnimation.Vibrato, moveAnimation.Elasticity)
                     .SetDelay(moveAnimation.startDelay)
                     .SetUpdate(true)
                     .SetSpeedBased(false)
                     .SetEase(moveAnimation.ease);
    }

    internal static void StopAnimations(object rectTransform, AnimationType punch)
    {
        throw new NotImplementedException();
    }
 
    public static Tween MoveStateTween(RectTransform target, Move animation, Vector3 startValue)
    {
        Tween tween = target.DOAnchorPos(startValue + animation.By, animation.duration)
                            .SetDelay(animation.startDelay)
                            .SetUpdate(true)
                            .SetSpeedBased(false);
        tween.SetEase(animation.ease);
        return tween;
    }
  
    public static Tween RotateTween(RectTransform target, ROJOAnimation animation, Vector3 startValue, Vector3 endValue)
    {
        target.localRotation = Quaternion.Euler(startValue);
        Tweener tween = target.DOLocalRotate(endValue, animation.Rotate.duration, animation.Rotate.rotateMode)
                              .SetDelay(animation.Rotate.startDelay)
                              .SetUpdate(Settings.IgnoreUnityTimescale)
                              .SetSpeedBased(Settings.SpeedBasedAnimations);
        tween.SetEase(animation.Rotate.ease);
        return tween;
    }

    public static Tween RotatePunchTween(RectTransform target, Rotate rotateAnimation)
    {
        return target.DOPunchRotation(rotateAnimation.By, rotateAnimation.duration, rotateAnimation.Vibrato, rotateAnimation.Elasticity)
                     .SetDelay(rotateAnimation.startDelay)
                     .SetUpdate(Settings.IgnoreUnityTimescale)
                     .SetSpeedBased(Settings.SpeedBasedAnimations)
                     .SetEase(rotateAnimation.ease);
    }

    public static Tween RotateStateTween(RectTransform target, Rotate animation, Vector3 startValue)
    {
        Tween tween = target.DOLocalRotate(startValue + animation.By, animation.duration, animation.rotateMode)
                            .SetDelay(animation.startDelay)
                            .SetUpdate(Settings.IgnoreUnityTimescale)
                            .SetSpeedBased(Settings.SpeedBasedAnimations);
        tween.SetEase(animation.ease);
        return tween;
    }

    public static Tween ScaleTween(RectTransform target, ROJOAnimation animation, Vector3 startValue, Vector3 endValue)
    {
        startValue.z = 1f;
        endValue.z = 1f;
        target.localScale = startValue;
        Tweener tween = target.DOScale(endValue, animation.Scale.duration)
                              .SetDelay(animation.Scale.startDelay)
                              .SetUpdate(Settings.IgnoreUnityTimescale)
                              .SetSpeedBased(Settings.SpeedBasedAnimations);

        tween.SetEase(animation.Scale.ease);

        return tween;
    }

    public static Tween ScalePunchTween(RectTransform target, Scale scaleAnimation)
    {
        scaleAnimation.By.z = 1f;
        var tween = target.DOPunchScale(scaleAnimation.By, scaleAnimation.duration, scaleAnimation.Vibrato, scaleAnimation.Elasticity)
                     .SetDelay(scaleAnimation.startDelay)
                     .SetUpdate(Settings.IgnoreUnityTimescale)
                     .SetSpeedBased(Settings.SpeedBasedAnimations);
        tween.SetEase(scaleAnimation.ease);

        return tween;
    }

    public static Tween ScaleStateTween(RectTransform target, Scale animation, Vector3 startValue)
    {
        animation.By.z = 0f;
        Tween tween = target.DOScale(startValue + animation.By, animation.duration)
                            .SetDelay(animation.startDelay)
                            .SetUpdate(Settings.IgnoreUnityTimescale)
                            .SetSpeedBased(Settings.SpeedBasedAnimations);
        tween.SetEase(animation.ease);
        return tween;
    }

    public static void Move(RectTransform target, ROJOAnimation animation, Vector3 startValue, Vector3 endValue, bool instantAction = false, UnityAction onStartCallback = null, UnityAction onCompleteCallback = null)
    {
        if (!animation.Move.Enabled && !instantAction)
            return;

        if (instantAction)
        {
            target.anchoredPosition3D = endValue;
            onStartCallback?.Invoke();
            onCompleteCallback?.Invoke();
            return;
        }


        DOTween.Sequence()
            .SetId(GetTweenId(target, animation.AnimationType, AnimationAction.Move))
            .SetUpdate(Settings.IgnoreUnityTimescale)
            .SetSpeedBased(Settings.SpeedBasedAnimations)
            .OnStart(() => onStartCallback?.Invoke())
            .OnComplete(() => onCompleteCallback?.Invoke())
            .Append(MoveTween(target, animation, startValue, endValue))
            .Play();

    }

    public static void Rotate(RectTransform target, ROJOAnimation animation, Vector3 startValue, Vector3 endValue, bool instantAction = false, UnityAction onStartCallback = null, UnityAction onCompleteCallback = null)
    {
        if (!animation.Rotate.Enabled && !instantAction) return;

        if (instantAction)
        {
            target.localRotation = Quaternion.Euler(endValue);
            onStartCallback?.Invoke();
            onCompleteCallback?.Invoke();
            return;
        }

        DOTween.Sequence()
            .SetId(GetTweenId(target, animation.AnimationType, AnimationAction.Rotate))
            .SetUpdate(Settings.IgnoreUnityTimescale)
            .SetSpeedBased(Settings.SpeedBasedAnimations)
            .OnStart(() => onStartCallback?.Invoke())
            .OnComplete(() => onCompleteCallback?.Invoke())
            .Append(RotateTween(target, animation, startValue, endValue))
            .Play();
    }

    public static void Scale(RectTransform target, ROJOAnimation animation, Vector3 startValue, Vector3 endValue, bool instantAction = false, UnityAction onStartCallback = null, UnityAction onCompleteCallback = null)
    {
        if (!animation.Scale.Enabled && !instantAction) return;

        startValue.z = 1;
        endValue.z = 1;
        if (instantAction)
        {
            target.localScale = endValue;
            onStartCallback?.Invoke();
            onCompleteCallback?.Invoke();
            return;
        }

        DOTween.Sequence()
               .SetId(GetTweenId(target, animation.AnimationType, AnimationAction.Scale))
               .SetUpdate(Settings.IgnoreUnityTimescale)
               .SetSpeedBased(Settings.SpeedBasedAnimations)
               .OnStart(() => onStartCallback?.Invoke()
               )
               .OnComplete(() => onCompleteCallback?.Invoke())
               .Append(ScaleTween(target, animation, startValue, endValue))
               .Play();
    }

    public static void MovePunch(RectTransform target, Move moveAnimation, Vector3 startValue)
    {
        if (!moveAnimation.Enabled) return;
        if (moveAnimation.AnimationType != AnimationType.Punch) return;

        DOTween.Sequence()
               .SetId(GetTweenId(target, moveAnimation.AnimationType, AnimationAction.Move))
               .SetUpdate(Settings.IgnoreUnityTimescale)
               .SetSpeedBased(Settings.SpeedBasedAnimations)
               .OnComplete(() =>
               {
                   target.DOAnchorPos(startValue, DefaultDurationOnComplete);
               })
               .Append(MovePunchTween(target, moveAnimation))
               .Play();
    }

    public static void RotatePunch(RectTransform target, Rotate rotateAnimation, Vector3 startValue)
    {
        if (!rotateAnimation.Enabled) return;
        if (rotateAnimation.AnimationType != AnimationType.Punch) return;

        DOTween.Sequence()
               .SetId(GetTweenId(target, rotateAnimation.AnimationType, AnimationAction.Rotate))
               .SetUpdate(Settings.IgnoreUnityTimescale)
               .SetSpeedBased(Settings.SpeedBasedAnimations)
               .OnComplete(() =>
               {
                   target.DOLocalRotate(startValue, DefaultDurationOnComplete).OnComplete(() =>
                   {
                   }).Play();
               })
               .Append(RotatePunchTween(target, rotateAnimation))
               .Play();
    }

    public static void ScalePunch(RectTransform target, Scale scaleAnimation, Vector3 startValue)
    {
        if (!scaleAnimation.Enabled) return;
        if (scaleAnimation.AnimationType != AnimationType.Punch) return;

        DOTween.Sequence()
               .SetId(GetTweenId(target, scaleAnimation.AnimationType, AnimationAction.Scale))
               .SetUpdate(Settings.IgnoreUnityTimescale)
               .SetSpeedBased(Settings.SpeedBasedAnimations)
               .OnComplete(() =>
               {
                   target.DOScale(startValue, DefaultDurationOnComplete);
               })
               .Append(ScalePunchTween(target, scaleAnimation))
               .Play();
    }

    public static void MoveState(RectTransform target, Move animation, Vector3 startValue)
    {
        if (!animation.Enabled) return;
        if (animation.AnimationType != AnimationType.State) return;

        DOTween.Sequence()
               .SetId(GetTweenId(target, animation.AnimationType, AnimationAction.Move))
               .SetUpdate(Settings.IgnoreUnityTimescale)
               .SetSpeedBased(Settings.SpeedBasedAnimations)
               .Append(MoveStateTween(target, animation, startValue))
               .Play();
    }


    public static void RotateState(RectTransform target, Rotate animation, Vector3 startValue)
    {
        if (!animation.Enabled) return;
        if (animation.AnimationType != AnimationType.State) return;

        DOTween.Sequence()
               .SetId(GetTweenId(target, animation.AnimationType, AnimationAction.Rotate))
               .SetUpdate(Settings.IgnoreUnityTimescale)
               .Append(RotateStateTween(target, animation, startValue))
               .Play();
    }

    public static void ScaleState(RectTransform target, Scale animation, Vector3 startValue)
    {
        if (!animation.Enabled) return;
        if (animation.AnimationType != AnimationType.State) return;

        DOTween.Sequence()
               .SetId(GetTweenId(target, animation.AnimationType, AnimationAction.Scale))
               .SetUpdate(Settings.IgnoreUnityTimescale)
               .Append(ScaleStateTween(target, animation, startValue))
               .Play();
    }

    public static void PropertyState(Material material, AnimatedProperty animation)
    {
        if (!animation.Enabled) return;
        if (animation.property.AnimationType != AnimationType.State) return;
        DOTween.Sequence()
            .SetId(GetTweenId(animation, animation.property.AnimationType, AnimationAction.Property))
            .SetUpdate(Settings.IgnoreUnityTimescale)
            .SetSpeedBased(Settings.SpeedBasedAnimations)
            .Append(PropertyStateTween(material, animation))
            .Play();
    }

    public static void ResetPropertyState(Material material, AnimatedProperty animation)
    {
        DOTween.Sequence()
            .SetId(GetTweenId(animation, animation.property.AnimationType, AnimationAction.Property))
            .SetUpdate(Settings.IgnoreUnityTimescale)
            .SetSpeedBased(Settings.SpeedBasedAnimations)
            .Append(ResetPropertyStateTween(material, animation))
            .Play();
    }

    private static Tween ResetPropertyStateTween(Material material, AnimatedProperty animation)
    {
        PropertyState state = animation.property;
        Tween tween;
        switch (state.propertyType)
        {
            case ShaderPropertyType.Color:
                tween = material.DOColor(state.startColor, state.propertyID, state.duration);
                break;

            default:
                tween = material.DOFloat(state.floatValue, state.propertyID, state.duration);
                break;
        }

        tween.SetDelay(animation.StartDelay)
            .SetUpdate(Settings.IgnoreUnityTimescale)
            .SetSpeedBased(Settings.SpeedBasedAnimations)
            .SetEase(state.ease);

        return tween;
    }

    private static Tween PropertyStateTween(Material material, AnimatedProperty animation)
    {
        PropertyState state = animation.property;
        Tween tween;
        switch (state.propertyType)
        {
            case ShaderPropertyType.Color:
                tween = material.DOColor(state.colorValue, state.propertyID, state.duration);
                break;
            default:
                tween = material.DOFloat(state.floatValue, state.propertyID, state.duration);
                break;
        }

        tween.SetDelay(animation.StartDelay)
            .SetUpdate(Settings.IgnoreUnityTimescale)
            .SetSpeedBased(Settings.SpeedBasedAnimations)
            .SetEase(animation.Ease);
        return tween;
    }

    public static void StopAnimations(object target, AnimationType AnimationType, bool complete = true)
    {
        if (target == null) return;
        DOTween.Kill(GetTweenId(target, AnimationType, AnimationAction.Move), complete);
        DOTween.Kill(GetTweenId(target, AnimationType, AnimationAction.Rotate), complete);
        DOTween.Kill(GetTweenId(target, AnimationType, AnimationAction.Scale), complete);
        DOTween.Kill(GetTweenId(target, AnimationType, AnimationAction.Fade), complete);
        DOTween.Kill(GetTweenId(target, AnimationType, AnimationAction.Property), complete);
    }

    public static void StopAnimation(object target, AnimationType animationType, AnimationAction action, bool complete = true)
    {
        if (target == null) return;
        string id = GetTweenId(target, animationType, action);
        int killed = DOTween.Kill(id);

#if UNITY_EDITOR
        Debug.Log($"Tweens killded {killed}");
#endif

    }
    private static string GetTweenId(object target, AnimationType animationType, AnimationAction action) => Utils.GetTweenId(target, animationType, action);

}
