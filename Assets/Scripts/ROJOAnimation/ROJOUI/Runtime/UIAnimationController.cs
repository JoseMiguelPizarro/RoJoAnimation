using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UIAnimation
{
    #region Public fields
    public UIBehaviourType behaviourType;
    public string name;
    public Move move;
    public Scale scale;
    public Rotate rotate;
    public AnimatedProperty animatedProperty;
    public UnityEvent OnStart;
    public UnityEvent OnComplete;
    #endregion Public Fields

    #region Private Field
    private WaitForSeconds _waitDuration;
    private float _duration;
    private Coroutine _currentAnimation;
    #endregion Private Fields


    public UIAnimation(UIBehaviourType behaviourType)
    {
        this.behaviourType = behaviourType;
    }

    public void Init(Material material)
    {
        animatedProperty.Init(material);

        CalculateTotalDuration();
    }

    private void CalculateTotalDuration()
    {
        _duration = Mathf.Max(move.TotalDuration, scale.TotalDuration, rotate.TotalDuration, animatedProperty.TotalDuration);
        _waitDuration = new WaitForSeconds(_duration);
    }

    public bool Enabled;

    public void PlayAnimation(UIComponent component)
    {
        if (!Enabled) return;
        if (_currentAnimation != null) Coroutiner.KillCoroutine(_currentAnimation);
        OnStart?.Invoke();
        _currentAnimation = Coroutiner.Wait(_waitDuration, () => OnComplete?.Invoke());


        if (scale.Enabled)
        {
            switch (scale.AnimationType)
            {
                case AnimationType.Punch:
                    ROJOAnimator.StopAnimation(component.RectTransform, AnimationType.Punch, AnimationAction.Scale);
                    ROJOAnimator.ScalePunch(component.RectTransform, scale, component.StartScale);
                    break;
                case AnimationType.State:
                    ROJOAnimator.StopAnimation(component.RectTransform, AnimationType.State, AnimationAction.Scale);
                    ROJOAnimator.ScaleState(component.RectTransform, scale, component.StartScale);
                    break;
                default:
                    break;
            }
        }

        if (move.Enabled)
        {
            switch (move.AnimationType)
            {
                case AnimationType.Punch:
                    ROJOAnimator.StopAnimation(component.RectTransform, AnimationType.Punch, AnimationAction.Move);
                    ROJOAnimator.MovePunch(component.RectTransform, move, component.StartPosition);
                    break;
                case AnimationType.State:
                    ROJOAnimator.StopAnimation(component.RectTransform, AnimationType.State, AnimationAction.Move);
                    ROJOAnimator.MoveState(component.RectTransform, move, component.StartPosition);
                    break;
                default:
                    break;
            }
        }

        if (rotate.Enabled)
        {
            switch (rotate.AnimationType)
            {
                case AnimationType.Punch:
                    ROJOAnimator.StopAnimation(component.RectTransform, AnimationType.Punch, AnimationAction.Rotate);
                    ROJOAnimator.RotatePunch(component.RectTransform, rotate, component.StartRotation);
                    break;
                case AnimationType.State:
                    ROJOAnimator.StopAnimation(component.RectTransform, AnimationType.State, AnimationAction.Rotate);
                    ROJOAnimator.RotateState(component.RectTransform, rotate, component.StartRotation);
                    break;
                default:
                    break;
            }
        }

        if (animatedProperty.Enabled)
        {
            switch (animatedProperty.property.AnimationType)
            {
                case AnimationType.Punch:
                    ROJOAnimator.StopAnimation(component.material, AnimationType.Punch, AnimationAction.Property);
                    ROJOAnimator.PropertyPunch(component.material, animatedProperty);
                    break;
                case AnimationType.State:
                    ROJOAnimator.StopAnimation(component.material, AnimationType.State, AnimationAction.Property);
                    ROJOAnimator.PropertyState(component.material, animatedProperty);
                    break;
                default:
                    break;
            }
        }
    }

    private void ResetAnimations(UIComponent component)
    {
        if (move.Enabled) component.ResetPosition();
        if (rotate.Enabled) component.ResetRotation();
        if (scale.Enabled) component.ResetScale();
    }
}
