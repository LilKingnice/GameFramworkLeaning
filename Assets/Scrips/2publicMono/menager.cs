using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Using the BaseMono class
/// This class Demonstrates the benefits of using GlobalMono CLASS 
/// To call methods in MonoBehaviour that does not inherited from MonoBehaviour
/// </summary>
public class menager : BaseSingletonWithoutMono<menager>
{
    private Coroutine menagerCoroutine;//this field is used to load a function which is call by outside IEnumerator
    public void IcanUpdate()
    {
        BaseMono.Instance.AddUpdateListener(myUpdate);

        menagerCoroutine=BaseMono.Instance.StartCoroutine(menagerIE());
    }

    public void IStopUpdate()
    {
        BaseMono.Instance.RemoveUpdateListener(myUpdate);

        BaseMono.Instance.StopCoroutine(menagerCoroutine);
    }

    IEnumerator menagerIE()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("menager IEnumerator！！！");
    }

    private void myUpdate()
    {
        Debug.Log("menager update");
    }
}
