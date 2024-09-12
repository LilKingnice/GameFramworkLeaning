using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// BaseSingleton without MonoBehaviour
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseSingletonWithoutMono<T> where T : class //,new()
{
    private static T instance;

    public static readonly object lockObj;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // instance = new T();
                Type type = typeof(T);
                ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    Type.EmptyTypes,
                    null);
                if (info != null)
                {
                    instance = info.Invoke(null) as T;
                }
                else
                {
                    Debug.LogError("没有找到对应无参构造函数！需要显示创建无参构造函数！");
                }
            }

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
        {
            // instance = new T();
            Type type = typeof(T);
            ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                Type.EmptyTypes,
                null);
            if (info != null)
            {
                instance = info.Invoke(null) as T;
            }
            else
            {
                Debug.LogError("没有找到对应无参构造函数！需要显示创建无参构造函数！");
            }
        }

        return instance;
    }
}

#region 可能存在的问题

/*
 * 1.构造函数的问题：在外部可以被new，但是单例具有唯一性只能存在一个。
 * --解决方法：
 * 在继承类中显式实现一个私有的构造函数
 * 同时使用反射去获取到子类中的私有构造函数
 *
 *2.切换场景可能会重复出现多个单例对象
 * --解决方法：
 * 判断instance为空就Destroy
 *
 * 3.单个物体上被挂载多个单例脚本
 *--解决方法：
 * 添加特性[DisallowMultipleComponent]
 * (只能检测到添加特性后的重复添加，并且只能检测当前对象上不能重复添加同一个脚本，无法做到全场景检测)
 *
 * 4.多线程问题：当一个线程同时访问管理器时，可能会出现共享资源的安全访问问题。
 *--解决方法：
 * 利用lock关键字
 * 通常需要保护的是不继承MonoBehavior的类，因为在这个类中不需要通常
 *
 *
 */

#endregion