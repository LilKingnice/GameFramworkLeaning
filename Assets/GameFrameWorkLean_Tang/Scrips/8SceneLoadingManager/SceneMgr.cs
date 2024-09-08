using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr : BaseSingletonWithoutMono<SceneMgr>
{
    

    public void LoadingScene(string scenename,UnityAction callback=null)
    {
        SceneManager.LoadScene(scenename);
        callback?.Invoke();
        callback=null;
    }


    public void AsyncLoadingScene(string scenename,UnityAction callback=null)
    {
        BaseMono.Instance.StartCoroutine(RealLoading(scenename,callback));
    }

    IEnumerator RealLoading(string name,UnityAction callback=null)
    {

        AsyncOperation ao=SceneManager.LoadSceneAsync(name);

        while(!ao.isDone)
        {
            EventCenter.Instance.EventTrigger<float>(E_EventType.E_LoadingProgress,ao.progress);
            yield return 0; 
        }
        EventCenter.Instance.EventTrigger<float>(E_EventType.E_LoadingProgress,1);
        callback?.Invoke();
        callback=null;
    }

}
