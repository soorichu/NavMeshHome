using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager targetInstance;
    public GameObject target;
    GameObject agent;
//    AudioSource bell;

    void Start()
    {
        if(targetInstance == null)
        {
            targetInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        target = null;
        agent = GameObject.FindWithTag("agent");
 //       bell = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (target != null)
        {
            float dist = Vector3.Distance(target.transform.position, agent.transform.position);
  //          Debug.Log("Distance between target and agent: " + dist);
            if(dist <= 3)
            {
                target.transform.position = agent.transform.position + new Vector3(1, 0, 1);
  //              bell.Play();
            }
        }
    }
}
