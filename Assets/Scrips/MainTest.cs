using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTest : MonoBehaviour
{
    public bool isOpenLayout;//控制poolmanager中的布局整理开关

    Vector3 pos;
    void Start()
    {
        Poolmanager.isOpenLayout=this.isOpenLayout;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            menager.Instance.IcanUpdate();
        if (Input.GetKeyUp(KeyCode.Space))
            menager.Instance.IStopUpdate();

        #region PoolTesting
        //use the poolmanager
        if (Input.GetMouseButton(0))
        {
            Poolmanager.Instance.PopItem("Cube",MousePointToWorld.Instance.GetMousePosition());
        }
        //didn't use the poolmanager
        if (Input.GetMouseButton(1))
        {
            pos=MousePointToWorld.Instance.GetMousePosition();
            //Poolmanager.Instance.PopItem("Sphere",MousePointToWorld.Instance.GetMousePosition());
            Instantiate(Resources.Load<GameObject>("Sphere"), pos, Quaternion.identity.normalized);
        }
        #endregion
    }
}
