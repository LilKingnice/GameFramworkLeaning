using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLoading : MonoBehaviour
{
    public RawImage image1;
    public RawImage image2;
    // Start is called before the first frame update
    void Start()
    {

        // ResourcesManager.Instance.AsyncLoad<GameObject>("ResourceLoadingAsset", (obj) =>
        // {
        //     Instantiate(obj);
        // });

        // ResourcesManager.Instance.LoadAsync("ResourceLoadingAsset",typeof(GameObject),(Object obj)=>{
        //     Instantiate(obj as GameObject);
        // });


        // Instantiate(EditorResManager.Instance.LoadEditorRes<GameObject>("Cube.prefab"));
        // Instantiate(EditorResManager.Instance.LoadEditorRes<GameObject>("EditorPrefab.prefab"));

        // ResourcesManager.Instance.AsyncLoad<GameObject>("ResourceLoadingAsset", TestFunc);
        // Debug.Log(ResourcesManager.Instance.GetRefCount<GameObject>("ResourceLoadingAsset"));


        // ResourcesManager.Instance.AsyncLoad<GameObject>("ResourceLoadingAsset", TestFunc);
        // Debug.Log(ResourcesManager.Instance.GetRefCount<GameObject>("ResourceLoadingAsset"));

        // ResourcesManager.Instance.UnloadAsset<GameObject>("ResourceLoadingAsset", false, TestFunc);
        // Debug.Log(ResourcesManager.Instance.GetRefCount<GameObject>("ResourceLoadingAsset"));

        // Instantiate(ResourcesManager.Instance.SyncLoad<GameObject>("ResourceLoadingAsset"), new Vector3(6, 2, 0), Quaternion.identity);
        // Debug.Log(ResourcesManager.Instance.GetRefCount<GameObject>("ResourceLoadingAsset"));

        // ResourcesManager.Instance.UnloadAsset<GameObject>("ResourceLoadingAsset", false, TestFunc);
        // Debug.Log(ResourcesManager.Instance.GetRefCount<GameObject>("ResourceLoadingAsset"));

        // ResourcesManager.Instance.UnloadAsset<GameObject>("ResourceLoadingAsset", false, TestFunc);
        // Debug.Log(ResourcesManager.Instance.GetRefCount<GameObject>("ResourceLoadingAsset"));



        //StartCoroutine(HttpGet());
        // WWWLoadingManager.Instance.WWW_GetImage("https://img.freepik.com/free-photo/painting-mountain-lake-with-mountain-background_188544-9126.jpg",
        // image1, WWWLoadingType.HTTP);

        // WWWLoadingManager.Instance.UnityWebReq_LoadText(Application.dataPath + "/webReq.txt", WWWLoadingType.FILE);

        // WWWLoadingManager.Instance.UnityWebReq_Texture(Application.dataPath + "/title.png", image2, WWWLoadingType.FILE);

        UWQResManager.Instance.LoadResource<Texture>("https://img.freepik.com/free-photo/painting-mountain-lake-with-mountain-background_188544-9126.jpg",
        (str) =>
        {
            image1.texture = str;
        },
        () =>
        {
            Debug.LogWarning("加载失败");
        },
         LoadingType.HTTP);

        UWQResManager.Instance.LoadResource<string>(Application.dataPath + "/webReq.txt",
        (txt) =>
        {
            Debug.Log(txt);
        },
        () =>
        {
            Debug.Log("文本加载失败！");
        },
            LoadingType.FILE);


        UWQResManager.Instance.LoadResource<Texture>(Application.dataPath + "/title.png",
        (img) =>
        {
            image2.texture=img;
        },
        () =>
        {
            Debug.Log("图片二加载失败！");
        },
            LoadingType.FILE);
        //UWQResManager.Instance.UnityWebReq_Texture(Application.dataPath + "/title.png", image2, LoadingType.FILE);

        ResLoadingManager.Instance.LoadRES<GameObject>((res)=>{
            GameObject obj=Instantiate(res);
            obj.name="从editor中加载的cube";
        },"Cube.prefab","gameobj",false,false);
    }

    // IEnumerator HttpGet()
    // {
    //     WWW mywww = new WWW("https://img.freepik.com/free-photo/painting-mountain-lake-with-mountain-background_188544-9126.jpg");

    //     while (!mywww.isDone)
    //     {
    //         print("下载进度1:" + mywww.progress);
    //         print("已经下载了的字节数量1:" + mywww.bytesDownloaded);
    //         yield return mywww;
    //     }

    //     if (mywww.error == null)
    //     {
    //         print("下载进度2:" + mywww.progress);
    //         print("已经下载了的字节数量2:" + mywww.bytesDownloaded);
    //         image.texture = mywww.texture;
    //     }
    //     else
    //         print(mywww.error);
    // }
    void TestFunc(GameObject obj)
    {
        Instantiate(obj, Vector3.zero, Quaternion.identity);
    }
    void TestFunc(GameObject obj, Vector3 vector3)
    {
        Instantiate(obj, vector3, Quaternion.identity);
    }
    // Update is called once per frame 
    void Update()
    {

    }
}
