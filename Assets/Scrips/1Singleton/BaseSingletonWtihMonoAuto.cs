using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Automatic load Script even create a new gameobject
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseSingletonWtihMonoAuto<T>: MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                //rename the newGameObject
                obj.name = typeof(T).ToString();
                instance=obj.AddComponent<T>();
                
                DontDestroyOnLoad(obj);
            }
 
            return instance;
        }
    }

    protected virtual void Awake()
    {
        instance = this as T;
    }
}
