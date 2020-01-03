using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ROJOUI/Database")]
public class ROJOUIDatabase : ScriptableObject
{
    private static ROJOUIDatabase instance;
    public static List<UIAnimation> data => instance.animations;
    public List<UIAnimation> animations;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void Save(UIAnimation animation)
    {
        instance.animations.Add(animation);
    }
}
