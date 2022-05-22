using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collioder");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");

    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
