using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolmanager : BaseSingletonWithoutMono<Poolmanager>
{
    private Poolmanager()
    {
        
    }

    //是否开启布局功能
    public static bool isOpenLayout = false;

    private Dictionary<string, PoolData> pooldic = new Dictionary<string, PoolData>();


    //管理类中对象池的父对象
    private GameObject poolObj;

    /// <summary>
    /// output with default position
    /// </summary>
    /// <param name="name"></param>
    public GameObject PopItem(string name)
    {
        GameObject obj;
        if (pooldic.ContainsKey(name) && pooldic[name].Count > 0)
        {

            obj = pooldic[name].Pop();

            // //sets the active of the object to true or false instead of destroy
            // //You can also place the inactivated object in a place where it cannot be captured by the camera
            obj.SetActive(true);
            obj.transform.SetParent(null);
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
    /// <param name="maxNum">The maximum number of objects that can be generated</param>
    /// <returns></returns>
    public GameObject PopItem(string name, Vector3 pos)
    {
        GameObject obj;
        #region 无上限时候的逻辑
        // if (pooldic.ContainsKey(name) && pooldic[name].Count > 0)
        // {

        //     obj = pooldic[name].Pop();
        //     //obj.transform.SetParent(null);
        //     //sets the active of the object to true or false instead of destroy
        //     //You can also place the inactivated object in a place where it cannot be captured by the camera
        //     obj.transform.position = pos;
        //     //obj.SetActive(true);
        // }
        // else
        // {
        //     obj = GameObject.Instantiate(Resources.Load<GameObject>(name), pos, Quaternion.identity.normalized);
        //     obj.name = name;
        // }

        #endregion

        if (poolObj == null)
            poolObj = new GameObject("PoolManager");

        #region 加入数量上限优化后的逻辑
        if (!pooldic.ContainsKey(name) || (pooldic[name].Count == 0 && pooldic[name].canCreate))
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name), pos, Quaternion.identity.normalized);
            obj.name = name;

            if (!pooldic.ContainsKey(name))
                pooldic.Add(name, new PoolData(poolObj, name, obj));
            else
                pooldic[name].PushUsedList(obj);
        }
        else
        {
            obj = pooldic[name].Pop();
        }
        obj.transform.position=pos;
        return obj;
        #endregion

    }

    /// <summary>
    /// Pushed into the ObjectPool
    /// </summary>
    /// <param name="obj">The GameObject that will be pushed into the ObjectPool</param>
    public void PushItem(GameObject obj)
    {

        // if (!pooldic.ContainsKey(obj.name))
        //     pooldic.Add(obj.name, new PoolData(poolObj, obj.name));

        pooldic[obj.name].Push(obj);
    }


    /// <summary>
    /// Clear the ObjectPool
    /// </summary>
    public void ClearPool()
    {
        pooldic.Clear();
        poolObj = null;
    }
}