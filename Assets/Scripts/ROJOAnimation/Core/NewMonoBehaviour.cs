using UnityEngine;
using System.Collections;
using System;
public class Coroutiner : MonoBehaviour
{
    private static Coroutiner Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("Coroutiner");
                _instance = obj.AddComponent<Coroutiner>();
            }

            return _instance;
        }
    }
    private static Coroutiner _instance;

    public static Coroutine Wait(WaitForSeconds wait, Action OnComplete = null)
    {
        return Instance.StartCoroutine(WaitCoroutine(wait, OnComplete));
    }

    public static void KillCoroutine(Coroutine c)
    {
        Instance.StopCoroutine(c);
    }


    private static IEnumerator WaitCoroutine(WaitForSeconds wait, Action OnComplete = null)
    {
        float startTime = Time.time;
        yield return wait;
        OnComplete?.Invoke();
    }
}
