using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool on = false;
    private GameObject spotLight;

    // Start is called before the first frame update
    void Start()
    {
        //TODO : switch that controls 2 lights
        //2 is spotlight index;
        spotLight = transform.parent.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void clicked()
    {
        on = !on;
        spotLight.SetActive(on);
    }
}
