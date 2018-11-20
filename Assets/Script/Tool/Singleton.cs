using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T>: MonoBehaviour where T:MonoBehaviour {
    public static T Instance {
        get {
            lock (_lockObj)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (FindObjectsOfType(typeof(T)).Length > 1) {
                        return instance;
                    }
                    if (instance == null) {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).ToString();
                        DontDestroyOnLoad(singleton);
                    }
                }
            }
            return instance;
        }
    }
    private static readonly object _lockObj=new object();
    private static T instance;
}
