using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] public float gameOverDelay = 4f;
    float delayTimer = 0f;
    HealthManager healthManager = null;
    ManageHealthbar healthbar = null;
    CheckpointManager checkpointManager = null;
    GameObject gameOver = null;
    GameObject paused = null;
    GameObject controls = null;
    GameObject options = null;
    GameObject youWin = null;
    GameObject chkptBtn = null;
    GameObject player = null;
    GameObject HUD = null;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
        healthbar = FindObjectOfType<ManageHealthbar>();
        checkpointManager = GameObject.FindGameObjectWithTag("Player").GetComponent<CheckpointManager>();
        gameOver = gameObject.transform.GetChild(0).gameObject;
        paused = gameObject.transform.GetChild(1).gameObject;
        controls = gameObject.transform.GetChild(2).gameObject;
        options = gameObject.transform.GetChild(3).gameObject;
        youWin = gameObject.transform.GetChild(4).gameObject;
        chkptBtn = gameOver.transform.GetChild(1).gameObject;
        HUD = GameObject.FindGameObjectWithTag("HUD");
        gameOver.SetActive(false);
        paused.SetActive(false);
        controls.SetActive(false);
        options.SetActive(false);
        youWin.SetActive(false);
        delayTimer = gameOverDelay;
    }

    void Update()
    {
        if (gameOver.activeSelf)
            return;
        CheckPlayerHealth();
        GetPauseMenuInput();
        
    }

    private void GetPauseMenuInput()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePauseMenu();
        }

    }

    public void TogglePauseMenu()
    {
        if (gameOver.activeSelf || controls.activeSelf || options.activeSelf)
            return;

        if (paused.activeSelf)
        {
            paused.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            paused.SetActive(true);
            Time.timeScale = 0;
        }
            
    }

    private void CheckPlayerHealth()
    {
        if (healthManager.health <= 0)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0f)
                LoadGameOver();
        }
    }

    public void LoadGameOver()
    {
        if (checkpointManager.lastCheckpoint != null)
        {
            chkptBtn.GetComponent<Button>().gameObject.SetActive(true);
            gameOver.transform.GetComponentInChildren<EventSystem>().firstSelectedGameObject = chkptBtn;
        }
        gameOver.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void LoadLastCheckpoint()
    {
        player.SetActive(false);
        healthManager.GetComponent<Transform>().position = checkpointManager.lastCheckpoint.position;
        player.SetActive(true);
        healthManager.health = healthManager.maxHealth;
        healthbar.ActivateHealthSlots();
        healthManager.SetAlive();
        delayTimer = gameOverDelay;
        Time.timeScale = 1;
        
        gameOver.SetActive(false);
        
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        //SceneManager.LoadScene("MainMenu");
    }

    public void ShowControls()
    {
        paused.SetActive(false);
        HUD.SetActive(false);
        controls.SetActive(true);
        options.SetActive(false);
        youWin.SetActive(false);
    }

    public void ShowOptions()
    {
        paused.SetActive(false);
        HUD.SetActive(false);
        controls.SetActive(false);
        options.SetActive(true);
        youWin.SetActive(false);
    }

    public void ShowYouWin()
    {
        Time.timeScale = 0;

        paused.SetActive(false);
        HUD.SetActive(false);
        controls.SetActive(false);
        options.SetActive(false);
        youWin.SetActive(true);
    }

    public void ReturnToPauseMenu()
    {
        controls.SetActive(false);
        HUD.SetActive(true);
        paused.SetActive(true);
        options.SetActive(false);
        youWin.SetActive(false);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if ( index == SceneManager.sceneCount && index != 1 )
            return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
}
