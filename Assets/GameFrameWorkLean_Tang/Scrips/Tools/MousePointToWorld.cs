using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Get the mouse raycast position in world coordinates
/// </summary>
public class MousePointToWorld : BaseSingletonWtihMonoAuto<MousePointToWorld>
{
    private Vector3 mouseScreenPos = new Vector3();
    void Start()
    {
        BaseMono.Instance.AddFixedUpdateListener(UpdateMousePosition);
    }

    void UpdateMousePosition()
    {
        //Debug.Log("Mouse clicked world position:"+GetMousePosition());
        mouseScreenPos = Input.mousePosition;
    }

    /// <summary>
    /// The method to get the current mouse position
    /// </summary>
    /// <returns>current mouse position</returns>
    public Vector3 GetMousePosition()
    {
        Vector3 worldHitPos=new Vector3();
        if (Camera.main!=null)
        {
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPos);
            if (Physics.Raycast(ray,out RaycastHit hit))
            {
                worldHitPos = hit.point;
                //Debug.Log("Mouse clicked world position:"+worldHitPos);
            }
        }

        return worldHitPos;
    }
}
