using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUI : MonoBehaviour
{
	public GameObject gameUI;
	public GameObject mainMenu;
	public GameObject customizeMenu;
    public GameObject endMatchMenu;
    public GameObject toMainMenuButton;
    
	public void ExitGame ()
	{
		Application.Quit();
	}
	public void toMainMenu ()
	{
		gameUI.SetActive(false);
		customizeMenu.SetActive(false);
        endMatchMenu.SetActive(false);
        toMainMenuButton.SetActive(false);
		mainMenu.SetActive(true);
    }

	public void PressPlay ()
	{
		customizeMenu.SetActive(false);
		mainMenu.SetActive(false);
        toMainMenuButton.SetActive(false);
        gameUI.SetActive(true);
        toMainMenuButton.SetActive(true);
        gameUI.GetComponent<GameUI>().StartGame();
	}

    public void ToEndMatchMenu(int _score)
    {
        customizeMenu.SetActive(false);
        mainMenu.SetActive(false);
        gameUI.SetActive(false);
        toMainMenuButton.SetActive(false);
        endMatchMenu.SetActive(true);
        endMatchMenu.GetComponent<EndMatchMenu>().UpdateInformation(_score);
    }

	public void PressCustomize ()
	{
		mainMenu.SetActive(false);
		gameUI.SetActive(false);
        toMainMenuButton.SetActive(false);
        customizeMenu.SetActive(true);
        toMainMenuButton.SetActive(true);
    }

	// Start is called before the first frame update
	void Start ()
	{
		toMainMenu();
	}
}

