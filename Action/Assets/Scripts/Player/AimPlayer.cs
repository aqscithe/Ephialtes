using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPlayer : MonoBehaviour
{
    [SerializeField] GameObject flashlight = null;
    [SerializeField] float rotateSpeed = 30f;

    void Update()
    {
        if (Input.GetAxis("Rotate") > 0f)
        {
            Debug.Log("Rotating right");
            flashlight.transform.Rotate(rotateSpeed * Time.deltaTime, 0f, 0f);
        }
        else if(Input.GetAxis("Rotate") < 0f)
        {
            Debug.Log("Rotating left");
            flashlight.transform.Rotate(-rotateSpeed * Time.deltaTime, 0f, 0f);
        }
        
    }
}
