using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLight : MonoBehaviour
{
    public string lightName;

    private GameObject spotLight;
    private Inventory inv;

    // Start is called before the first frame update
    void Start()
    {
        //2 is spotlight index
        spotLight = transform.GetChild(2).gameObject;
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        foreach (string key in inv.keys)
        {
            if (key == lightName)
            {
                inv.keys.Remove(key);
                spotLight.SetActive(true);
                return;
            }
        }
    }
}
