using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformAnimator
{
    public static void PlayAnimation(TransformAnimationComponent target)
    {
        Move move = target.move;
        Scale scale = target.scale;
        Rotate rotate = target.rotate;
        AnimatedProperty property = target.animatedProperty;

        if (move.Enabled)
        {

        }
    }
}
