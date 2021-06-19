using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    GameObject menu;
    GameObject options;
    GameObject lvlSelect;

    // Start is called before the first frame update
    void Start()
    {
        menu = transform.GetChild(0).gameObject;
        options = transform.GetChild(1).gameObject;
        lvlSelect = transform.GetChild(2).gameObject;

        showMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showMenu()
    {
        menu.SetActive(true);
        options.SetActive(false);
        lvlSelect.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        //0 is first button index
        EventSystem.current.SetSelectedGameObject(menu.transform.GetChild(0).gameObject);
    }

    public void showOptions()
    {
        menu.SetActive(false);
        options.SetActive(true);
        lvlSelect.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        //1 is second button index (back button)
        EventSystem.current.SetSelectedGameObject(options.transform.GetChild(1).gameObject);
    }

    public void showLevelSelect()
    {
        menu.SetActive(false);
        options.SetActive(false);
        lvlSelect.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        //1 is first button index
        EventSystem.current.SetSelectedGameObject(lvlSelect.transform.GetChild(1).gameObject);
    }

    public void levelOne()
    {
        SceneManager.LoadScene("Hospital");
    }

    public void levelTwo()
    {
        SceneManager.LoadScene("Factory_beta");
    }

    public void levelThree()
    {
        SceneManager.LoadScene("Forest");
    }

    public void levelFour()
    {
        SceneManager.LoadScene("Village_beta");
    }

    public void levelFive()
    {
        SceneManager.LoadScene("Mansion-b");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
