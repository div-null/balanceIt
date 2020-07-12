using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMatchMenu : Singleton<EndMatchMenu>
{
    public Text scoreText;
	public ScoreDB scoreDB = new ScoreDB();

	public void UpdateInformation(int _score)
    {
		scoreDB.Load();
		// записываю текущий счет
		scoreDB.SetScore(_score);
		int bestScore = scoreDB.BestScore;
		Debug.Log(bestScore);

		scoreText.text = "Score: " + _score;
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
