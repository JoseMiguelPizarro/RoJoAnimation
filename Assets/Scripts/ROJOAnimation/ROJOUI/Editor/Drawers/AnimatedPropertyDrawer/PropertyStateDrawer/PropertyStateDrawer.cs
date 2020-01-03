using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using RoJoStudios.Utils;
using RoJoStudios.EditorUtils;
using System;

[CustomPropertyDrawer(typeof(PropertyState))]
public class PropertyStateDrawer : PropertyDrawer
{
    private const string TREE_NAME = "PropertyStateDrawer.uxml";
    private const string STYLE_SHEET = "PropertyStateDrawer.uss";
    private GameObject _targetObject;
    private Shader _materialShader;
    private DropdownList _shaderPropertiesField;
    private string[] _propertiesNames;
    VisualElement _mainContainer;
    SerializedProperty _propertyName;
    Enum _enumValue;
    SerializedProperty _property;
    SerializedProperty _propertyTypeProperty;
    SerializedProperty _valueProperty;
    SerializedProperty _colorProperty;
    private VisualElement _valueContainer;
    private VisualElement _valueElement;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        ///
        _property = property;
        _valueProperty = property.FindPropertyRelative("floatValue");
        _colorProperty = property.FindPropertyRelative("colorValue");

        _propertyTypeProperty = property.FindPropertyRelative("propertyType");
        var treePath = this.GetAssetPath() + TREE_NAME;
        var styleSheetPath = this.GetAssetPath() + STYLE_SHEET;
        _mainContainer = ROJOEditor.LoadAssetTree(treePath);
        _mainContainer.name = _mainContainer.GetHashCode().ToString();
        _mainContainer.LoadStyleSheet(styleSheetPath);
        _propertyName = property.FindPropertyRelative("propertyName");

        var component = property.serializedObject.targetObject as MonoBehaviour;
        _targetObject = component.gameObject;

        var typeField = _mainContainer.Q<EnumField>(name: "typeField");
        typeField.Init(AnimationType.Punch);
        typeField.BindProperty(property.FindPropertyRelative("AnimationType"));

        _shaderPropertiesField = _mainContainer.Q<DropdownList>(name: "shaderPropertiesField");
        _valueContainer = _mainContainer.Q<VisualElement>(name: "valueContainer");
        var durationField = _mainContainer.Q<FloatField>(name: "duration");
        durationField.BindProperty(property.FindPropertyRelative("duration"));
        GetShader(_propertyName.stringValue);
        InitShaderPropertiesField(_enumValue);
        SetShaderProperty(_shaderPropertiesField.value);

        return _mainContainer;
    }

    private Material GetMaterial()
    {
        Material material;
        var renderer = _targetObject.GetComponent<Renderer>();
        if (renderer == null)
        {
            var image = _targetObject.GetComponent<UnityEngine.UI.Image>();
            material = image.material;
        }
        else
        {
            material = renderer.sharedMaterial;
        }
        return material;
    }

    private void GetShader(string currentPropertyName)
    {
        var material = GetMaterial();
        _materialShader = material.shader;
        int propertyNumber = ShaderUtil.GetPropertyCount(_materialShader);
        _propertiesNames = new string[propertyNumber];

        for (int i = 0; i < propertyNumber; i++)
        {
            _propertiesNames[i] = ShaderUtil.GetPropertyName(_materialShader, i);
        }

        Type enumType;
        Enum tempEnum = ROJOEditor.GenerateTempEnum(_propertiesNames, "ShaderProperties", out enumType);
        int propertyIndex = _propertiesNames.IndexOf(currentPropertyName);

        if (propertyIndex != -1)
            _enumValue = Enum.Parse(enumType, _propertiesNames[propertyIndex]) as Enum;
        else
            _enumValue = Enum.Parse(enumType, _propertiesNames[0]) as Enum;
    }

    private void InitShaderPropertiesField(Enum tempEnum)
    {
        _shaderPropertiesField.Init(tempEnum);
        _shaderPropertiesField.RegisterValueChangedCallback(OnChangeProperty);
    }

    private void OnChangeProperty(ChangeEvent<Enum> evt)
    {
        var value = evt.newValue;
        SetShaderProperty(value);
    }

    private void SetShaderProperty(Enum value)
    {
        int propertyIndex = _propertiesNames.IndexOf(value.ToString());
        ShaderPropertyType propertyType = _materialShader.GetPropertyType(propertyIndex);
        _propertyTypeProperty.enumValueIndex = (int)propertyType;
        _propertyName.stringValue = value.ToString();

        _propertyName.serializedObject.ApplyModifiedProperties();
        _property.serializedObject.ApplyModifiedProperties();
        SetValueElement(propertyType);
    }

    public void SetValueElement(ShaderPropertyType propertyType)
    {
        _valueElement?.RemoveFromHierarchy();
        _valueElement?.Unbind();
        switch (propertyType)
        {
            case ShaderPropertyType.Color:
                _valueElement = new ColorField();
                _valueContainer.Add(_valueElement);
                (_valueElement as ColorField).BindProperty(_colorProperty);
                break;
            case ShaderPropertyType.Vector:
                break;
            case ShaderPropertyType.Float:
                _valueElement = new FloatField();
                _valueContainer.Add(_valueElement);
                (_valueElement as FloatField).BindProperty(_valueProperty);
                break;
            case ShaderPropertyType.Range:
                _valueElement = new FloatField();
                (_valueElement as FloatField).BindProperty(_valueProperty);
                break;
            case ShaderPropertyType.Texture:
                break;
            default:
                break;
        }
    }
}
