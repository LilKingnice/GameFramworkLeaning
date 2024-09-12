using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// generic template of singleton design pattern,the moust simple way to design singleton
/// </summary>
public class SimpleSingleton
{
    private static SimpleSingleton instance;

    /// <summary>
    /// the first create a singletonBase way(property)
    /// </summary>
    public static SimpleSingleton Instance
    {
        get
        {
            if (instance ==null)
                instance = new SimpleSingleton();
            return instance;
        }
    }
    
    /// <summary>
    /// using the method way to create a singleton
    /// </summary>
    /// <returns></returns>
    public static SimpleSingleton GetInstance()
    {
        if (instance == null)
            instance = new SimpleSingleton();
        return instance;
    }

}
