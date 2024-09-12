using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Test for use Event to using update method
/// </summary>
public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BaseMono.Instance.AddUpdateListener(testingReuseUpdate);
    }

    /// <summary>
    /// Reuse Update by calling singleton mode. 
    /// </summary>
    void testingReuseUpdate()
    {
        Debug.Log("testingtestingtestingtesting!!!!");
        
    }
}
