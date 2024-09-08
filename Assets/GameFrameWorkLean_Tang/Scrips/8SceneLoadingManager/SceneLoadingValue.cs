using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadingValue : MonoBehaviour
{
    public Slider slider;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        EventCenter.Instance.AddListener<float>(E_EventType.E_LoadingProgress,SliderValueChange);
        DontDestroyOnLoad(gameObject);
    }
    public void SliderValueChange(float value)
    {
        Debug.Log("当前value："+value);
        slider.value=value;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            SceneMgr.Instance.AsyncLoadingScene("PoolManagerModule",()=>{
                Debug.Log("加载完成");
            });
        }
    }
}
