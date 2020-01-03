using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.IO;
using System;

namespace RoJoStudios.Utils
{
    public static class IOUtility
    {
        /// <summary>
        /// Get the asset path in the form /Asset/
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="callerFilePath"></param>
        /// <returns></returns>
        public static string GetAssetPath(this object obj, [CallerFilePath] string callerFilePath = null)
        {
            var folder = Path.GetDirectoryName(callerFilePath);

#if UNITY_EDITOR_WIN
            folder = folder.Substring(folder.LastIndexOf(@"\Assets\", StringComparison.Ordinal) + 1);
#else
        folder = folder.Substring(folder.LastIndexOf("/Assets/", StringComparison.Ordinal) + 1);
#endif

            return folder + @"\";
        }
    }
}