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
		LoadScore();
		}

	/// <summary>
	/// Устанавливает счет, если новый счет больше
	/// </summary>
	/// <param name="score"></param>
	public void SetScore (int score)
		{
		BestScore = score > BestScore ? score : BestScore;
		UpLoadScore();
		}

	/// <summary>
	/// Загружает данные из хранилища
	/// </summary>
	/// <returns></returns>
	private int LoadScore ()
		{
		string key = "score";
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
		string key = "score";
		PlayerPrefs.SetInt(key, BestScore);
			
		}

	public void Dispose ()
		{
		UpLoadScore();
		}
	}