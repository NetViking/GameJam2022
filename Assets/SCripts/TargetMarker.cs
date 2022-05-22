using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetMarker : MonoBehaviour
{

    public LayerMask worldLayer;

    
    public void OnSetTargetPos()
    {
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData, worldLayer))
            {
                // target.SetTargetPos(hitData.point);
                transform.position = hitData.point;
            }
        
    }
}
