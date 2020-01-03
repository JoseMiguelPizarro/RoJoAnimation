using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using RoJoStudios.EditorUtils;
using RoJoStudios.Utils;

[CustomPropertyDrawer(typeof(TransformAnimation))]
public class TransformAnimationDrawer : PropertyDrawer
{
    private const string TREE_NAME = "TransformAnimationDrawer.uxml";
    private const string STYLE_SHEET_NAME = "TransformAnimationDrawer.uss";

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        string styleSheetPath = this.GetAssetPath() + STYLE_SHEET_NAME;
        string treePath = this.GetAssetPath() + TREE_NAME;
        var container = ROJOEditor.LoadAssetTree(treePath);
        container.LoadStyleSheet(styleSheetPath);
        var eventsContainer = container.Q<VisualElement>(name: "Events");
        var animationContainer = container.Q<VisualElement>(name: "Animations");

        eventsContainer.AddProperty(property.FindPropertyRelative("OnStart"));
        eventsContainer.AddProperty(property.FindPropertyRelative("OnComplete"));

        animationContainer.AddProperty(property.FindPropertyRelative("move"));
        animationContainer.AddProperty(property.FindPropertyRelative("scale"));
        animationContainer.AddProperty(property.FindPropertyRelative("rotate"));
        animationContainer.AddProperty(property.FindPropertyRelative("animatedProperty"));
        animationContainer.AddProperty(property.FindPropertyRelative("animatedProperty2"));
        animationContainer.AddProperty(property.FindPropertyRelative("animatedProperty3"));


        ToggleView toggleView = container.Q<ToggleView>(name: "toggleView");
        toggleView.Init();
        return container;
    }
}
