using System;
using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum WWWLoadingType
{
    HTTP,
    FTP,
    FILE
}

/// <summary>
/// A LoadingManager write by myself。
/// Using WWW API to loading resource
/// </summary>
public class WWWLoadingManager : BaseSingletonWithoutMono<WWWLoadingManager>
{

    /// <summary>
    /// using http obtain resources
    /// </summary>
    /// <param name="url">the url</param>
    /// <param name="res">rawimage type resource</param>
    /// <param name="loadingType">insert different loadingtype</param>
    public void WWW_GetImage(string url, RawImage res, WWWLoadingType loadingType)
    {
        switch (loadingType)
        {
            case WWWLoadingType.HTTP:
                Debug.Log("CurrentLoadingType:" + loadingType);
                BaseMono.Instance.StartCoroutine(IE_Get(url, res));
                break;
            case WWWLoadingType.FTP:
                Debug.Log("CurrentLoadingType:" + loadingType);
                BaseMono.Instance.StartCoroutine(IE_Get("ftp://" + url, res));
                break;
            case WWWLoadingType.FILE:
                Debug.Log("CurrentLoadingType:" + loadingType);
                BaseMono.Instance.StartCoroutine(IE_Get("file://" + url, res));
                break;
            default:
                break;
        }
        //BaseMono.Instance.StartCoroutine(IE_HttpGet(url, res));
    }


    IEnumerator IE_Get(string url, RawImage res)//TODO 这里写死了，只能传入图片类型读取的也是图片类型
    {
        WWW mywww = new WWW(url);

        while (!mywww.isDone)
        {
            Debug.Log("下载进度1:" + mywww.progress);
            Debug.Log("已经下载了的字节数量1:" + mywww.bytesDownloaded);
            yield return mywww;
        }

        if (mywww.error == null)
        {
            Debug.Log("下载进度2:" + mywww.progress);
            Debug.Log("已经下载了的字节数量2:" + mywww.bytesDownloaded);
            Debug.Log("当前获取到的类型是：" + mywww.GetType());
            res.texture = mywww.texture;
        }
        else
            Debug.Log(mywww.error);
    }


    /// <summary>
    /// Different path formats are assigned according to different types
    /// </summary>
    /// <param name="url">HTTP:full url,FTP/FILE:half url</param>
    /// <param name="loadingType"></param>
    public void UnityWebReq_LoadText(string url, WWWLoadingType loadingType)
    {
        switch (loadingType)
        {
            case WWWLoadingType.HTTP:
                Debug.Log("CurrentLoadingType:" + loadingType);
                BaseMono.Instance.StartCoroutine(IE_LoadText(url));
                break;
            case WWWLoadingType.FTP:
                Debug.Log("CurrentLoadingType:" + loadingType);
                BaseMono.Instance.StartCoroutine(IE_LoadText("ftp://" + url));
                break;
            case WWWLoadingType.FILE:
                Debug.Log("CurrentLoadingType:" + loadingType);
                //ssdafasdfa
                BaseMono.Instance.StartCoroutine(IE_LoadText("file://" + url));
                break;
            default:
                break;
        }
    }


    IEnumerator IE_LoadText(string url)//TODO 这里写死了，只能获取text的文件，应该需要用到泛型或者type来适应传入多种类型文件
    {
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("获得到的文本信息：" + req.downloadHandler.text);

            byte[] bytes = req.downloadHandler.data;
            Debug.Log("字节长度：" + bytes.Length);
        }
        else
        {
            Debug.LogError("请检查路径是否正确！" + url);
            Debug.LogError("获取失败" + req.result + req.error + req.responseCode);
        }
    }


    /// <summary>
    /// Texture load by UnityWebRequest
    /// </summary>
    /// <param name="url"></param>
    /// <param name="image"></param>
    /// <param name="loadingType"></param>
    public void UnityWebReq_Texture(string url, RawImage image, WWWLoadingType loadingType)
    {
        switch (loadingType)
        {
            case WWWLoadingType.HTTP:
                Debug.Log("CurrentLoadingType:" + loadingType);
                BaseMono.Instance.StartCoroutine(IE_LoadTexture(url, image));
                break;
            case WWWLoadingType.FTP:
                Debug.Log("CurrentLoadingType:" + loadingType);
                BaseMono.Instance.StartCoroutine(IE_LoadTexture("ftp://" + url, image));
                break;
            case WWWLoadingType.FILE:
                Debug.Log("CurrentLoadingType:" + loadingType);
                //ssdafasdfa
                BaseMono.Instance.StartCoroutine(IE_LoadTexture("file://" + url, image));
                break;
            default:
                break;
        }
    }
    IEnumerator IE_LoadTexture(string url, RawImage image)
    {
        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            //方式一：
            // image.texture=(req.downloadHandler as DownloadHandlerTexture).texture;

            //方式二：
            image.texture = DownloadHandlerTexture.GetContent(req);
        }
        else
        {
            Debug.LogError("请检查路径是否正确！" + url);
            Debug.LogError("获取失败" + req.result + req.error + req.responseCode);
        }
    }



    /// <summary>
    /// AssetBundle Loading by UnityWebRequest
    /// </summary>
    /// <param name="url"></param>
    public void UnityWebReq_AssetBundle(string url)
    {
        BaseMono.Instance.StartCoroutine(IE_LoadAssetBundle(url));
    }

    IEnumerator IE_LoadAssetBundle(string url)
    {
        UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle(url);
        while (!req.isDone)
        {
            Debug.Log("资源加载的进度：" + req.downloadProgress);
            Debug.Log("加载的字节数量：" + req.downloadedBytes);
            yield return null;
        }
        //yield return req;
        if (req.result == UnityWebRequest.Result.Success)
        {
            //方法一：
            //AssetBundle ab = (req.downloadHandler as DownloadHandlerAssetBundle).assetBundle;

            //方法二：
            AssetBundle ab = DownloadHandlerAssetBundle.GetContent(req);
        }
        else
        {
            Debug.LogError("请检查路径是否正确！" + url);
            Debug.LogError("获取失败" + req.result + req.error + req.responseCode);
        }
    }
}
