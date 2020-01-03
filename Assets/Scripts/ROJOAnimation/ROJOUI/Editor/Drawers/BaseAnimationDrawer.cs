using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using RoJoStudios.EditorUtils;
using RoJoStudios.Utils;
using System;
public abstract class BaseAnimationDrawer : PropertyDrawer
{
    protected abstract string treePath { get; }
    protected abstract string stylePath { get; }
    private VisualElement _container;
    private VisualElement _propertiesContainer;
    private VisualElement _punchOptions;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        _container = ROJOEditor.LoadAssetTree(treePath);
        _container.AddToClassList("animation-field");
        _container.LoadStyleSheet(stylePath);
        _propertiesContainer = _container.Q<VisualElement>(name: "propertiesContainer");
        _punchOptions = _container.Q<VisualElement>(name: "punchOptions");

        var ease = _container.Q<EnumField>(name: "ease");
        var typeField = _container.Q<EnumField>(name: "typeField");
        ease.BindProperty(property.FindPropertyRelative("ease"));
        typeField.BindProperty(property.FindPropertyRelative("AnimationType"));
        typeField.RegisterValueChangedCallback(OnChangeType);

        var enabledToggle = _container.Q<Toggle>("enabledToggle");
        enabledToggle.BindProperty(property.FindPropertyRelative("Enabled"));
        enabledToggle.RegisterValueChangedCallback(OnEnbledToggle);

        var startDelayField = _container.Q<FloatField>(name: "startDelay");
        var durationField = _container.Q<FloatField>(name: "duration");
        var elasticityField = _container.Q<FloatField>(name: "elasticity");
        var vibratoField = _container.Q<IntegerField>(name: "vibrato");
        var byField = _container.Q<Vector3Field>(name: "by");

        startDelayField.BindProperty(property.FindPropertyRelative("startDelay"));
        durationField.BindProperty(property.FindPropertyRelative("duration"));
        elasticityField.BindProperty(property.FindPropertyRelative("Elasticity"));
        vibratoField.BindProperty(property.FindPropertyRelative("Vibrato"));
        byField.BindProperty(property.FindPropertyRelative("By"));

        _propertiesContainer.EnableInClassList("hidden", !enabledToggle.value);

        SetAnimationType((AnimationType)typeField.value);
        return _container;
    }

    private void OnChangeType(ChangeEvent<Enum> evt)
    {
        if (evt.newValue!=null)
        {
        var value = (AnimationType)evt.newValue;
        SetAnimationType(value);
        }
        else
        {
            SetAnimationType(AnimationType.Punch);
        }
    }

    private void SetAnimationType(AnimationType value)
    {
        switch (value)
        {
            case AnimationType.Punch:
                _punchOptions.EnableInClassList("hidden", false);
                break;
            case AnimationType.State:
                _punchOptions.EnableInClassList("hidden", true);
                break;
            default:
                break;
        }
    }

    private void OnEnbledToggle(ChangeEvent<bool> evt)
    {
        bool value = evt.newValue;
        _propertiesContainer.EnableInClassList("hidden", !value);
    }
}
