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
