using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseController : MonoBehaviour
{
    NavMeshAgent nav;
    TargetMarker target;
    public LayerMask worldLayer;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<TargetMarker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
