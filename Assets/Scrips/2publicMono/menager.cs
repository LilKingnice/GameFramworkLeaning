using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menager : BaseSingletonWithoutMono<menager>
{
    private Coroutine menagerCoroutine;
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
