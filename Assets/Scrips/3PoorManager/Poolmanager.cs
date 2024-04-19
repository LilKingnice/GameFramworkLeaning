using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolmanager: BaseSingletonWithoutMono<Poolmanager>
{
    private Dictionary<string, Stack<GameObject>> pooldic = new Dictionary<string, Stack<GameObject>>();

    
    /// <summary>
    /// output with default position
    /// </summary>
    /// <param name="name"></param>
    public GameObject PopItem(string name)
    {
        GameObject obj;
        if (pooldic.ContainsKey(name)&& pooldic[name].Count>0)
        {
            obj = pooldic[name].Pop();
            
            //sets the active of the object to true or false instead of destroy
            //You can also place the inactivated object in a place where it cannot be captured by the camera
            obj.SetActive(true);
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
        }

        return obj;
    }
    
    /// <summary>
    /// output with custom position
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pos">Generating position</param>
    /// <returns></returns>
    public GameObject PopItem(string name,Vector3 pos)
    {
        GameObject obj;
        if (pooldic.ContainsKey(name)&& pooldic[name].Count>0)
        {
            obj = pooldic[name].Pop();
            
            //sets the active of the object to true or false instead of destroy
            //You can also place the inactivated object in a place where it cannot be captured by the camera
            obj.transform.position = pos;
            obj.SetActive(true);
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name),pos,Quaternion.identity.normalized);
        }
        return obj;
    }
    
    
    
    
    /// <summary>
    /// Pushed into the ObjectPool
    /// </summary>
    /// <param name="name">容器名称</param>
    /// <param name="obj">The GameObject that will be pushed into the ObjectPool</param>
    public void PushItem(string name,GameObject obj)
    {
        obj.SetActive(false);
        if (!pooldic.ContainsKey(name))
            pooldic.Add(name, new Stack<GameObject>());
        pooldic[name].Push(obj);
        
        // if (pooldic.ContainsKey(name))
        // {
        //     pooldic[name].Push(obj);
        // }
        // else
        // {
        //     pooldic.Add(name, new Stack<GameObject>());
        //     pooldic[name].Push(obj);
        // }
    }

    
    /// <summary>
    /// Clear the ObjectPool
    /// </summary>
    public void ClearPool()
    {
        pooldic.Clear();
    }
}