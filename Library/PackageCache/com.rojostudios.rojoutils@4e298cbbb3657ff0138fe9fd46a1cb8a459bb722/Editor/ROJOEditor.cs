using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

using System.Reflection.Emit;
using System.Reflection;
using System;

namespace RoJoStudios.EditorUtils
{
    public static class ROJOEditor
    {
        public static VisualElement LoadAssetTree(string path)
        {
            var assetTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            return assetTree.CloneTree();
        }

        public static Enum GenerateTempEnum(IEnumerable<string> fields, string name, out Type enumType)
        {
            AssemblyName aName = new AssemblyName("TempEnumAssembly");
            AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndCollect);
            ModuleBuilder mb = ab.DefineDynamicModule(aName.Name);
            EnumBuilder eb = mb.DefineEnum(name, TypeAttributes.Public, typeof(uint));

            int index = 0;
            foreach (var f in fields)
            {
                eb.DefineLiteral(f, index++);
            }

            enumType = eb.CreateType();
            return (Enum)Enum.ToObject(enumType, 0);
        }
    }
}