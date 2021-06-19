using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 6, -15);

    private FocusPlayer fp;
    private Vector3 baseOffset;

    // Start is called before the first frame update
    void Start()
    {
        fp = Camera.main.GetComponent<FocusPlayer>();
        baseOffset = fp.offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        fp.offset = offset;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        fp.offset = baseOffset;
    }
}
