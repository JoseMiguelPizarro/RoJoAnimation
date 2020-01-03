using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace RoJoStudios.EditorUtils
{
    public static class Styles
    {
        static GUIStyle m_panelStyle;
        static GUIStyle m_buttonStyle;

        const string backgroundTexPath = "Assets/Resources/styles/panelBackground.png";
        const string selectedButtonTexPath = "Assets/Resources/styles/selectedButton.png";

        static GUIStyleState hoverButtonState;
        static GUIStyleState normalButtonState;

        public static Texture2D blackTexture;

        private static bool s_initialized;

        static Styles()
        {
            if (s_initialized) return;

            s_initialized = true;

            m_panelStyle = new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    background = IconUtility.GetIcon(backgroundTexPath),
                },
                stretchHeight = true,
                stretchWidth = true,
                padding = new RectOffset(10, 10, 10, 10),
                alignment = TextAnchor.MiddleCenter,
            };

            m_buttonStyle = new GUIStyle(EditorStyles.miniButton);
            m_buttonStyle.normal = new GUIStyleState();
            m_buttonStyle.onActive = new GUIStyleState() { background = IconUtility.GetIcon(selectedButtonTexPath) };
            m_buttonStyle.active = new GUIStyleState() { background = IconUtility.GetIcon(selectedButtonTexPath) };
            m_buttonStyle.onFocused = new GUIStyleState() { background = IconUtility.GetIcon(selectedButtonTexPath) };
            m_buttonStyle.focused = new GUIStyleState() { background = IconUtility.GetIcon(selectedButtonTexPath) };
            m_buttonStyle.onHover = new GUIStyleState() { background = IconUtility.GetIcon(selectedButtonTexPath) };
        }

        public static GUIStyle button
        {
            get => m_buttonStyle;
        }

        public static GUIStyle panel
        {
            get => m_panelStyle;
        }

        private static void InitTextures()
        {
            blackTexture = new Texture2D(32, 32);
            Color[] colors = new Color[32];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.black;
            }
            blackTexture.SetPixels(0, 0, 32, 32, colors);
        }
    }
}