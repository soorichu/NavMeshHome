using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject agent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, agent.transform.position)<2)
        {
            transform.position = agent.transform.position + new Vector3(0, 1, 0);
        }
  //      Debug.Log(Vector3.Distance(transform.position, agent.transform.position));
    }
}
