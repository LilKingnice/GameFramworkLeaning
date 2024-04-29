using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 不使用对象池的销毁脚本
/// </summary>
public class Justdistory : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("myDestroy",0.6f);
    }

    private void myDestroy()
    {
        Destroy(gameObject);
    }
}
