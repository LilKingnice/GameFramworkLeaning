using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventDeepLean
{
    public class ArgsExtend : EventArgs
    {
        public int temp;
        public ArgsExtend()
        {
            Debug.Log("temp="+temp);
        }
    }

    public class TestingEvents : MonoBehaviour
    {
        //完整的事件声明方式
        public event EventHandler<ArgsExtend> OnKeyDown;
        
        //dotnet事件声明方式
        public delegate void myEventDelegate(float a);
        public event myEventDelegate OnKeyDown2;
        
        
        //使用内置委托声明
        
        void Start()
        {
            
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                OnKeyDown?.Invoke(this,new ArgsExtend());
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                OnKeyDown2?.Invoke(26.88f);
            }
        }
    }
}