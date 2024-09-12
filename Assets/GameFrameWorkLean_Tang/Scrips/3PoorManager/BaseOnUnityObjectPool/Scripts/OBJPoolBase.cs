using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class OBJPoolBase<T> : BaseSingletonWtihMonoAuto<T> where T : MonoBehaviour
{
    private ObjectPool<T> _pool;

    [SerializeField] private T template;

    [SerializeField,Range(5,20)] private int poolDefualtSize;
    [SerializeField,Range(10,20)] private int poolMaxSize;


    protected override void Awake()
    {
        base.Awake();
        _pool = new ObjectPool<T>(OnCreatePool,OnGetItem,OnPushItem,OnDestroyPool);
    }

    #region 传入委托方法

    protected virtual T OnCreatePool()
    {
        return Instantiate(template, transform);
    }
    protected virtual void OnGetItem(T obj)
    {
        obj.gameObject.SetActive(true);
    }
    protected virtual void OnPushItem(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    public void OnDestroyPool(T obj)
    {
        Destroy(obj.gameObject);
    }

    #endregion


    #region 真正的实现方法

    protected virtual T GetPoolItem()
    {
        return _pool.Get();
    }

    protected virtual void ReleasePoolItem(T item)
    {
        _pool.Release(item);
    }

    #endregion

}