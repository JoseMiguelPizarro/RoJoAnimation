using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using RoJoStudios.EditorUtils;
using RoJoStudios.Utils;
using System;

[CustomPropertyDrawer(typeof(Scale))]
public class ScaleDrawer : BaseAnimationDrawer
{
    protected override string treePath => this.GetAssetPath() + "ScaleDrawer.uxml";
    protected override string stylePath => this.GetAssetPath() + "ScaleDrawer.uss";

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        return base.CreatePropertyGUI(property);
    }
}
