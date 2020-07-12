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

	public void StartGame ()
		{
        Game.Instance.Initialize();
        ScoreText.text = "Score: 0";
		}

	public void DrawScore (int score)
		{
		ScoreText.text = "Score: " + score;
		}
}
