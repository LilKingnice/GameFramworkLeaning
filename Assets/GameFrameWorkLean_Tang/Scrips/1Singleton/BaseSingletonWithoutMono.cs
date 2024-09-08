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

#region 可能存在的问题
/*
 * 1.构造函数的问题：在外部可以被new，但是单例具有唯一性只能存在一个。
 * 2.多线程问题：当一个线程同时访问管理器时，可能会出现共享资源的安全访问问题。
 */
#endregion
