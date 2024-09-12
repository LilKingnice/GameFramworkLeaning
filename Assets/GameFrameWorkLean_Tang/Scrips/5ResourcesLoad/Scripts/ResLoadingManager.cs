using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResLoadingManager : BaseSingletonWithoutMono<ResLoadingManager>
{
    private ResLoadingManager()
    {
    }
    //private bool isDebuging = true;

/// <summary>
/// 
/// </summary>
/// <param name="callBack"></param>
/// <param name="resName">Resources Name</param>
/// <param name="abName">ABpackage Name</param>
/// <param name="isSync">is or not Async(Defualt value is true"sync")</param>
/// <param name="isDebuging"></param>
/// <typeparam name="T"></typeparam>
    public void LoadRES<T>(UnityAction<T> callBack, string resName, string abName = null, bool isSync = false,bool isDebuging = true) where T : Object
    {
        string path = null;
#if UNITY_EDITOR
        if (isDebuging)
        {
            if (abName != null)
                path = abName + "/" + resName;
            else
                path = resName;
            T res = EditorResManager.Instance.LoadEditorRes<T>(path);
            callBack?.Invoke(res as T);
        }
        else
        {
            ABmanager.Instance.LoadResAsync<T>(abName, resName, callBack, isSync);
        }
#else
            ABmanager.Instance.LoadResAsync<T>(abName,resName,callBack,isSync);
#endif
    }
}
