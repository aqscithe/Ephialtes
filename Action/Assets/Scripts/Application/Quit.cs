using UnityEngine.SceneManagement;
using UnityEngine;

public class Quit : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Q))
            Application.Quit();
        }
    }

    public void quitApp()
    {
        Application.Quit();
    }
}
