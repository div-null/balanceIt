using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMatchMenu : Singleton<EndMatchMenu>
{
    public Text scoreText;
    public Text bestScoreText;
	public ScoreDB scoreDB = new ScoreDB();

    public Text CustomTitleAfterGameText;

    public void UpdateInformation(int _score)
    {
		scoreDB.Load();
		// записываю текущий счет
		bool isRecord = scoreDB.SetScore(_score);
		int bestScore = scoreDB.BestScore;

        SetCustomTitleAAfterGame(isRecord);
        bestScoreText.text = bestScore.ToString();
        scoreText.text = _score.ToString();
    }

    public void SetCustomTitleAAfterGame(bool isNewRecord)
    {
        Color recordColor = new Color(1f, 0.753f, 0.127f, 1f);
        Color gameOverColor = new Color(0f, 0.832f, 1f, 1f);

        if (isNewRecord)
        {
            CustomTitleAfterGameText.text = "New High Score!";
            CustomTitleAfterGameText.color = recordColor;
            scoreText.color = recordColor;
            bestScoreText.color = recordColor;
            CustomTitleAfterGameText.GetComponent<Animator>().SetInteger("active", 0);
        }
        else
        {
            CustomTitleAfterGameText.text = "GAME OVER";
            CustomTitleAfterGameText.color = gameOverColor;
            scoreText.color = Color.white;
            bestScoreText.color = Color.white;
            CustomTitleAfterGameText.GetComponent<Animator>().SetInteger("active", 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
