using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCamera : MonoBehaviour
{
    public Vector3 target;
    public float speed = 5f;

    private Camera cam;
    private FocusPlayer fp;

    private bool inRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        fp = cam.GetComponent<FocusPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inRoom)
            return;

        cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position + target, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        fp.enabled = false;
        inRoom = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        fp.enabled = true;
        inRoom = false;
    }
}
