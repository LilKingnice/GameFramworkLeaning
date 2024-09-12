using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public enum LoadingType
{
    HTTP,
    FTP,
    FILE
}

/// <summary>
/// Using UnityWebRequest Way to Load resources
/// </summary>
public class UWQResManager : BaseSingletonWithoutMono<UWQResManager>
{
    private UWQResManager()
    {
        
    }

    public void LoadResource<T>(string url, UnityAction<T> callBack, UnityAction failedCallBack, LoadingType loadingType) where T : class
    {
        // BaseMono.Instance.StartCoroutine(IE_LoadResource(url, callBack, failedCallBack));

        switch (loadingType)
        {
            case LoadingType.HTTP:
                Debug.Log("CurrentLoadingType:" + loadingType);
                BaseMono.Instance.StartCoroutine(IE_LoadResource(url, callBack, failedCallBack));

                break;
            case LoadingType.FTP:
                Debug.Log("CurrentLoadingType:" + loadingType);
                BaseMono.Instance.StartCoroutine(IE_LoadResource("ftp://" + url, callBack, failedCallBack));
                break;
            case LoadingType.FILE:
                Debug.Log("CurrentLoadingType:" + loadingType);
                BaseMono.Instance.StartCoroutine(IE_LoadResource("file://" + url, callBack, failedCallBack));
                break;
            default:
                break;
        }
    }
    IEnumerator IE_LoadResource<T>(string url, UnityAction<T> callBack, UnityAction failedCallBack) where T : class
    {
        Type type = typeof(T);
        UnityWebRequest req = null;

        if (type == typeof(string) || type == typeof(byte[]))
            req = UnityWebRequest.Get(url);
        else if (type == typeof(Texture))
            req = UnityWebRequestTexture.GetTexture(url);
        else if (type == typeof(AssetBundle))
            req = UnityWebRequestAssetBundle.GetAssetBundle(url);
        else
        {
            failedCallBack?.Invoke();
            yield break;
        }
        yield return req.SendWebRequest();


        if (req.result == UnityWebRequest.Result.Success)
        {
            //return text content
            if (type == typeof(string))
                callBack?.Invoke(req.downloadHandler.text as T);
            //return byte[]
            else if (type == typeof(byte[]))
                callBack?.Invoke(req.downloadHandler.data as T);
            //return image(texture)
            else if (type == typeof(Texture))
                callBack?.Invoke(DownloadHandlerTexture.GetContent(req) as T);
            //return ABpackage
            else if (type == typeof(AssetBundle))
                callBack?.Invoke(DownloadHandlerAssetBundle.GetContent(req) as T);
        }
        else
        {
            failedCallBack?.Invoke();
        }

        req.Dispose();//release
    }
}
