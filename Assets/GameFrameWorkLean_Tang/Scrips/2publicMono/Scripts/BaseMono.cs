using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Reuses Update FixedUpdate LateUpdate
/// </summary>
public class BaseMono : BaseSingletonWtihMonoAuto<BaseMono>
{
    private event UnityAction UpdateAction;
    private event UnityAction FixedUpdateAction;
    private event UnityAction LateUpdateAction;

    public void AddUpdateListener(UnityAction method)
    {
        UpdateAction += method;
    }
    public void AddFixedUpdateListener(UnityAction method)
    {
        FixedUpdateAction += method;
    }
    public void AddLateUpdateListener(UnityAction method)
    {
        LateUpdateAction += method;
    }

    public void RemoveUpdateListener(UnityAction method)
    {
        UpdateAction -= method;
    }
    
    public void RemoveFixedUpdateListener(UnityAction method)
    {
        FixedUpdateAction -= method;
    }
    
    public void RemoveLateUpdateListener(UnityAction method)
    {
        LateUpdateAction -= method;
    }
    
    
    private void Update()
    {
        UpdateAction?.Invoke();
    }

    private void FixedUpdate()
    {
        FixedUpdateAction?.Invoke();
    }

    private void LateUpdate()
    {
        LateUpdateAction?.Invoke();
    }
}


/*
 * 主要作用：
 * 让不继承MonoBehaviour的脚本也能
 * 1.利用帧更新或定时更新处理逻辑
 * 2.利用协同程序处理逻辑
 * 3.可以统一执行管理帧更新或定时更新相关逻辑
 *
 * 公共Mono模块的基本原理
 * 1.通过事件或委托管理 方法执行，不需要继承MonoBehaviour
 * 2.提供协同程序开启或关闭的方法
 */
