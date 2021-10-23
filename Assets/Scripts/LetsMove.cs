using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LetsMove : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    public Transform older;
    bool seeked;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        seeked = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !seeked)
        {
            agent.SetDestination(target.position);
        }
        if(Vector3.Distance(transform.position, target.position) < 2)
        {
            seeked = true;
            agent.SetDestination(older.position);
        }
    }
}
