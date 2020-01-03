using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;
using System;
namespace RoJoStudios.EditorUtils
{
    public class DropdownString : BaseField<string>
    {
        public new class UxmlFactory : UxmlFactory<DropdownString, UxmlTraits> { }
        public new class UxmlTraits : BaseField<string>.UxmlTraits
        {
            UxmlStringAttributeDescription _text = new UxmlStringAttributeDescription() { name = "text" };
            UxmlIntAttributeDescription _minWidth = new UxmlIntAttributeDescription() { name = "mind-width", defaultValue = 100 };
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get
                {
                    yield return new UxmlChildElementDescription(typeof(VisualElement));
                }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {

                base.Init(ve, bag, cc);
                ((DropdownList)ve).text = _text.GetValueFromBag(bag, cc);
                ve.style.minWidth = _minWidth.GetValueFromBag(bag, cc);
            }
        }

        private string[] values;
        private TextElement _textElement;
        private VisualElement _arrowElement;

        #region class names
        public new static readonly string ussClassName = "unity-enum-field";
        public static readonly string textUssClassName = ussClassName + "__text";
        public static readonly string arrowUssClassName = ussClassName + "__arrow";
        public new static readonly string labelUssClassName = ussClassName + "__label";
        public new static readonly string inputUssClassName = ussClassName + "__input";
        #endregion class names
        public string text
        {
            get => _textElement.text;
            set { _textElement.text = value; }
        }

        private void Initialize(string defaultValue)
        {
            _textElement = new TextElement();
            _textElement.AddToClassList(textUssClassName);
            _textElement.pickingMode = PickingMode.Ignore;

            Add(_textElement);

            _arrowElement = new VisualElement();
            _arrowElement.AddToClassList(arrowUssClassName);
            _arrowElement.pickingMode = PickingMode.Ignore;
            Add(_arrowElement);

        }

        public void Init(string[] values, string value)
        {
            this.values = values;
            this.value = value;
        }


        public DropdownString() : this(label: null)
        {
        }


        public DropdownString(string label) : base(label, null)
        {
            AddToClassList(ussClassName);
            labelElement.AddToClassList(labelUssClassName);
            AddToClassList(inputUssClassName);
            Initialize(null);
        }

        protected override void ExecuteDefaultActionAtTarget(EventBase evt)
        {
            base.ExecuteDefaultActionAtTarget(evt);

            if (evt == null)
            {
                return;
            }

            var showMenu = false;

            KeyDownEvent kde = (evt as KeyDownEvent);
            if (kde != null)
            {
                if (kde.keyCode == KeyCode.Space ||
                    kde.keyCode == KeyCode.KeypadEnter ||
                    kde.keyCode == KeyCode.Return)
                {
                    showMenu = true;
                }
            }
            else if ((evt as MouseDownEvent)?.button == (int)MouseButton.LeftMouse)
            {
                var mde = (MouseDownEvent)evt;
                if (ContainsPoint(this.WorldToLocal(mde.mousePosition)))
                {
                    showMenu = true;
                }
            }

            if (showMenu)
            {
                ShowMenu();
                evt.StopPropagation();
            }
        }


        public override void SetValueWithoutNotify(string newValue)
        {
            if (rawValue != newValue)
            {
                base.SetValueWithoutNotify(newValue);

                int idx = Array.IndexOf(values, newValue);

                if (idx >= 0 & idx < values.Length)
                {
                    _textElement.text = values[idx];
                }
            }
        }

        private void ShowMenu()
        {
            var menu = new GenericMenu();
            int selectedIndex = Array.IndexOf(values, value);

            for (int i = 0; i < values.Length; i++)
            {
                bool isSelected = selectedIndex == i;
                menu.AddItem(new GUIContent(values[i]), isSelected,
                    contentview => ChangeValueFromMenu(contentview),
                    values[i]);
            }

            var menuPosition = new Vector2(layout.xMin, layout.height);
            menuPosition = this.LocalToWorld(menuPosition);
            var menuRect = new Rect(menuPosition, Vector2.zero);
            menu.DropDown(menuRect);
        }

        private void ChangeValueFromMenu(object menuItem)
        {
            value = menuItem as string;
        }
    }
}