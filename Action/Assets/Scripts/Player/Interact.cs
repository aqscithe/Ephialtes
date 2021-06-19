using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private GameObject grabbable = null;
    private GameObject interactible = null;
    private bool triggered = false;



    // Update is called once per frame
    void Update()
    {
        //Debug.Log(grabbable);



        if (grabbable != null)
        {
            if (Input.GetButton("Grab"))
            {
                grabbable.transform.position = new Vector3(grabbable.transform.position.x,
                                                           transform.position.y + grabbable.transform.localScale.y / 2,
                                                           grabbable.transform.position.z);
                grabbable.GetComponent<Rigidbody>().isKinematic = true;
                grabbable.transform.parent = transform;
            }
            else
            {
                grabbable.GetComponent<Rigidbody>().isKinematic = false;
                grabbable.transform.parent = null;
                grabbable = null;
            }
        }
        else if (interactible != null)
        {
            Vector3 tempPos = transform.position;
            if (Input.GetButtonDown("Interact"))
            {
                interactible.GetComponent<Switch>().clicked();
            }
            else if (!triggered)
            {
                interactible = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactible"))
        {
            interactible = other.gameObject;
            triggered = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Interactible"))
            triggered = false;
    }
}
