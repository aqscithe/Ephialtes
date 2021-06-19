using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public GameObject objet;
    // Start is called before the first frame update
    void Start()
    {
        objet.SetActive(false);
    }

    void OnTriggerEnter()
    {
        objet.SetActive(true);
    }
}
