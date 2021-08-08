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
    public Text BestText;
    public Text coinsAmountText;

    int coinsAmount;

	public void StartGame ()
	{
        Game.Instance.Initialize();
        ScoreText.text = "Score: 0";
        BestText.text = "Best: 0";
        coinsAmount = 0;
        coinsAmountText.text = "Coins: 0";
    }

	public void DrawScore (int score)
	{
		ScoreText.text = "Score: " + score;
	}

    public void IncreaseCoinsAmount()
    {
        coinsAmount++;
        coinsAmountText.text = $"Coins: {coinsAmount}";
        Game.Instance.coinsAudio.Play();
    }

    private void Update()
    {
        if (Input.anyKey)
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
                GeneralUI.Instance.PressPause();
    }
}
