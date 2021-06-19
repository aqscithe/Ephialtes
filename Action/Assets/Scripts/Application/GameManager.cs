using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 1f;
    public float gameOverDelay = 1f;
    bool gameEnded = false;
    int lastScene = 0;


    private void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Debug.Log("Destroyed Extra Game Manager");
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            if (Input.GetKey(KeyCode.M))
            {
                
                LoadMainMenu();
            }
            else if(Input.GetKey(KeyCode.R))
            {
                LoadLastScene();
            }
        }
    } 

    public void LoadMainMenu()
    {
        gameEnded = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLastScene()
    {
        gameEnded = false;
        SceneManager.LoadScene(lastScene);
    }

    public void EndGame(int lastSceneIndex)
    {
        if (!gameEnded)
        {
            gameEnded = true;
            lastScene = lastSceneIndex;
            Debug.Log("GAME OVER");
            Invoke("GameOver", gameOverDelay);
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    
}
