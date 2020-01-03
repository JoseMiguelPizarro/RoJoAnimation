using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
public static class VisualElementEX
{
    public static void LoadStyleSheet(this VisualElement element, string path)
    {
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
        element.styleSheets.Add(styleSheet);
    }

    public static void CloneAssetTree(this VisualElement container, string path)
    {
        var assetTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        assetTree.CloneTree(container);
    }

    public static void AddProperty(this VisualElement element, SerializedProperty property)
    {
        element.Add(new PropertyField(property));
    }
}
