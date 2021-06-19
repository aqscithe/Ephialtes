using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleFallingObjects : MonoBehaviour
{
    GameObject playerSpawn = null;
    InGameMenu inGameMenu = null;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("Spawn_Player");
        inGameMenu = FindObjectOfType<InGameMenu>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            inGameMenu.LoadGameOver();
        else if (!other.gameObject.CompareTag("Ground") && !other.gameObject.CompareTag("Background"))
        {
            Debug.Log("destroying" + other.gameObject.name);
            Destroy(other.gameObject);
        }

    }
}
