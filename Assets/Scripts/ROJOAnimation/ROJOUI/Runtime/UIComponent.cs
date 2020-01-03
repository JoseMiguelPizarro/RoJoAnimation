using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    #region Public Fields
    public UIAnimation OnPointerEnterBehaviour = new UIAnimation(UIBehaviourType.OnPointerEnter);
    public UIAnimation OnPointerExitBehaviour = new UIAnimation(UIBehaviourType.OnPointerExit);
    public UIAnimation OnPointerDownBehaviour = new UIAnimation(UIBehaviourType.OnPointerDown);
    public UIAnimation FreeAnimation = new UIAnimation(UIBehaviourType.Free);
    [HideInInspector]
    public Material material;
    #endregion Public Fields

    #region Public Properties
    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }
            return _rectTransform;
        }
    }
    public Vector3 StartPosition { get; private set; }
    public Vector3 StartScale { get; private set; }
    public Vector3 StartRotation { get; private set; }
    #endregion Public Properties


    #region Private Fields
    private RectTransform _rectTransform;
    #endregion Private Fields

    private void Awake()
    {
        StartPosition = RectTransform.localPosition;
        StartScale = RectTransform.localScale;
        StartRotation = RectTransform.eulerAngles;

        material = GetComponent<Image>().material;
        OnPointerEnterBehaviour.Init(material);
        OnPointerExitBehaviour.Init(material);
        OnPointerDownBehaviour.Init(material);
    }

    #region EventSystem Events
    public void OnPointerExit(PointerEventData eventData)
    {
        ExecutePointerExit();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        ExecutePointerDown();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ExecutePointerEnter();
    }
    #endregion EventSystem Events


    private void ExecutePointerDown()
    {
        if (OnPointerDownBehaviour.Enabled)
            OnPointerDownBehaviour.PlayAnimation(this);
    }

    private void ExecutePointerExit()
    {
        if (OnPointerExitBehaviour.Enabled)
            OnPointerExitBehaviour.PlayAnimation(this);
    }


    private void ExecutePointerEnter()
    {
        if (OnPointerEnterBehaviour.Enabled)
        {
            OnPointerEnterBehaviour.PlayAnimation(this);
        }
    }

    internal void ResetPosition() => _rectTransform.anchoredPosition3D = StartPosition;
    internal void ResetRotation() => _rectTransform.localEulerAngles = StartRotation;
    internal void ResetScale() => _rectTransform.localScale = StartScale;


}
