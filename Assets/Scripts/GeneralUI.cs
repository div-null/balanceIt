using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUI : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject mainMenu;
    public GameObject customizeMenu;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void toMainMenu()
    {
        gameUI.SetActive(false);
        customizeMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void PressPlay()
    {
        customizeMenu.SetActive(false);
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        gameUI.GetComponent<GameUI>().StartGame();
    }

    public void PressCustomize()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(false);
        customizeMenu.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        toMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
