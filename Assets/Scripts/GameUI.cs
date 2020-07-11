using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Button exitButton;
    public Button toMainMenuButton;

    public Text ScoreText;
    private int score;

    public void StartGame()
    {
        StopCoroutine(GameScore());
        ScoreText.text = "Score: 0";
        score = 0;
        StartCoroutine(GameScore());
    }

    IEnumerator GameScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            score++;
            ScoreText.text = "Score: " + score;
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
