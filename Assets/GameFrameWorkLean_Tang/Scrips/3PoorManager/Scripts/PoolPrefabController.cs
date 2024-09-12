using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 使用对象池的销毁脚本
/// </summary> 
public class PoolPrefabController : MonoBehaviour
{
    public int maxNums;
    private void OnEnable()
    {
        Invoke("myDestroy",1f);
    }

    private void myDestroy()
    {
        Poolmanager.Instance.PushItem(gameObject);
    }
}
