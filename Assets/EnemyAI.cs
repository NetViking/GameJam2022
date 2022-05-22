using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    float movementspeed = 2.2f;
    [SerializeField]
    float chasespeed = 3.0f;
    [SerializeField]
    float searchrotation = 0.5f;
    [SerializeField]
    float viewfield = 60f;
    [SerializeField]
    float viewrange = 5f;
    [SerializeField]
    float audiorange = 5f;
    Vector3 target;
    [SerializeField]
    private int index;
    [SerializeField]
    GameObject RouteObject;
    PatrolPoint[] pointlist;
    float chasingTimer = 0f;
    [SerializeField]
    float maxchaseTimeout = 3f;
    [SerializeField]
    Mode mode = Mode.idle;


    NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        // Set Start at home
        nav = GetComponent<NavMeshAgent>();
        index = 0;
        pointlist = RouteObject.GetComponentsInChildren<PatrolPoint>();
        target = this.transform.position;
        GetComponent<SphereCollider>().radius = audiorange;
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case Mode.idle:
                target = this.transform.position;
                if (pointlist.Length > 0)
                {
                    mode = Mode.patrol;
                }
                break;
            case Mode.chase:
                {
                    nav.speed = chasespeed;
                    if (TargetReached())
                    {
                        if (chasingTimer >= maxchaseTimeout)
                        {
                            mode = Mode.patrol;
                            target = pointlist[index].transform.position;
                        }
                        else
                        {
                            // searching at the last point
                            transform.Rotate(0, searchrotation, 0);
                            chasingTimer += Time.deltaTime;
                        }
                    }
                    else
                    {
                        // not reached destination chasing goes on
                        chasingTimer = 0f;
                    }
                    break;
                }
            case Mode.patrol:
                {
                    nav.speed = movementspeed;
                    if (TargetReached())
                    {
                        if (index < pointlist.Length - 1)
                        {
                            index++;
                            target = pointlist[index].transform.position;

                        }
                        else
                        {
                            index = 0;
                            target = pointlist[index].transform.position;

                        }
                    }
                    else
                    {
                        nav.SetDestination(target);
                        //nav.speed = movementspeed;
                    }
                    break;
                }
            default:
                mode = Mode.patrol;
                break;
        }
                nav.SetDestination(target);
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // player found
            Debug.Log(gameObject.name + " hears you");
            mode = Mode.chase;
            target = other.transform.position;
            chasingTimer = 0f;

            if (Vector3.Angle(Vector3.forward, other.transform.position) > (360-viewfield) || Vector3.Angle(Vector3.forward, other.transform.position) < ( viewfield))
            {
                Ray ray = new Ray(transform.position, other.transform.position - transform.position);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (hit.transform.gameObject.tag == "Player")
                {
                    Debug.DrawRay(transform.position, hit.point, Color.red, 1.2f);
                    Debug.Log(gameObject.name + " sees you");
                }
            }
            
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // player found
            Debug.Log(gameObject.name + " still hears you");
            mode = Mode.chase;
            target = other.transform.position;
            chasingTimer = 0f;
            if (Vector3.Angle(Vector3.forward, other.transform.position) > (360 - viewfield) || Vector3.Angle(Vector3.forward, other.transform.position) < (viewfield))
            {
                Ray ray = new Ray(transform.position, other.transform.position - transform.position); 
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (hit.transform.gameObject.tag == "Player")
                {
                    Debug.DrawRay(transform.position, hit.point, Color.green, 0.5f);
                    Debug.Log(gameObject.name + " still sees you");
                }
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis(viewfield, transform.up) * transform.forward * viewrange);
        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis(-viewfield, transform.up) * transform.forward * viewrange);

        
        Gizmos.DrawWireSphere(transform.position, audiorange);
    }

    bool TargetReached()
    {
        if (target.x - transform.position.x < 0.2f &&
            target.z - transform.position.z < 0.2f)
        //if (Vector3.Distance(target, transform.position) < 0.6f)
        { return true; }
        return false;
    }

    enum Mode {
        idle,
        chase,
        patrol
    }
}
