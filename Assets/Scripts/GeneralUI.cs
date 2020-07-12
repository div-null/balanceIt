using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUI : Singleton<GeneralUI>
{
	public GameObject gameUI;
	public GameObject mainMenu;
	public GameObject customizeMenu;
    public GameObject endMatchMenu;
    public GameObject toMainMenuButton;
    public GameObject world;

    public SpriteRenderer background;
    public SpriteRenderer foreground;




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
        //world.SetActive(false);
		mainMenu.SetActive(true);
    }
    public void BacktoMainMenu()
    {
        StartCoroutine(Game.Instance.ExitGame());
        toMainMenu();
    }

    public void PressPlay ()
	{
		customizeMenu.SetActive(false);
		mainMenu.SetActive(false);
        toMainMenuButton.SetActive(false);
        endMatchMenu.SetActive(false);
        //world.SetActive(true);
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
		Camera cam = Camera.main;
		float screenRaio = (float)Screen.width / Screen.height;
		float bgRatio = background.bounds.size.x / background.bounds.size.y;
		float fgRatio = foreground.bounds.size.x / foreground.bounds.size.y;

		float minRatio = bgRatio > fgRatio ? fgRatio : bgRatio;
		cam.orthographicSize = foreground.bounds.size.x * (1/screenRaio ) / 2;
		}
}

