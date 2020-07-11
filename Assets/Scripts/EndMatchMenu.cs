﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMatchMenu : MonoBehaviour
{
    private Text scoreText;

    public void UpdateInformation(int _score)
    {
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
