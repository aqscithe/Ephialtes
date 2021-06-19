using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_pos : MonoBehaviour
{
    public Vector3 position;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        position = this.position;
        camera.GetComponent<FocusPlayer>().offset = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
