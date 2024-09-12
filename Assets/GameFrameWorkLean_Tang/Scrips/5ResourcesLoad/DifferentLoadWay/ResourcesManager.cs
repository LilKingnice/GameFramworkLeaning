//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class ResInfoBase
{
    //reference counter
    public int refCount;
}

/// <summary>
/// Used for store resources info/delegate info/coroutine info
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResInfo<T> : ResInfoBase
{
    //Asset
    public T asset;
    //Mainly used for Asynchronous loading after 
    //the end of the transfer of Resources to the external delegate function
    public UnityAction<T> callBack;

    //Used to store startup coroutines
    public Coroutine currentCoroutine;

    //Is it needed remove (Tag)[For some resources that occupy relatively large]
    public bool isDel;



    public void AddCount()
    {
        ++refCount;
    }
    public void SubCount()
    {
        --refCount;
        if (refCount < 0)
            Debug.LogError("资源引用计数器小于零!!!检查使用和卸载数量是否配对!!!!");
    }
}

/// <summary>
/// Resources Folder Assets Load Manager
/// </summary>
public class ResourcesManager : BaseSingletonWithoutMono<ResourcesManager>
{
    private ResourcesManager()
    {
    }

    //Used to store resources that have been loaded or loading
    Dictionary<string, ResInfoBase> ResDic = new Dictionary<string, ResInfoBase>();


    /// <summary>
    /// SyncLoading
    /// </summary>
    /// <param name="path"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T SyncLoad<T>(string path) where T : Object
    {
        ResInfo<T> info;
        string resName = path + "_" + typeof(T).Name;
        if (!ResDic.ContainsKey(resName))
        {
            T res = Resources.Load<T>(path);
            info = new ResInfo<T>();

            info.AddCount();
            info.asset = res;
            ResDic.Add(resName, info);
            return info.asset;
        }
        else
        {
            //Retrieve records stored in the dictionary container.
            info = ResDic[resName] as ResInfo<T>;
            info.AddCount();
            if (info.asset == null)
            {
                //Stop the coroutine function for asynchronous loading 
                //and instead load resources using synchronous loading method directly.
                BaseMono.Instance.StopCoroutine(info.currentCoroutine);
                T res = Resources.Load<T>(path);

                info.asset = res;

                info.callBack?.Invoke(res);
                info.callBack = null;
                info.currentCoroutine = null;
                Debug.Log("同步加载，新的加载");
                return info.asset;

            }
            else
            {
                Debug.Log("同步加载，读取已经加载好的资源");
                return info.asset;
            }
        }
    }

    #region AsyncLoading

    public void AsyncLoad<T>(string path, UnityAction<T> callBack) where T : Object
    {
        //recording the current subClass info
        ResInfo<T> info;
        //Define a Unique ID of resources,
        //Because it is necessary to prevent some resources with the same name
        string resName = path + "_" + typeof(T).Name;

        //There is no record of current resources in the Dictionary(ResDic).
        if (!ResDic.ContainsKey(resName))
        {
            info = new ResInfo<T>();

            info.AddCount();//
            ResDic.Add(resName, info);

            //Start the coroutine and record its reference to use for stopping it later
            info.currentCoroutine = BaseMono.Instance.StartCoroutine(IE_AsyncLoad<T>(path));

            //Record the incoming delegate, and use it after loading done.
            info.callBack += callBack;

            Debug.Log("异步加载，新的加载");
        }
        else
        {
            Debug.Log("异步加载，读取已经加载好的资源");
            info = ResDic[resName] as ResInfo<T>;

            info.AddCount();
            //In the event that its value is null, it signifies that the data is still being asynchronously loaded.
            if (info.asset == null)
                info.callBack += callBack;
            else
                callBack?.Invoke(info.asset);
        }
    }

    /// <summary>
    /// Asynchronously loading resources by using generic
    /// </summary>
    /// <param name="path">path</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IEnumerator IE_AsyncLoad<T>(string path) where T : Object
    {
        ResourceRequest rq = Resources.LoadAsync<GameObject>(path);
        Debug.Log("异步正在加载。。。");
        yield return rq;
        string resName = path + "_" + typeof(T).Name;
        if (ResDic.ContainsKey(resName))
        {
            Debug.Log("异步加载到了资源！");

            ResInfo<T> resInfo = ResDic[resName] as ResInfo<T>;
            //recording the recources
            resInfo.asset = rq.asset as T;

            if (resInfo.refCount == 0)
            {
                UnloadAsset<T>(path, resInfo.isDel,null,false);
            }
            else
            {
                resInfo.callBack?.Invoke(resInfo.asset);

                //After loading is completed, these applications should be cleared 
                //to avoid potential memory leak issues caused by retained references.
                resInfo.callBack = null;
                resInfo.currentCoroutine = null;
            }

        }
    }


    [System.Obsolete("优先选择泛型方法!!不要type和泛型混用!!!")]
    public void AsyncLoad(string path, System.Type type, UnityAction<Object> callBack)
    {

        ResInfo<Object> info;
        string resName = path + "_" + type.Name;
        //There is no record of current resources in the Dictionary(ResDic).
        if (!ResDic.ContainsKey(resName))
        {
            info = new ResInfo<Object>();
            ResDic.Add(resName, info);

            //Start the coroutine and record its reference to use for stopping it later
            info.currentCoroutine = BaseMono.Instance.StartCoroutine(IE_AsyncLoad(path, type));

            //Record the incoming delegate, and use it after loading done.
            info.callBack += callBack;
        }
        else
        {
            info = ResDic[resName] as ResInfo<Object>;
            info.AddCount();

            //In the event that its value is null, it signifies that the data is still being asynchronously loaded.
            if (info.asset == null)
                info.callBack += callBack;
            else
                callBack?.Invoke(info.asset);
        }
    }


