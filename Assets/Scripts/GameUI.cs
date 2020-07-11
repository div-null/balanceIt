
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
	{
	public Button exitButton;
	public Button toMainMenuButton;

	public Text ScoreText;

	private void Start ()
		{
		Game.Instance.Initialize();
		}
	public void StartGame ()
		{
		ScoreText.text = "Score: 0";
		}

	public void FinishGame (int score)
		{
		// отобразить конце игры
		}

	public void DrawScore (int score)
		{
		ScoreText.text = "Score: " + score;
		}
	}
