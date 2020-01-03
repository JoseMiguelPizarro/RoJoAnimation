using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using DG.Tweening;
public class UISelectable : MonoBehaviour, ISelectHandler, IPointerDownHandler, IPointerExitHandler
{
    public static List<UISelectable> allSelectablesArray = new List<UISelectable>();
    public UnityEvent OnClick;
    protected RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        allSelectablesArray.Add(this);
    }

    private void OnDisable()
    {
        allSelectablesArray.Remove(this);
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnClick?.Invoke();

#if UNITY_EDITOR
        Debug.Log($"Pointer Down On {gameObject.name}");
#endif

    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
    }

    public UISelectable FindSelectable(Vector2 dir)
    {
        dir = dir.normalized;
        Vector3 currentPosition = transform.TransformPoint(GetPointOnRectEdge(transform as RectTransform, Quaternion.Inverse(transform.rotation) * dir));
        float maxScore = float.NegativeInfinity;
        UISelectable bestCandidate = null;
        for (int index = 0; index < allSelectablesArray.Count; index++)
        {
            UISelectable selectable2 = allSelectablesArray[index];
            if (selectable2 != this || selectable2 != null)
            {
                RectTransform rect = selectable2.transform as RectTransform;
                Vector3 position = rect == null ? Vector3.zero : (Vector3)rect.rect.center;
                Vector3 rhs = selectable2.transform.TransformPoint(position) - currentPosition;
                float angleFactor = Vector3.Dot(dir, rhs);
                if (angleFactor > 0)
                {
                    float score = angleFactor / rhs.sqrMagnitude;
                    if (score > maxScore)
                    {
                        maxScore = score;
                        bestCandidate = selectable2;
                    }
                }
            }
        }
        return bestCandidate;
    }

    private static Vector3 GetPointOnRectEdge(RectTransform rect, Vector2 dir)
    {
        if (rect == null)
            return Vector3.zero;
        if (dir != Vector2.zero)
            dir /= Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
        dir = rect.rect.center + Vector2.Scale(rect.rect.size, dir * 0.5f);
        return dir;
    }

    private void OnDestroy()
    {
        allSelectablesArray.Remove(this);
    }
}
