﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider2D))]
public class Game : Singleton<Game>
	{
	public event Action<int> ChangeScore;
	//мертвая зона для мяча
	public Collider2D deadZone;

    public GameObject stonePrefab;
    public GameObject meteoritePrefab;

    [SerializeField]
	public GameObject World;

    public GameObject PlatformPrefab;
    public GameObject BallPrefrab;

    public GameObject Platform;
    public GameObject Ball;

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
        // поставить палку
        // заспавнить мяч

        //Не влияет на переход в MainMenu
        GameScore = 0;
        StartCoroutine(StartGame());
		ChangeScore += GameUI.Instance.DrawScore;
        Platform = Instantiate(PlatformPrefab, World.transform);
        Ball = Instantiate(BallPrefrab, World.transform);
    }

	private void Start ()
		{
		World.SetActive(false);
		deadZone = GetComponent<Collider2D>();
		}

	private void OnTriggerEnter2D (Collider2D other)
		{
        Debug.Log("wasd");
		if ( other.tag == "Ball" )
			{
			Destroy(other, 1f);
			StartCoroutine(EndGame());
			}

        if (other.tag == "Meteor" || other.tag =="Stone")
            Destroy(other, 0.5f);
        }

	IEnumerator IncreaseScore ()
		{
		while ( true )
			{
			yield return new WaitForSeconds(1f);
			GameScore++;
            if (GameScore > 5)
                StartCoroutine(SpawnStones());

            if (GameScore > 12)
                StartCoroutine(SpawnMeteorites());
            }
		}


    IEnumerator SpawnStones()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(stonePrefab, new Vector3(Random.Range(-6f, 6f), Camera.main.ScreenToWorldPoint(Vector3.zero).y * -1.25f, 0), new Quaternion(0, 0, 0, 0));
    }

    IEnumerator SpawnMeteorites()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(meteoritePrefab, new Vector3(Random.Range(-6f, 6f), Camera.main.ScreenToWorldPoint(Vector3.zero).y * -1.25f, 0), new Quaternion(0, 0, 0, 0));
    }

    public IEnumerator StartGame ()
		{
		StopCoroutine(IncreaseScore());
		World.SetActive(true);
		yield return new WaitForSeconds(1f);
		StartCoroutine(IncreaseScore());
		}

	public IEnumerator EndGame()
		{
		yield return new WaitForEndOfFrame();
        //StopCoroutine(IncreaseScore());
        //StopCoroutine(SpawnMeteorites());
        //StopCoroutine(SpawnStones());
        Destroy(Platform);
        Destroy(Ball);
        World.SetActive(false);
        ChangeScore -= GameUI.Instance.DrawScore;
        GeneralUI.Instance.ToEndMatchMenu(GameScore);
        StopAllCoroutines();
        }

    public IEnumerator ExitGame()
    {
        yield return new WaitForEndOfFrame();
        //StopCoroutine(IncreaseScore());
        //StopCoroutine(SpawnMeteorites());
        //StopCoroutine(SpawnStones());
        Destroy(Platform);
        Destroy(Ball);
        World.SetActive(false);
        ChangeScore -= GameUI.Instance.DrawScore;
        GeneralUI.Instance.toMainMenu();
        StopAllCoroutines();
    }

}


