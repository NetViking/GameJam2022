using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    NavMeshAgent nav;
    GameObject target;
    [SerializeField]
    MovementType movehow = MovementType.walking;

    [SerializeField]
    enum MovementType

    {// times ten to convert easily to float (enum only accepts integer)
        stand = 0,
        sneak = 6,
        walking = 22, 
        running = 38
    }

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<TargetMarker>().gameObject;
    }


    bool TargetReached()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < 0.3f)
        { return true; }
        return false;
    }

    public void OnRun ()
    {
        Debug.Log("Run");
        movehow = MovementType.running;
    }

    public void OnSneak()
    {
        Debug.Log("SNEAL");
        movehow = MovementType.sneak;
    }

    // Update is called once per frame
    void Update()
    {
                
        if (TargetReached())
        {
            movehow = MovementType.stand;
        }
        else
        {
            // if still standing and not there, walk
            if (movehow == MovementType.stand)
            {
                movehow = MovementType.walking;
            }
            
        }
        nav.speed = ((float)movehow)/10f;
        nav.SetDestination(target.transform.position);

    }
}
