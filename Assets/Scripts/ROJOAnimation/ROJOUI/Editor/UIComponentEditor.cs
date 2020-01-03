using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using RoJoStudios.EditorUtils;

[CustomEditor(typeof(UIComponent))]
public class UIComponentEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement container = new VisualElement();
        var so = serializedObject;
        container.AddProperty(so.FindProperty("OnPointerEnterBehaviour"));
        container.AddProperty(so.FindProperty("OnPointerExitBehaviour"));
        container.AddProperty(so.FindProperty("OnPointerDownBehaviour"));
        container.AddProperty(so.FindProperty("FreeAnimation"));

        return container;
    }

    private void OnDestroy()
    {

    }
}
