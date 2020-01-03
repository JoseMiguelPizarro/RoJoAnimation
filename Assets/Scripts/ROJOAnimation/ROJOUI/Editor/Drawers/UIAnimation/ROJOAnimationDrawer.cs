using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(ROJOAnimation))]
public class ROJOAnimationDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement container = new VisualElement();

        var moveProperty = property.FindPropertyRelative("Move");
        var rotationProperty = property.FindPropertyRelative("Rotate");
        var scaleProperty = property.FindPropertyRelative("Scale");
        var materialProperty = property.FindPropertyRelative("animatedProperties");
        var animationTypeProperty = property.FindPropertyRelative("AnimationType");

        PropertyField moveField = new PropertyField(moveProperty);
        PropertyField rotateField = new PropertyField(rotationProperty);
        PropertyField scaleField = new PropertyField(scaleProperty);
        PropertyField animatedProperties = new PropertyField(materialProperty);
        PropertyField animationTypeField = new PropertyField(animationTypeProperty);

        container.Add(animationTypeField);
        container.Add(moveField);
        container.Add(rotateField);
        container.Add(scaleField);
        container.Add(animatedProperties);

        return container;
    }
}
