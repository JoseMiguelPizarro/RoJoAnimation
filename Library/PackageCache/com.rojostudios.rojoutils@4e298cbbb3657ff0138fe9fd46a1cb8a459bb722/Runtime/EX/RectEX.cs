using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoJoStudios.Utils
{
    public static class RectEX
    {
        public static Rect FlipHorizontally(this Rect rect)
        {
            rect.position = new Vector2(-rect.x - rect.width, rect.y);
            return rect;
        }
    }
}