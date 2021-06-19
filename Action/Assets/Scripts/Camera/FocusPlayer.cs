using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPlayer : MonoBehaviour
{
    private GameObject player;

    public Vector3 offset = new Vector3(0, 6, -15);
    public float smooth = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        transform.position = Vector3.Lerp(transform.position, newPosition + offset, Time.fixedDeltaTime * smooth);
    }
}
