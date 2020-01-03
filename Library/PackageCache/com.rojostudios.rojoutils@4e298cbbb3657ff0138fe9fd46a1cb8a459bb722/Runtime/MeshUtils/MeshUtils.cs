using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoJoStudios.MeshUtils
{
    public static class MeshUtils
    {
        public static void GetHardNormals(List<int> tris, List<Vector3> positions, List<Vector3> normals)
        {
            normals.Clear();
            for (int i = 0; i < tris.Count; i += 3)
            {
                int a = tris[i];
                int b = tris[i + 1];
                int c = tris[i + 2];

                Vector3 v1 = positions[b] - positions[a];
                Vector3 v2 = positions[c] - positions[a];
                Vector3 normal = Vector3.Cross(v1, v2);
                normals.Add(normal);
                normals.Add(normal);
                normals.Add(normal);
            }
        }



        public static void AddTriangleTris(List<int> triangles, int i1, int i2, int i3)
        {
            triangles.Add(i1);
            triangles.Add(i2);
            triangles.Add(i3);

        }

        public static void AddTrianglePositions(List<Vector3> positions, Vector3 v0, Vector3 v1, Vector3 v2)
        {
            positions.Add(v0);
            positions.Add(v1);
            positions.Add(v2);
        }

        public static void AddQuadPositions(List<Vector3> verteces, Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
        {
            verteces.Add(v0);
            verteces.Add(v1);
            verteces.Add(v2);
            verteces.Add(v3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tris"></param>
        /// <param name="a">top left corner index</param>
        /// <param name="b">top right corner index</param>
        /// <param name="c">bot left corner index</param>
        /// <param name="d">bot right corner index</param>
        public static void AddQuadTris(List<int> tris, int a, int b, int c, int d)
        {
            tris.Add(a);
            tris.Add(b);
            tris.Add(d);

            tris.Add(a);
            tris.Add(d);
            tris.Add(c);
        }

        public static void AddQuadTris(List<int> tris, int i0)
        {
            int i1 = i0 + 1;
            int i2 = i0 + 2;
            int i3 = i0 + 3;

            tris.Add(i0);
            tris.Add(i1);
            tris.Add(i3);

            tris.Add(i0);
            tris.Add(i3);
            tris.Add(i2);
        }

        public static void AddQuadUV(List<Vector2> uvs, Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3)
        {
            uvs.Add(v0);
            uvs.Add(v1);
            uvs.Add(v2);
            uvs.Add(v3);
        }

        public static void AddTriangleTris(List<int> tris, int index)
        {
            AddTriangleTris(tris, index, index + 1, index + 2);
        }

        public static void AddQuadColor(List<Color32> colors, Color32 c0, Color32 c1, Color32 c2, Color32 c3)
        {
            colors.Add(c0);
            colors.Add(c1);
            colors.Add(c2);
            colors.Add(c3);
        }

        public static void AddTriangleUV(List<Vector2> uvs, Vector3 uva, Vector3 uvb, Vector3 uvc)
        {
            uvs.Add(uva);
            uvs.Add(uvb);
            uvs.Add(uvc);
        }

        public static void AddTriangleColor(List<Color32> colors, Color32 colora, Color32 colorb, Color32 colorc)
        {
            colors.Add(colora);
            colors.Add(colorb);
            colors.Add(colorc);
        }
    }
}