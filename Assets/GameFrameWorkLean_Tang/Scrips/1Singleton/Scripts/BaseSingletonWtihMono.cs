using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BaseSingleton with MonoBehaviour
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseSingletonWtihMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance => instance;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this);
    }
}