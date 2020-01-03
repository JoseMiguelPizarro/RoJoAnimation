using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using RoJoStudios.Utils;
using RoJoStudios.EditorUtils;


[CustomPropertyDrawer(typeof(UIAnimation))]
public class UIAnimationDrawer : PropertyDrawer
{
    private const string TREE_NAME = "UIAnimationDrawer.uxml";
    private const string STYLE_NAME = "UIAnimationDrawer.uss";
    VisualElement _container;
    TextElement _behaviourName;
    VisualElement _animationContainer;
    VisualElement _eventsContainer;
    VisualElement _mainContainer;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        string path = this.GetAssetPath() + TREE_NAME;
        _container = ROJOEditor.LoadAssetTree(path);
        string stylePath = this.GetAssetPath() + STYLE_NAME;
        _container.LoadStyleSheet(stylePath);
        var enabledToggle = _container.Q<Toggle>(name: "enabledToggle");
        enabledToggle.RegisterValueChangedCallback(OnToggleEnabled);
        enabledToggle.BindProperty(property.FindPropertyRelative("Enabled"));

        _behaviourName = _container.Q<TextElement>(name: "behaviourName");
        _behaviourName.text = property.name;
        _mainContainer = _container.Q<VisualElement>(name: "mainContainer");
        _animationContainer = _container.Q<VisualElement>(name: "Animations");
        _eventsContainer = _container.Q<VisualElement>(name: "Events");

        ToggleView toggleView = _container.Q<ToggleView>(name: "toggleView");
        toggleView.Init();

    

        var moveProperty = property.FindPropertyRelative("move");
        var scaleProperty = property.FindPropertyRelative("scale");
        var rotateProperty = property.FindPropertyRelative("rotate");
        var propertiesProperty = property.FindPropertyRelative("animatedProperty");

        var onStartProperty = property.FindPropertyRelative("OnStart");
        var onEndProperty = property.FindPropertyRelative("OnComplete");

        _animationContainer.Add(new PropertyField(moveProperty));
        _animationContainer.Add(new PropertyField(scaleProperty));
        _animationContainer.Add(new PropertyField(rotateProperty));
        _animationContainer.Add(new PropertyField(propertiesProperty));

        _eventsContainer.Add(new PropertyField(onStartProperty));
        _eventsContainer.Add(new PropertyField(onEndProperty));

        SetEnable(enabledToggle.value);
        return _container;
    }
    private void OnToggleEnabled(ChangeEvent<bool> evt)
    {
        bool value = evt.newValue;
        SetEnable(value);
    }
    private void SetEnable(bool value)
    {
        _mainContainer.EnableInClassList("hidden", !value);
    }
    private void EnableElement(VisualElement element, bool value)
    {
        element.EnableInClassList("hidden", !value);
    }
}
