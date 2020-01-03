using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
public class TransformAnimationComponent : MonoBehaviour
{
    public Renderer targetRenderer;
    public TransformAnimation transformAnimation;

    private Material _material;

    public AnimatedProperty animatedProperty => transformAnimation.animatedProperty;
    public Move move => transformAnimation.move;
    public Scale scale => transformAnimation.scale;
    public Rotate rotate => transformAnimation.rotate;

    private void Awake()
    {
        _material = targetRenderer.material;
        transformAnimation.Init(transform, _material);
    }

    public void PlayAnimations()
    {
        transformAnimation.PlayAnimation(this);
    }

    public void Restore()
    {
        transformAnimation.Restore(this);
    }

    private void OnDestroy()
    {
        Destroy(_material);
    }
}
