using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreDB : IDisposable
	{
	public int BestScore = 0;

	public void Load ()
		{
		BestScore = LoadScore();
		}

	/// <summary>
	/// Устанавливает счет, если новый счет больше
	/// </summary>
	/// <param name="score"></param>
	public bool SetScore (int score)
	{
		bool isScoreBestScore = score > BestScore ? true : false;
		BestScore = score > BestScore ? score : BestScore;
		UpLoadScore();

		return isScoreBestScore;
	}

	/// <summary>
	/// Загружает данные из хранилища
	/// </summary>
	/// <returns></returns>
	private int LoadScore ()
		{
		string key = "BestScore";
		int score = 0;
		if ( PlayerPrefs.HasKey(key) )
			score = PlayerPrefs.GetInt(key);
		return score;
		}

	/// <summary>
	/// Выгружает данные в хранилище
	/// </summary>
	private void UpLoadScore ()
	{
		string key = "BestScore";
		PlayerPrefs.SetInt(key, BestScore);
		PlayerPrefs.Save();
	}

	public void Dispose ()
		{
		UpLoadScore();
		}
	}