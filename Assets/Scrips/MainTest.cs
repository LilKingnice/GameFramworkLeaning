using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            menager.Instance.IcanUpdate();
        if (Input.GetKeyUp(KeyCode.Space))
            menager.Instance.IStopUpdate();



        #region PoolTesting

        if (Input.GetMouseButton(0))
        {
            Poolmanager.Instance.PopItem("PoolManagerPrefab/Cube",MousePointToWorld.Instance.GetMousePosition());
        }

        if (Input.GetMouseButton(1))
        {
            Poolmanager.Instance.PopItem("PoolManagerPrefab/Sphere",MousePointToWorld.Instance.GetMousePosition());
        }
        #endregion
    }
}
