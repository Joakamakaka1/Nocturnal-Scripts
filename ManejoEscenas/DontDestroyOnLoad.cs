using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DontDestroyOnLoadManager
{
    private static List<GameObject> dontDestroyOnLoadObjects = new List<GameObject>();

    public static void Register(GameObject obj)
    {
        dontDestroyOnLoadObjects.Add(obj);
    }

    public static void ClearDontDestroyOnLoadObjects()
    {
        foreach (var obj in dontDestroyOnLoadObjects)
        {
            Object.Destroy(obj);
        }
        dontDestroyOnLoadObjects.Clear();
    }
}
