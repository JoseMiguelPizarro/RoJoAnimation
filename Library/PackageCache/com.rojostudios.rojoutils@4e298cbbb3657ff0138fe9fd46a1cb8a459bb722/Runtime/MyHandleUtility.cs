using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEditor;

namespace RoJoStudios.Utils
{
    public static class MyHandleUtility
    {
#if UNITY_EDITOR
        public static Vector3 GUIPosToWorldPos(Vector3 guiPos, SceneView view)
        {
            guiPos.y = (guiPos.y - view.position.height) * -1;
            return guiPos;
        }
#endif


        /// <summary>
        /// Find a triangle intersected by InRay on InMesh.  InRay is in world space.
        /// Returns the index in mesh.faces of the hit face, or -1.  Optionally can ignore backfaces.
        /// </summary>
        /// <param name="worldRay"></param>
        /// <param name="mesh"></param>
        /// <param name="hit"></param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public static bool FaceRaycast(Ray worldRay, Transform objectTransform, List<Vector3[]> faces, out RaycastHit hit, HashSet<Vector3[]> ignore = null)
        {
            return FaceRaycast(worldRay, objectTransform, faces, out hit, Mathf.Infinity, ignore);
        }

        /// <summary>
        /// Find the nearest face intersected by InWorldRay on this pb_Object.
        /// </summary>
        /// <param name="worldRay">A ray in world space.</param>
        /// <param name="mesh">The ProBuilder object to raycast against.</param>
        /// <param name="hit">If the mesh was intersected, hit contains information about the intersect point in local coordinate space.</param>
        /// <param name="distance">The distance from the ray origin to the intersection point.</param>
        /// <param name="cullingMode">Which sides of a face are culled when hit testing. Default is back faces are culled.</param>
        /// <param name="ignore">Optional collection of faces to ignore when raycasting.</param>
        /// <returns>True if the ray intersects with the mesh, false if not.</returns>
        internal static bool FaceRaycast(Ray worldRay, Transform objectTransform, List<Vector3[]> faces, out RaycastHit hit, float distance, HashSet<Vector3[]> ignore = null)
        {
            // Transform ray into model space
            worldRay.origin -= objectTransform.position; // Why doesn't worldToLocalMatrix apply translation?
            worldRay.origin = objectTransform.worldToLocalMatrix * worldRay.origin;
            worldRay.direction = objectTransform.worldToLocalMatrix * worldRay.direction;


            float OutHitPoint = Mathf.Infinity;
            int OutHitFace = -1;
            Vector3 OutNrm = Vector3.zero;
            Vector3[] positions = faces.SelectMany(v => v).ToArray();

            // Iterate faces, testing for nearest hit to ray origin. Optionally ignores backfaces.
            for (int i = 0, fc = faces.Count; i < fc; ++i)
            {
                if (ignore != null && ignore.Contains(faces[i]))
                    continue;


                for (int j = 0; j < 2; j++)
                {
                    Vector3 a = faces[i][1];
                    Vector3 b = faces[i][0];
                    Vector3 c = faces[i][2];

                    if (j == 1)
                    {
                        if (faces[i].Length > 3)
                        {
                            a = faces[i][2];
                            b = faces[i][0];
                            c = faces[i][3];
                        }
                    }
                    Vector3 nrm = Vector3.Cross(b - a, c - a);
                    nrm = GetFaceNormal(faces[i]);
                    float dot = Vector3.Dot(worldRay.direction, nrm);

                    bool skip = false;

                    if (dot > 0f) skip = true;


                    var dist = 0f;

                    Vector3 point;
                    if (!skip && Math.RayIntersectsTriangle(worldRay, a, b, c, out dist, out point))
                    {
                        if (dist > OutHitPoint || dist > distance)
                            continue;

                        OutNrm = nrm;
                        OutHitFace = i;
                        OutHitPoint = dist;
                    }
                }
            }

            hit = new RaycastHit(OutHitPoint,
                    worldRay.GetPoint(OutHitPoint),
                    OutNrm,
                    OutHitFace);

            return OutHitFace > -1;
        }

        public static Vector3 GetFaceNormal(Vector3[] positions)
        {
            if (positions.Length < 3)
            {
                throw new ArgumentException("face has less than 3 verteces");
            }

            try
            {
                Vector3 a = positions[0];
                Vector3 b = positions[1];
                Vector3 c = positions[2];


                Vector3 nrm1 = Vector3.Cross(b - a, c - a);
                return (nrm1).normalized;

            }
            catch (System.Exception)
            {
                Debug.Log("error perrito");
                throw new System.Exception();
            }

        }

    }
}