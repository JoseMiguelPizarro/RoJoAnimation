using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace RoJoStudios.MeshUtils
{
    public static class MeshGenerator
    {
        private static List<Vector2> currentUvs = new List<Vector2>();

        internal static int[] GetQuadIndexes(int startIndex)
        {
            int[] tris = new int[] {
                startIndex,startIndex+ 1,startIndex+ 3,
                startIndex,startIndex+ 3,startIndex+ 2};

            return tris;
        }

        public static Mesh GenerateBaseBox(IEnumerable<Vector3> positions, float baseHeight)
        {

            Mesh box = new Mesh();
            var leftPosition = positions.Min(v => v.x);
            var rightPosition = positions.Max(v => v.x);

            var topPosition = positions.Max(v => v.z);
            var bottomPosition = positions.Min(v => v.z);

            var leftPositions = positions.Where(v => v.x == leftPosition).OrderByDescending(v => v.z).ToArray();
            var bottomPositions = positions.Where(v => v.z == bottomPosition).OrderBy(v => v.x).ToArray();
            var rightPositions = positions.Where(v => v.x == rightPosition).OrderBy(v => v.z).ToArray();
            var topPositions = positions.Where(v => v.z == topPosition).OrderByDescending(v => v.x).ToArray();

            int totalPositions = leftPositions.Length + rightPositions.Length + topPositions.Length + bottomPositions.Length;

            Vector3[] meshPositions = new Vector3[totalPositions + 8]; //8 is for the base positions;
            int currentIndex = 0;

            meshPositions[currentIndex] = new Vector3(leftPosition, baseHeight, topPosition);
            currentIndex += 1;
            leftPositions.CopyTo(meshPositions, currentIndex);
            currentIndex += leftPositions.Length;
            meshPositions[currentIndex] = new Vector3(leftPosition, baseHeight, bottomPosition);
            currentIndex += 1;


            meshPositions[currentIndex] = new Vector3(leftPosition, baseHeight, bottomPosition);
            currentIndex += 1;
            bottomPositions.CopyTo(meshPositions, currentIndex);
            currentIndex += bottomPositions.Length;
            meshPositions[currentIndex] = new Vector3(rightPosition, baseHeight, bottomPosition);
            currentIndex += 1;

            meshPositions[currentIndex] = new Vector3(rightPosition, baseHeight, bottomPosition);
            currentIndex += 1;
            rightPositions.CopyTo(meshPositions, currentIndex);
            currentIndex += rightPositions.Length;
            meshPositions[currentIndex] = new Vector3(rightPosition, baseHeight, topPosition);
            currentIndex += 1;

            meshPositions[currentIndex] = new Vector3(rightPosition, baseHeight, topPosition);
            currentIndex += 1;
            topPositions.CopyTo(meshPositions, currentIndex);
            currentIndex += rightPositions.Length;
            meshPositions[currentIndex] = new Vector3(leftPosition, baseHeight, rightPosition);
            currentIndex += 1;

            int[] tris = new int[totalPositions * 3];
            int posIndex = 0;
            int triIndex = 0;

            for (int i = 0; i < leftPositions.Length; i++)
            {
                var v0 = meshPositions[posIndex + 0];
                var v1 = meshPositions[posIndex + i + 1];
                var v2 = meshPositions[posIndex + i + 2];


                AddTriangle(tris, ref triIndex, posIndex, posIndex + i + 1, posIndex + i + 2);
            }
            posIndex += 2 + leftPositions.Length;

            for (int i = 0; i < bottomPositions.Length; i++)
            {
                var v0 = meshPositions[posIndex + 0];
                var v1 = meshPositions[posIndex + i + 1];
                var v2 = meshPositions[posIndex + i + 2];


                AddTriangle(tris, ref triIndex, posIndex, posIndex + i + 1, posIndex + i + 2);
            }

            posIndex += 2 + bottomPositions.Length;

            for (int i = 0; i < rightPositions.Length; i++)
            {
                var v0 = meshPositions[posIndex + 0];
                var v1 = meshPositions[posIndex + i + 1];
                var v2 = meshPositions[posIndex + i + 2];


                AddTriangle(tris, ref triIndex, posIndex, posIndex + i + 1, posIndex + i + 2);
            }

            posIndex += 2 + rightPositions.Length;

            for (int i = 0; i < topPositions.Length; i++)
            {
                var v0 = meshPositions[posIndex + 0];
                var v1 = meshPositions[posIndex + i + 1];
                var v2 = meshPositions[posIndex + i + 2];


                AddTriangle(tris, ref triIndex, posIndex, posIndex + i + 1, posIndex + i + 2);
            }

            posIndex += 2 + topPositions.Length;



            box.vertices = meshPositions;
            box.triangles = tris;
            box.RecalculateNormals();
            return box;
        }

        internal static Vector2[] GetQuadUV()
        {
            Vector2[] uvs = new Vector2[] {
                new Vector2(0,1),
                new Vector2(1,1),
                new Vector2(0,0),
                new Vector2(1,0)
            };

            return uvs;
        }

        public static Mesh CreatePlane(int columns, int rows, float size)
        {
            Mesh mesh = new Mesh();
            int triangles = columns * rows * 2 * 3;
            int vertices = (columns + 1) * (rows + 1);
            float xoffset = -columns / 2;
            float zoffset = rows / 2;


            int[] tris = new int[triangles];
            Vector3[] verts = new Vector3[vertices];
            Color[] vertexColor = new Color[vertices];
            Vector2[] uvs = new Vector2[vertices];

            for (int i = 0; i < vertexColor.Length; i++)
            {
                vertexColor[i] = Color.white;
            }


            int triIndex = 0;

            for (int y = 0, vertexIndex = 0; y < columns + 1; y++)
            {
                for (int x = 0; x < rows + 1; x++, vertexIndex++)
                {
                    verts[vertexIndex] = new Vector3(x + xoffset, 0, zoffset - y);


                    if (x < rows && y < columns)
                    {
                        tris[triIndex] = vertexIndex;
                        tris[triIndex + 1] = vertexIndex + (columns + 1) + 1;
                        tris[triIndex + 2] = vertexIndex + (columns + 1);
                        tris[triIndex + 3] = vertexIndex + 1;
                        tris[triIndex + 4] = vertexIndex + (columns + 1) + 1;
                        tris[triIndex + 5] = vertexIndex;

                        triIndex += 6;

                    }


                }
            }

            mesh.vertices = verts;
            mesh.triangles = tris;
            mesh.colors = vertexColor;
            mesh.uv = uvs;
            mesh.RecalculateNormals();

            return mesh;
        }

        public static void SetQuad(Mesh mesh, Vector3[] verteces, int[] tris, Vector2[] uvs)
        {
            tris[0] = 0;
            tris[1] = 1;
            tris[2] = 2;
            tris[3] = 1;
            tris[4] = 3;
            tris[5] = 2;

            uvs[0] = new Vector2(0, 1);
            uvs[1] = new Vector2(1, 1);
            uvs[2] = new Vector2(0, 0);
            uvs[3] = new Vector2(1, 0);

            mesh.vertices = verteces;
            mesh.triangles = tris;
            mesh.uv = uvs;
            mesh.RecalculateNormals();
        }
        private static void AddUvs(Vector2[] uvs = null)
        {
            if (uvs == null)
            {
                currentUvs.Add(new Vector2(0, 1));
                currentUvs.Add(new Vector2(1, 1));
                currentUvs.Add(new Vector2(0, 0));
                currentUvs.Add(new Vector2(1, 0));
            }
            else
            {
                currentUvs.AddRange(uvs);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseVerteces"></param>
        /// <param name="positionOffset">The world position offset of the baseVerteces</param>
        /// <returns></returns>
        public static Mesh CreateQuad(Vector3[] baseVerteces, Vector3 positionOffset)
        {
            Mesh mesh = new Mesh();
            int[] tris = new int[6];
            int currentIndex = 0;
            int currentVertex = 0;
            Vector3[] positions = new Vector3[4];
            Vector2[] uvs = new Vector2[4];


            Vector3 v1 = baseVerteces[0] - positionOffset;
            Vector3 v2 = baseVerteces[1] - positionOffset;
            Vector3 v3 = baseVerteces[2] - positionOffset;
            Vector3 v4 = baseVerteces[3] - positionOffset;

            AddTriangle(tris, ref currentIndex, positions, ref currentVertex, uvs, v1, v2, v3, v4);
            mesh.name = "CustomQuad";
            mesh.vertices = positions;
            mesh.uv = uvs;
            mesh.triangles = tris;
            mesh.RecalculateNormals();
            return mesh;
        }

        public static Mesh CreateOpenCube(Vector3[] baseVerteces, Vector3 offset, Vector3 positionOffset)
        {
            Mesh mesh = new Mesh();
            int[] tris = new int[24];
            int currentIndex = 0;
            int currentVertex = 0;
            Vector3[] positions = new Vector3[16];
            Vector2[] uvs = new Vector2[16];


            Vector3 v1 = baseVerteces[2] + offset - positionOffset;
            Vector3 v2 = baseVerteces[3] + offset - positionOffset;
            Vector3 v3 = baseVerteces[2] - positionOffset;
            Vector3 v4 = baseVerteces[3] - positionOffset;

            AddTriangle(tris, ref currentIndex, positions, ref currentVertex, uvs, v1, v2, v3, v4);


            v1 = baseVerteces[0] + offset - positionOffset;
            v2 = baseVerteces[2] + offset - positionOffset;
            v3 = baseVerteces[0] - positionOffset;
            v4 = baseVerteces[2] - positionOffset;

            AddTriangle(tris, ref currentIndex, positions, ref currentVertex, uvs, v1, v2, v3, v4);


            v1 = baseVerteces[3] + offset - positionOffset;
            v2 = baseVerteces[1] + offset - positionOffset;
            v3 = baseVerteces[3] - positionOffset;
            v4 = baseVerteces[1] - positionOffset;

            AddTriangle(tris, ref currentIndex, positions, ref currentVertex, uvs, v1, v2, v3, v4);

            v1 = baseVerteces[1] + offset - positionOffset;
            v2 = baseVerteces[0] + offset - positionOffset;
            v3 = baseVerteces[1] - positionOffset;
            v4 = baseVerteces[0] - positionOffset;
            AddTriangle(tris, ref currentIndex, positions, ref currentVertex, uvs, v1, v2, v3, v4);

            mesh.vertices = positions;
            mesh.triangles = tris;
            mesh.uv = uvs;
            mesh.RecalculateNormals();

            return mesh;
        }

        private static void AddTriangle(int[] triangles, ref int currentIndex, Vector3[] verteces, ref int vertexIndex, Vector2[] uvs, params Vector3[] positions)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                verteces[vertexIndex + i] = positions[i];
            }

            uvs[vertexIndex] = new Vector2(0, 1);
            uvs[vertexIndex + 1] = new Vector2(1, 1);
            uvs[vertexIndex + 2] = new Vector2(0, 0);
            uvs[vertexIndex + 3] = new Vector2(1, 0);


            triangles[currentIndex] = vertexIndex;
            triangles[currentIndex + 1] = vertexIndex + 1;
            triangles[currentIndex + 2] = vertexIndex + 3;

            triangles[currentIndex + 3] = vertexIndex;
            triangles[currentIndex + 4] = vertexIndex + 3;
            triangles[currentIndex + 5] = vertexIndex + 2;

            currentIndex += 6;
            vertexIndex += 4;

        }

        private static void AddTriangle(int[] triangles, ref int currentIndex, params int[] vertexIndex)
        {
            triangles[currentIndex] = vertexIndex[0];
            triangles[currentIndex + 1] = vertexIndex[1];
            triangles[currentIndex + 2] = vertexIndex[2];


            currentIndex += 3;
        }

        public static Mesh CreatePlaneWithRepeatedVertices(int columns, int rows, float size)
        {
            Mesh mesh = new Mesh();
            int triangles = columns * rows * 2 * 3;
            int vertices = (columns) * (rows) * 4;
            float xoffset = -columns / 2;
            float zoffset = rows / 2;

            Vector2[] uvs = new Vector2[vertices];
            int[] tris = new int[triangles];
            Vector3[] verts = new Vector3[vertices];
            int triIndex = 0;
            int uvIndex = 0;

            for (int y = 0, vertexIndex = 0; y < columns + 1; y++)
            {
                for (int x = 0; x < rows + 1; x++)
                {
                    if (x < rows && y < columns)
                    {
                        verts[vertexIndex] = new Vector3(x + xoffset, 0, zoffset - y);
                        verts[vertexIndex + 1] = new Vector3(x + 1 + xoffset, 0, zoffset - y);
                        verts[vertexIndex + 2] = new Vector3(x + xoffset, 0, zoffset - y - 1);
                        verts[vertexIndex + 3] = new Vector3(x + 1 + xoffset, 0, zoffset - y - 1);


                        uvs[uvIndex] = new Vector2(0, 1);
                        uvs[uvIndex + 1] = new Vector2(1, 1);
                        uvs[uvIndex + 2] = new Vector2(0, 0);
                        uvs[uvIndex + 3] = new Vector2(1, 0);

                        tris[triIndex] = vertexIndex;
                        tris[triIndex + 1] = vertexIndex + 1;
                        tris[triIndex + 2] = vertexIndex + 3;
                        tris[triIndex + 3] = vertexIndex;
                        tris[triIndex + 4] = vertexIndex + 3;
                        tris[triIndex + 5] = vertexIndex + 2;

                        triIndex += 6;
                        vertexIndex += 4;
                        uvIndex += 4;
                    }
                }
            }

            mesh.vertices = verts;
            mesh.triangles = tris;
            mesh.uv = uvs;
            mesh.RecalculateNormals();

            return mesh;
        }
    }
}