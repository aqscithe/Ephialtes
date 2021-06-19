using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NullifyLight : MonoBehaviour
{

    GameObject playerLight = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Thunder"))
            return;

        if (other.CompareTag("Player"))
        {
            playerLight = other.transform.GetChild(0).GetChild(2).gameObject;
            playerLight.SetActive(false);
        }
        else if (other.GetComponentInChildren<Light>())
            other.GetComponentInChildren<Light>().enabled = false;
        else if(other.GetComponent<Light>())
            other.GetComponent<Light>().enabled = false;
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Thunder"))
            return;

        if (other.CompareTag("Player"))
            playerLight.SetActive(true);
        else if (other.GetComponentInChildren<Light>())
            other.GetComponentInChildren<Light>().enabled = true;
        else if (other.GetComponent<Light>())
            other.GetComponent<Light>().enabled = true;
            
    }
}
