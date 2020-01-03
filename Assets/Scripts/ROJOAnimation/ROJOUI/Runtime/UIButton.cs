using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIButton : UISelectable
{
    private Vector3 localScale;
    private Tweener pointerDownTween;
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        localScale = transform.localScale;
        button.onClick.AddListener(() => Debug.Log("click"));
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        pointerDownTween?.Complete(true);
        pointerDownTween = transform.DOPunchScale(Vector3.one * .2f, .3f).OnComplete(() => transform.localScale = localScale);
        OnClick?.Invoke();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        Debug.Log("selected");
        transform.DOShakeRotation(.1f);
    }
}
