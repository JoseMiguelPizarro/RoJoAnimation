using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ROJOAnimatorUtils
{
    public static string GetTweenId(object target, AnimationType animationType, AnimationAction animationAction)
    { return target.GetHashCode() + "-" + animationType + "-" + animationAction; }
}
