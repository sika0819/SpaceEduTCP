using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T>where T:new() {
    public static T Instance {
        get {
            if (instance == null) {
                lock (_lockObj)
                {
                    if (instance == null) { 
                        instance = new T();
                    }
                }
            }
            return instance;
        }
    }
    private static readonly object _lockObj;
    private static T instance;
    static Singleton()
    {
        _lockObj = new object();
    }
}
