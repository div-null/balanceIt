﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUI : Singleton<GeneralUI>
{
	public GameObject gameUI;
	public GameObject mainMenu;
    public GameObject endMatchMenu;
    public GameObject pauseMenu;

    public SpriteRenderer background;
    public SpriteRenderer foreground;

    public AudioSource select;
    public Text tutorialText;
    public GameObject tutorialWindow;

    bool isHide = true;
    string accent = "#f6b778";
    string plain = "#9e6541";
    string briefing;
    

    //PAUSE MENU
    public void PressPause()
    {
        select.Play();
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void PressResume()
    {
        select.Play();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void PressRestart()
    {
        select.Play();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(Game.Instance.ExitGame());
        PressPlay();
    }
    //

    public void OpenTutorial()
    {
        select.Play();
        tutorialWindow.gameObject.SetActive(true);
        isHide = !isHide;
    }

    public void CloseTutorial()
    {
        select.Play();
        tutorialWindow.gameObject.SetActive(false);
    }

    public void ExitGame ()
	{
        select.Play();
        Application.Quit();
	}
	public void toMainMenu ()
	{
        gameUI.SetActive(false);
        endMatchMenu.SetActive(false);
		mainMenu.SetActive(true);
    }
    public void BacktoMainMenu()
    {
        select.Play();
        StartCoroutine(Game.Instance.ExitGame());
        toMainMenu();
    }

    public void PressPlay()
	{
        select.Play();
        mainMenu.SetActive(false);
        endMatchMenu.SetActive(false);
        gameUI.SetActive(true);
        gameUI.GetComponent<GameUI>().StartGame();
    }

    public void ToEndMatchMenu(int _score)
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(false);
        endMatchMenu.SetActive(true);
        endMatchMenu.GetComponent<EndMatchMenu>().UpdateInformation(_score);
    }

	public void PressCustomize ()
	{
		mainMenu.SetActive(false);
		gameUI.SetActive(false);
    }

    // Start is called before the first frame update
    void Start ()
	{
        briefing = $"Welcome to the game <color={accent}>“Balance It”</color>, where you should hold the <color={accent}>magic ball</color> on the hovering <color={accent}>island</color>" +
                        $" as long as possible and do not let it drop out. Drag horizontally to rotate the island.\n\r<color={accent}>Be careful!</color> Falling <color={accent}>rocks</color>" +
                        $" and <color={accent}>meteorites</color> will disturb you.\n\rGood luck!";
        tutorialText.text = briefing;
        toMainMenu();
		Camera cam = Camera.main;
		float screenRaio = (float)Screen.width / Screen.height;
		float bgRatio = background.bounds.size.x / background.bounds.size.y;
		float fgRatio = foreground.bounds.size.x / foreground.bounds.size.y;

		float minRatio = bgRatio > fgRatio ? fgRatio : bgRatio;
		cam.orthographicSize = foreground.bounds.size.x * (1/screenRaio ) / 2;
	}
}

