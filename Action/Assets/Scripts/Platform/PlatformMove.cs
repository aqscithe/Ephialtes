using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlatformMove : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 orientation = new Vector3(0, 1, 0);

    private int direction = 1;

    private bool inMotion = true;
    private InMotion mo;
    private bool hasComponentInMotion = false;

    private Rigidbody rb;

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
    void Update()
    {
        if (hasComponentInMotion)
            inMotion = mo.isMoving;
        else
            inMotion = true;

        setConstraints();

        if (inMotion)
        {
            rb.isKinematic = false;
            rb.velocity = new Vector3(direction * speed * orientation.x,
                                      direction * speed * orientation.y,
                                      direction * speed * orientation.z);
        }
        else
        {
            rb.isKinematic = true;
        }
    }

    private void setConstraints()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (orientation.x == 0f)
            rb.constraints |= RigidbodyConstraints.FreezePositionX;

        if (orientation.y == 0f)
            rb.constraints |= RigidbodyConstraints.FreezePositionY;

        if (orientation.z == 0f)
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UpperEdge"))
            direction = -1;
        else if (other.CompareTag("LowerEdge"))
            direction = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
