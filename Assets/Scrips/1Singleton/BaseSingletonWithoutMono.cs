using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BaseSingleton without MonoBehavior
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseSingletonWithoutMono <T> where T : class,new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = new T();
            return instance;
        }
    }
    
    /// <summary>
    /// the another way
    /// </summary>
    /// <returns></returns>
    public static T GetInstance()
    {
        if (instance == null)
            instance = new T();
        return instance;
    }
}