    /// <summary>
    /// Asynchronously loading resources by using Typeof
    /// </summary>
    /// <param name="path"></param>
    /// <param name="type">type</param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    IEnumerator IE_AsyncLoad(string path, System.Type type)
    {
        ResourceRequest rq = Resources.LoadAsync<GameObject>(path);
        yield return rq;
        string resName = path + "_" + type.Name;
        if (ResDic.ContainsKey(resName))
        {
            ResInfo<Object> resInfo = ResDic[resName] as ResInfo<Object>;
            //recording the recources
            resInfo.asset = rq.asset as Object;
            if (resInfo.refCount == 0)
            {
                UnloadAsset(path, type, resInfo.isDel,null,false);
            }
            else
            {
                resInfo.callBack?.Invoke(resInfo.asset);

                //After loading is completed, these applications should be cleared 
                //to avoid potential memory leak issues caused by retained references.
                resInfo.callBack = null;
                resInfo.currentCoroutine = null;
            }
        }
    }

    #endregion


    /// <summary>
    /// Unload the designated resources.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="isDel">Is it needed remove?Is it big or not?</param>
    /// <param name="callBack"></param>
    /// <typeparam name="T"></typeparam>
    public void UnloadAsset<T>(string path, bool isDel = true, UnityAction<T> callBack = null, bool canSub = true)
    {
        string resName = path + "_" + typeof(T).Name;
        if (ResDic.ContainsKey(resName))
        {
            ResInfo<T> resInfo = ResDic[resName] as ResInfo<T>;
            if (canSub)
                resInfo.SubCount();
            resInfo.isDel = isDel;

            //resources load is over
            if (resInfo.asset != null && resInfo.refCount == 0 && resInfo.isDel)
            {
                ResDic.Remove(resName);
                Resources.UnloadAsset(resInfo.asset as Object);
            }
            //resources is loading
            else if (resInfo.asset == null)
            {
                // BaseMono.Instance.StopCoroutine(resInfo.currentCoroutine);
                // ResDic.Remove(resName);

                //resInfo.isDel = true;

                //Remove the callback function record rather than directly unloading the resource.
                if (resInfo.callBack != null)
                    resInfo.callBack -= callBack;
            }
        }
    }

    /// <summary>
    /// for Type Override
    /// </summary>
    /// <param name="path"></param>
    /// <param name="type"></param>
    /// <param name="isDel">Is it needed remove?Is it big or not?</param>
    /// <param name="callBack"></param>
    public void UnloadAsset(string path, System.Type type, bool isDel = true, UnityAction<Object> callBack = null, bool canSub = true)
    {
        string resName = path + "_" + type.Name;
        if (ResDic.ContainsKey(resName))
        {
            ResInfo<Object> resInfo = ResDic[resName] as ResInfo<Object>;
            if (canSub)
                resInfo.SubCount();
            resInfo.isDel = isDel;

            //resources load is over
            if (resInfo.asset != null && resInfo.refCount == 0 && resInfo.isDel)
            {
                ResDic.Remove(resName);
                Resources.UnloadAsset(resInfo.asset);
            }
            //resources is loading
            else if (resInfo.asset == null)
            {
                // BaseMono.Instance.StopCoroutine(resInfo.currentCoroutine);
                // ResDic.Remove(resName);

                //resInfo.isDel = true;

                if (resInfo.callBack != null)
                    resInfo.callBack -= callBack;
            }
        }
    }



    /// <summary>
    /// Asynchronously uninstalls unused resources
    /// This method is typically used when transitioning between Scenes, 
    /// working in conjunction with the game's GC
    /// </summary>
    /// <param name="callBack">delegate function</param>
    public void UnloadUnusedAssets(UnityAction callBack)
    {
        BaseMono.Instance.StartCoroutine(IE_UnloadUnusedAssets(callBack));
    }

    IEnumerator IE_UnloadUnusedAssets(UnityAction callBack)
    {

        List<string> list = new List<string>();
        foreach (string path in ResDic.Keys)
        {
            if (ResDic[path].refCount == 0)
            {
                list.Add(path);
            }
        }

        foreach (string path in list)
        {
            ResDic.Remove(path);
        }
        AsyncOperation ao = Resources.UnloadUnusedAssets();
        yield return ao;

        callBack();//call other function when it's "Unloading Done"
    }


    /// <summary>
    /// This method also can be used to clear memory when transitioning between Scenes
    /// </summary>
    /// <param name="callBack"></param>
    public void ClearDic(UnityAction callBack)
    {
        BaseMono.Instance.StartCoroutine(IE_ClearDic(callBack));
    }

    IEnumerator IE_ClearDic(UnityAction callBack)
    {
        ResDic.Clear();
        AsyncOperation ao = Resources.UnloadUnusedAssets();
        yield return ao;
        callBack();//call other function when it's "Unloading Done"
    }



    /// <summary>
    /// Get Any Refrence Count
    /// </summary>
    /// <param name="path"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public int GetRefCount<T>(string path)
    {
        string resName = path + "_" + typeof(T).Name;
        if (ResDic.ContainsKey(resName))
        {
            return (ResDic[resName] as ResInfo<T>).refCount;
        }
        return 0;
    }
}
