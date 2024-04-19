using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolPrefabController : MonoBehaviour
{
    public string name;
    private void OnEnable()
    {
        Invoke("myDestroy",1f);
    }

    private void myDestroy()
    {
        Poolmanager.Instance.PushItem(name,gameObject);
    }
}
