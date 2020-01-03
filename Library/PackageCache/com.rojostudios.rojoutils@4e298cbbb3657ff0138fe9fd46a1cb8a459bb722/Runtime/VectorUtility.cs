using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RoJoStudios.Utils
{
    public static class VectorUtility
    {
        public static Vector2Int ToVector2Int(this Vector2 vector2)
        {
            Vector2Int v = new Vector2Int(Mathf.RoundToInt(vector2.x), Mathf.RoundToInt(vector2.y));
            return v;
        }

        /// <summary>
        /// Rotate a vector with angle in radians
        /// </summary>
        /// <param name="v"></param>
        /// <param name="angle">angle in radians</param>
        /// <returns></returns>
        public static Vector2Int Rotate(this Vector2Int v, float angle)
        {
            var cos = Mathf.Cos(angle);
            var sin = Mathf.Sin(angle);
            var x = v.x;
            var y = v.y;
            v.x = Mathf.RoundToInt((x * cos - y * sin));
            v.y = Mathf.RoundToInt((cos * y + sin * x));

            return v;
        }


        public static Vector2Int SquareRotate(this Vector2Int v, int angle)
        {
            int x = v.x;
            int y = v.y;

            switch (angle)
            {
                case 0:
                    break;
                case 90:
                    v.x = -y;
                    v.y = x;
                    break;
                case 180:
                    v.x = -x;
                    break;
                case 270:
                    v.x = y;
                    v.y = -x;
                    break;
                default:
                    break;
            }

            return v;
        }

        /// <summary>
        /// Rotate a vector with angle in radians
        /// </summary>
        /// <param name="v"></param>
        /// <param name="angle">angle in radians</param>
        /// <returns></returns>
        public static Vector2 Rotate(this Vector2 v, float angle)
        {
            var cos = Mathf.Cos(angle);
            var sin = Mathf.Sin(angle);
            var x = v.x;
            var y = v.y;
            v.x = (x * cos - y * sin);
            v.y = (cos * y + sin * x);

            return v;
        }


        public static Vector3 CalculateNormal(Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3 nrm = Vector3.Cross(b - a, c - a);
            return nrm;
        }
        public static bool IsGreater(this Vector3 v, Vector3 t)
        {
            if (v.z > t.z)
                return true;
            else if (v.z == t.z && v.x < t.x)
                return true;

            return false;
        }


        public static Vector3 Snap(ref this Vector3 v, int value)
        {
            v.x = Mathf.RoundToInt(v.x / value) * value;
            v.y = Mathf.RoundToInt(v.y / value) * value;
            v.z = Mathf.RoundToInt(v.z / value) * value;

            return v;
        }
        public static Vector3 RoundToNearestInt(ref this Vector3 v, bool x = true, bool y = true, bool z = true)
        {
            v.x = Mathf.RoundToInt(v.x);
            v.y = Mathf.RoundToInt(v.y);
            v.z = Mathf.RoundToInt(v.z);
            return v;
        }

        public static Vector3 RoundComponents(ref this Vector3 v, float x, float y, float z)
        {
            v.x = Mathf.RoundToInt(v.x / x) * x;
            v.y = Mathf.RoundToInt(v.y / y) * y;
            v.z = Mathf.RoundToInt(v.z / z) * z;

            return v;
        }


        public static Vector3 Floor(ref this Vector3 v)
        {
            v.x = Mathf.Floor(v.x);
            v.y = Mathf.Floor(v.y);
            v.z = Mathf.Floor(v.z);

            return v;
        }

        public static Vector3 ToInt(ref this Vector3 v)
        {
            v.x = (int)v.x;
            v.y = (int)v.y;
            v.z = (int)v.z;

            return v;
        }

        public static Vector3[] SortVerteces(this Vector3[] verteces)
        {
            for (int i = 0; i < verteces.Length; i++)
            {
                var v1 = verteces[i];
                for (int j = i + 1; j < verteces.Length; j++)
                {
                    var v2 = verteces[j];
                    if (!v1.IsGreater(v2))
                    {
                        verteces[i] = verteces[j];
                        verteces[j] = v1;
                        v1 = v2;
                    }
                }
            }

            return verteces;
        }


        public static Vector2Int ToGridSpace(this Vector3 coords, Vector2 resolution, Vector2 topLeftCorner)
        {
            //if ((int)resolution.x % 2 != 0 || (int)resolution.y % 2 != 0)
            //{
            //    throw new InvalidOperationException("Grid size must be even");
            //}
            Vector2Int coord2d = new Vector2Int((int)(coords.x), (int)(coords.z));

            coord2d.x = Mathf.RoundToInt((coords.x - topLeftCorner.x));
            coord2d.y = Mathf.RoundToInt((topLeftCorner.y - coords.z));

            coord2d.x = Mathf.Clamp(coord2d.x, 0, (int)resolution.x - 1);
            coord2d.y = Mathf.Clamp(coord2d.y, 0, (int)resolution.y - 1);

            //THIS WORKS ONLY IF RESOLUTION IS EVEN!

            return coord2d;
        }

        public static Vector2Int ToGridSpace(this Vector3 coords, Vector2 resolution)
        {

            if ((int)resolution.x % 2 != 0 || (int)resolution.y % 2 != 0)
            {
                throw new InvalidOperationException("Grid size must be even");
            }

            Vector2Int coord2d = new Vector2Int((int)(coords.x - .5f), (int)(coords.z + .5f));
            coord2d.x = (int)(coord2d.x + resolution.x / 2);
            coord2d.y = (int)(resolution.y - coord2d.y - resolution.y / 2);

            coord2d.x = Mathf.Clamp(coord2d.x, 0, (int)resolution.x - 1);
            coord2d.y = Mathf.Clamp(coord2d.y, 0, (int)resolution.y - 1);

            //THIS WORKS ONLY IF RESOLUTION IS EVEN!

            return coord2d;
        }
    }
}