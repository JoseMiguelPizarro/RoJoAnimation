using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RoJoStudios.EditorUtils
{
    public static class IconUtility
    {
        public static Texture2D GetIcon(string path)
        {
            var icon = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            return icon;
        }
    }
}