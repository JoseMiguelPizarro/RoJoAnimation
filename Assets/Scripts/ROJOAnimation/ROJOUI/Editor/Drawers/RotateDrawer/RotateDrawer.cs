using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using RoJoStudios.EditorUtils;
using RoJoStudios.Utils;
using System;

[CustomPropertyDrawer(typeof(Rotate))]
public class RotateDrawer : BaseAnimationDrawer
{
    protected override string treePath =>this.GetAssetPath()+ "RotateDrawer.uxml";
    protected override string stylePath => this.GetAssetPath()+ "RotateDrawer.uss";

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        return base.CreatePropertyGUI(property);
    }
}
