using System;
using UnityEngine;

namespace EventDeepLean
{
    public class TestEventSubscriber : MonoBehaviour
    {
        private void Start()
        {
            TestingEvents _testingEvents = GetComponent<TestingEvents>();
            _testingEvents.OnKeyDown += Testing_OnClickDown;

            _testingEvents.OnKeyDown2 += Testing_OnClickDown2;
        }

        private void Testing_OnClickDown2(float a)
        {
            Debug.Log("事件传入的参数为："+a);
        }

        void Testing_OnClickDown(object sender,EventArgs e)
        {
            
            Debug.Log(@"keydown"+" e.temp="+e);
        }
        
    }
}