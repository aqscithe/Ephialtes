using UnityEngine.SceneManagement;
using UnityEngine;

public class Restart : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (SceneManager.GetActiveScene().name != "GameOver")
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else
                SceneManager.LoadScene(0);
        }
            
    }
}
