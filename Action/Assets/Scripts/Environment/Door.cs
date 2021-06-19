using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string doorName;

    private Inventory inv;

    // Start is called before the first frame update
    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (!other.gameObject.CompareTag("Player"))
            return;

        foreach (string key in inv.keys)
        {
            if (key == doorName)
            {
                Destroy(this.gameObject);
                inv.keys.Remove(key);
                return;
            }
        }
    }
}
