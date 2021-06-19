using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]

public class LockRotation : MonoBehaviour
{
    //Only use for Popup Message

    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rt.rotation = Quaternion.Euler(0, 0, 0);
    }
}
