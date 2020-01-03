using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using RoJoStudios.EditorUtils;
using RoJoStudios.Utils;

[CustomPropertyDrawer(typeof(AnimatedProperty))]
public class AnimatedPropertyDrawer : PropertyDrawer
{
    private const string TREE_NAME = "AnimatedPropertyDrawer.uxml";
    private const string STYLE_NAME = "AnimatedPropertyDrawer.uss";
    private VisualElement _propertiesContainer;
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        string treePath = this.GetAssetPath() + TREE_NAME;
        string stylePath = this.GetAssetPath() + STYLE_NAME;
        VisualElement container = ROJOEditor.LoadAssetTree(treePath);
        container.LoadStyleSheet(stylePath);
        container.AddToClassList("animation-field");

        Toggle enabledToggle = container.Q<Toggle>("enabledToggle");
        enabledToggle.BindProperty(property.FindPropertyRelative("Enabled"));
        enabledToggle.RegisterValueChangedCallback(OnEnabledToggle);


        _propertiesContainer = container.Q<VisualElement>("propertiesContainer");
        _propertiesContainer.Add(new PropertyField(property.FindPropertyRelative("property")));
        SetEnabled(enabledToggle.value);

        return container;
    }
    private void OnEnabledToggle(ChangeEvent<bool> evt)
    {
        bool value = evt.newValue;
        SetEnabled(value);
    }
    private void SetEnabled(bool value)
    {
        _propertiesContainer.EnableInClassList("hidden", !value);
    }
}


