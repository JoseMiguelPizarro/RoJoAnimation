using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIBehaviourType 
{
    /// <summary>
    ///     UIButton behavior when the pointer performs a click over the button
    /// </summary>
    OnClick,

    /// <summary>
    ///     UIButton behavior when the pointer performs a double click over the button
    /// </summary>
    OnDoubleClick,

    /// <summary>
    ///     UIButton behavior when the pointer performs a long click over the button
    /// </summary>
    OnLongClick,

    /// <summary>
    ///     UIButton behavior when the pointer enters (hovers in) over the button's area
    /// </summary>
    OnPointerEnter,

    /// <summary>
    ///     UIButton behavior when the pointer exits (hovers out) the button's area (happens only after OnPointerEnter)
    /// </summary>
    OnPointerExit,

    /// <summary>
    ///     UIButton behavior when the pointer is down over the button
    /// </summary>
    OnPointerDown,

    /// <summary>
    ///     UIButton behavior when the pointer is up over the button (happens only after OnPointerDown)
    /// </summary>
    OnPointerUp,

    /// <summary>
    ///     UIButton behavior when the button gets selected
    /// </summary>
    OnSelected,

    /// <summary>
    ///     UIButton behavior when the button gets deselected
    /// </summary>
    OnDeselected,
    Free
}
