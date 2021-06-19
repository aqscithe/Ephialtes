using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour
{
    InGameMenu igm;

    // Start is called before the first frame update
    void Start()
    {
        igm = GameObject.Find("InGameMenuCanvas").GetComponent<InGameMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        igm.ShowYouWin();
    }
}
