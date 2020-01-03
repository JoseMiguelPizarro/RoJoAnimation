using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace RoJoStudios.Utils
{
    public static class InstanceProvider
    {
        private static Dictionary<Type, List<Type>> typeLookup = new Dictionary<Type, List<Type>>();
        private static Dictionary<Type, object> objects = new Dictionary<Type, object>();
        private static Type[] types;

        static InstanceProvider()
        {
            GetTypes();
        }

        private static void GetTypes()
        {
            types = GetAllTypes(AppDomain.CurrentDomain).ToArray();
        }

        public static List<T> GetInstancesOfSubClass<T>() where T : class
        {
            typeLookup.Clear();
            objects.Clear();
            GetTypes();

            List<T> instances = new List<T>();
            List<Type> typesList;
            bool exist = typeLookup.TryGetValue(typeof(T), out typesList);
            if (exist)
            {
                foreach (var t in typesList)
                {
                    instances.Add(objects[t] as T);
                }
            }
            else
            {
                instances = CreateInstancesOfSubClass<T>();
            }

            return instances;
        }

        public static List<Type> GetScriptableObjectSubclassTypes<T>() where T : ScriptableObject
        {
            List<Type> typesList = new List<Type>();
            GetTypes();
            foreach (var type in types)
            {
                if (type.IsClass && !type.IsAbstract)
                {
                    if (type.IsSubclassOf(typeof(T)))
                    {
                        typesList.Add(type);
                    }
                }
            }

            return typesList;
        }

        private static List<T> CreateInstancesOfSubClass<T>() where T : class
        {
            List<T> instances = new List<T>();
            List<Type> typesList = new List<Type>();
            foreach (var type in types)
            {
                if (type.IsClass && !type.IsAbstract)
                {
                    if (type.IsSubclassOf(typeof(T)))
                    {
                        var t = Activator.CreateInstance(type) as T;
                        objects.Add(type, t);
                        typesList.Add(type);
                        instances.Add(t);
                    }
                }
            }
            typeLookup.Add(typeof(T), typesList);
            return instances;
        }


        private static IEnumerable<Type> GetAllTypes(AppDomain domain)
        {
            foreach (var assembly in domain.GetAssemblies())
            {
                Type[] types = { };

                try
                {
                    types = assembly.GetTypes();
                }
                catch (Exception)
                {

                }

                foreach (var type in types)
                {
                    yield return type;
                }
            }
        }
    }

}
