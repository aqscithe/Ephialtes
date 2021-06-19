using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class LightBlocMove : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 orientation = new Vector3(0, 1, 0);

    private int direction = 1;

    private bool inMotion = true;
    private InMotion mo;
    private bool hasComponentInMotion = false;

    private Rigidbody rb;

    private Vector3 translation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent<InMotion>(out mo))
            hasComponentInMotion = true;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasComponentInMotion)
            inMotion = mo.isMoving;
        else
            inMotion = true;

        if (inMotion)
        {
            setConstraints();

            rb.isKinematic = false;
            translation = orientation * direction * speed * Time.deltaTime;
            transform.Translate(translation);
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;

            rb.isKinematic = true;
        }
    }

    private void setConstraints()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UpperEdge"))
            direction = -1;
        else if (other.CompareTag("LowerEdge"))
            direction = 1;
    }
}

