using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshObstacle))]

public class EnemyBox : MonoBehaviour
{
    Rigidbody rb;
    NavMeshObstacle ob;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        ob = GetComponent<NavMeshObstacle>();
        ob.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            ob.enabled = true;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            ob.enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        ob.enabled = false;
    }
}
