using System.Collections.Generic;
using UnityEngine.UIElements;
using System;
using System.Linq;
namespace RoJoStudios.EditorUtils
{
    public class ToggleView : VisualElement
    {
        private const string STYLE_SHEET_NAME = "Packages/com.rojostudios.rojoutils/Editor/UIElements/ROJOToggleView/ToggleView.uss";
        private VisualElement currentView;
        public new class UxmlFactory : UxmlFactory<ToggleView, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
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
            }
        }

        public void Init()
        {
            if (childCount == 0) return;

            var children = Children().ToArray();
            VisualElement btnContainer = new VisualElement() { style = { flexDirection = FlexDirection.Row } };
            VisualElement viewContainer = new VisualElement();

            for (int i = 0; i < children.Length; i++)
            {
                VisualElement c = children[i];
                Button btn = new Button() { text = c.name };
                btnContainer.Add(btn);
                btn.clicked += () => SetView(c);
                c.EnableInClassList("hidden", true);
                viewContainer.Add(c);
            }
            btnContainer.AddToClassList("buttons-container");
            Add(btnContainer);
            Add(viewContainer);

            SetView(children[0]);
        }

        public void SetView(VisualElement view)
        {
            currentView?.EnableInClassList("hidden", true);
            view.EnableInClassList("hidden", false);
            currentView = view;
        }

        private Type _enumType;
        private VisualElement _arrowElement;
        public ToggleView()
        {
            string styleSheetPath = STYLE_SHEET_NAME;
            this.LoadStyleSheet(styleSheetPath);
        }

    }
}
