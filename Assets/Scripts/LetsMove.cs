using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LetsMove : MonoBehaviour
{
    public static LetsMove instance;
    NavMeshAgent agent;
//    AudioSource bell;
    GameObject older;
//    public Transform target;
//    public Transform older;
    public bool seeking;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        older = GameObject.FindWithTag("older");
        agent = GetComponent<NavMeshAgent>();
//        bell = GetComponent<AudioSource>();
        seeking = false;
    }


    public void GoToTarget(GameObject target)
    {
 //       bell.Stop();

        if (!seeking)
        {
            agent.SetDestination(target.transform.position);
        }

        if(Vector3.Distance(transform.position, target.transform.position) < 3)
        {
            seeking = true;
 //           target.transform.SetParent(transform);
 //           target.transform.localPosition = new Vector3(0, 1, 0);

        }

    }

    public void BackToOlder(GameObject target)
    {
 //       bell.Stop();
        agent.SetDestination(older.transform.position);
        if(Vector3.Distance(transform.position, older.transform.position) <= 3)
        {
 //           target.transform.SetParent(null);
        }
    }

    public void FindTarget(GameObject target)
    {
        Debug.Log("Find " + target.name);
        GoToTarget(target);
    }

    public void TakeOlderToTarget(GameObject target) 
    {
        Debug.Log("Take Older to " + target.name);
 //       bell.Stop();

        if (!seeking)
        {
            agent.SetDestination(target.transform.position);
        }

        if (Vector3.Distance(transform.position, target.transform.position) <= 3)
        {
            seeking = true;
            //           target.transform.SetParent(transform);
            //           target.transform.localPosition = new Vector3(0, 1, 0);
            agent.SetDestination(older.transform.position);
        }
    }

    public void BringTargetToOlder(GameObject target)
    {
        Debug.Log("Bring " + target.name + " to Older");
//        bell.Stop();


        agent.SetDestination(target.transform.position);
        

        if (Vector3.Distance(transform.position, target.transform.position) < 3)
        {
            seeking = true;
            //           target.transform.SetParent(transform);
            //           target.transform.localPosition = new Vector3(0, 1, 0);
            agent.SetDestination(older.transform.position);
        }
    }


}
