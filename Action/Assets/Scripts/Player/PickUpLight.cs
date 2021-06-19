using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerOrientation po = other.gameObject.GetComponent<PlayerOrientation>();
        po.hasFlashlight = true;

        other.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        Destroy(this.gameObject);
    }
}
