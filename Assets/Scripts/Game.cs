using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Game : Singleton<Game>
	{
	public event Action<int> ChangeScore;
	//мертвая зона для мяча
	public Collider2D deadZone;

	[SerializeField]
	public GameObject World;

	private int gameScore;
	public int GameScore
		{
		get
			{
			return gameScore;
			}

		set
			{
			gameScore = value;
			ChangeScore?.Invoke(gameScore);
			}
		}
	public void Initialize ()
		{
		ChangeScore += GameUI.Instance.DrawScore;
		// поставить палку
		// заспавнить мяч
		StartCoroutine(StartGame());
		}

	private void Start ()
		{
		World.SetActive(false);
		deadZone = GetComponent<Collider2D>();
		}

	private void OnTriggerEnter2D (Collider2D other)
		{
		Debug.Log("Trigger enter");
		if ( other.tag == "ball" )
			{
			Destroy(other, 1f);
			StartCoroutine(EndGame());
			}
		
		}

	IEnumerator IncreaseScore ()
		{
		while ( true )
			{
			yield return new WaitForSeconds(1f);
			GameScore++;
			}
		}

	IEnumerator StartGame ()
		{
		StopCoroutine(IncreaseScore());
		World.SetActive(true);
		yield return new WaitForSeconds(1f);
		StartCoroutine(IncreaseScore());
		}

	IEnumerator EndGame ()
		{
		yield return new WaitForEndOfFrame();
		StopCoroutine(IncreaseScore());
		World.SetActive(false);
		ChangeScore -= GameUI.Instance.DrawScore;
		GameUI.Instance.FinishGame(gameScore);
		}

	}


