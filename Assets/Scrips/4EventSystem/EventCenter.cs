using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Using the Liskov—Substitution—Principle, 
/// the parent class wraps the subclass
/// </summary>
public abstract class EventInfoBase { }

public class EventInfo : EventInfoBase
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

/// <summary>
/// Used to wrap" The observer "That is the class of the delegate function
/// </summary>
/// <typeparam name="T"></typeparam>
public class EventInfo<T> : EventInfoBase
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo<T, O> : EventInfoBase
{
    public UnityAction<T, O> actions;

    public EventInfo(UnityAction<T, O> action)
    {
        actions += action;
    }
}


/// <summary>
/// Event Center
/// </summary>
public class EventCenter : BaseSingletonWithoutMono<EventCenter>
{
    //Event Container
    Dictionary<E_EventType, EventInfoBase> eventDic = new Dictionary<E_EventType, EventInfoBase>();

    #region EventTrigger
    /// <summary>
    /// Applies to events that do not require parameters
    /// </summary>
    /// <param name="e_type">EventType(enumType)</param>
    public void EventTrigger(E_EventType e_type)
    {
        //This delegate call is executed only 
        //if there is a gameobject that subscribes to the event
        if (eventDic.ContainsKey(e_type))
        {
            //Converting parent class unpacking to child class.
            (eventDic[e_type] as EventInfo).actions?.Invoke();
        }
    }

    /// <summary>
    /// Applicable to events that require only one parameter
    /// </summary>
    /// <param name="e_type"></param>
    /// <param name="info"></param>
    /// <typeparam name="T"></typeparam>
    public void EventTrigger<T>(E_EventType e_type, T info)
    {
        if (eventDic.ContainsKey(e_type))
        {
            (eventDic[e_type] as EventInfo<T>).actions?.Invoke(info);
        }
    }

    public void EventTrigger<T, O>(E_EventType e_type, T info, O info2)
    {
        if (eventDic.ContainsKey(e_type))
        {
            (eventDic[e_type] as EventInfo<T, O>).actions?.Invoke(info, info2);
        }
    }
    #endregion

    #region AddListner

    public void AddListener(E_EventType e_type, UnityAction func)
    {
        if (eventDic.ContainsKey(e_type))
        {
            (eventDic[e_type] as EventInfo).actions += func;
        }
        else
        {
            eventDic.Add(e_type, null);
            (eventDic[e_type] as EventInfo).actions += func;
        }
    }

    public void AddListener<T>(E_EventType e_type, UnityAction<T> func)
    {
        if (eventDic.ContainsKey(e_type))
        {
            (eventDic[e_type] as EventInfo<T>).actions += func;
        }
        else
        {
            eventDic.Add(e_type, new EventInfo<T>(func));
        }
    }

    public void AddListener<T, O>(E_EventType e_type, UnityAction<T, O> func)
    {
        if (eventDic.ContainsKey(e_type))
        {
            (eventDic[e_type] as EventInfo<T, O>).actions += func;
        }
        else
        {
            eventDic.Add(e_type, new EventInfo<T, O>(func));
        }
    }
    #endregion

    #region Remove

    public void RemoveListener(E_EventType e_type, UnityAction func)
    {
        if (eventDic.ContainsKey(e_type))
            (eventDic[e_type] as EventInfo).actions -= func;
    }
    public void RemoveListener<T>(E_EventType e_type, UnityAction<T> func)
    {
        if (eventDic.ContainsKey(e_type))
            (eventDic[e_type] as EventInfo<T>).actions -= func;
    }

    public void RemoveListener<T, O>(E_EventType e_type, UnityAction<T, O> func)
    {
        if (eventDic.ContainsKey(e_type))
            (eventDic[e_type] as EventInfo<T, O>).actions -= func;
    }
    #endregion

    public void ClearListener()
    {
        eventDic.Clear();
    }

    public void ClearListener(E_EventType e_type)
    {
        if (eventDic.ContainsKey(e_type))
            eventDic.Remove(e_type);
    }
}
