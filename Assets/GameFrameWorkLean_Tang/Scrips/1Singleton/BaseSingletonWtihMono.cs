using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BaseSingleton with MonoBehavior
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseSingletonWtihMono<T>: MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    protected virtual void Awake()
    {
        instance = this as T;
    }
}
