using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraZ : MonoBehaviour
{
    float zPos;

    private void Start()
    {
        zPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 setPosition = transform.position;
        setPosition.x = transform.position.x;
        setPosition.y = transform.position.y;
        setPosition.z = zPos;
        transform.position = setPosition;
    }
}
