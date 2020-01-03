using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using RoJoStudios.EditorUtils;
using RoJoStudios.Utils;
using UnityEngine.Rendering;
using System;

[CustomPropertyDrawer(typeof(Move))]
public class MoveDrawer : BaseAnimationDrawer
{
    protected override string treePath => this.GetAssetPath() + "MoveDrawer.uxml";
    protected override string stylePath => this.GetAssetPath()+"MoveDrawer.uss";
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        return base.CreatePropertyGUI(property);
    }
}
