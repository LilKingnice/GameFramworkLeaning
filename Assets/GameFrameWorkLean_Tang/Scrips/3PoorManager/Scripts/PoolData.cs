using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 抽屉对象
/// </summary>
public class PoolData
{
    //每一种类型的对象池容器
    Stack<GameObject> stackObjs = new Stack<GameObject>();

    //用来存储使用对象的容器
    List<GameObject> usedList = new List<GameObject>();

    int maxNum; //可存在的最大数量

    //具体的对象池父类对象
    GameObject rootObj;

    public int Count => stackObjs.Count; //获取容器中是否有对象 

    public int UsedCount => usedList.Count;

    public bool canCreate => usedList.Count < maxNum;

    public PoolData(GameObject poolroot, string name, GameObject usedObj)
    {
        if (Poolmanager.isOpenLayout)
        {
            rootObj = new GameObject(name); //设置具体池子和名字
            rootObj.transform.SetParent(poolroot.transform);
        }

        //在创建新对象池的时候就一定会伴随着创建新对象，这时候就将创建好的对象一起记录到使用列表List中
        PushUsedList(usedObj);
        PoolPrefabController prefcontroller = usedObj.GetComponent<PoolPrefabController>();
        if (prefcontroller == null)
        {
            Debug.LogWarning("添加销毁脚本PoolPrefabController！！");
            return;
        }

        maxNum = prefcontroller.maxNums;
    }

    //出栈
    public GameObject Pop()
    {
        GameObject obj;
        //当对象池中有实例
        if (Count > 0)
        {
            //直接取出当前实例对象
            obj = stackObjs.Pop();
            usedList.Add(obj);
        }
        else
        {
            //如果对象池中没有对象
            obj = usedList[0]; //第一个就是存在最久的
            usedList.RemoveAt(0); //先移除
            usedList.Add(obj); //再加回去使用
        }


        //给对象失活
        obj.SetActive(true);
        //给对象添加父节点为空
        if (Poolmanager.isOpenLayout)
            obj.transform.SetParent(null);
        return obj;
    }

    //进栈
    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        if (Poolmanager.isOpenLayout)
            obj.transform.SetParent(rootObj.transform);
        stackObjs.Push(obj);

        usedList.Remove(obj); //这个对象不再使用就从使用列表List中移除
    }

    /// <summary>
    /// 往使用中List添加对象
    /// </summary>
    /// <param name="obj"></param>
    public void PushUsedList(GameObject obj)
    {
        usedList.Add(obj);
    }
}