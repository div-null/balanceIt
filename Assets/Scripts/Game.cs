using System;
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

    public AudioSource explosionAudio;
    public AudioSource stoneImpactAudio;

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
        ChangeScore += ChangeDifficulty;

        }

    private void ChangeDifficulty(int Score)
    {
        if (GameScore == 3)
            StartCoroutine(SpawnStones());

        if (GameScore == 8)
            StartCoroutine(SpawnMeteorites());
    }

	private void OnTriggerEnter2D (Collider2D other)
		{
		if ( other.tag == "Ball" )
			{
			Destroy(other, 1f);
			StartCoroutine(EndGame());
			}

        if (other.tag == "Meteor" || other.tag =="Stone")
            {
            Debug.Log("Meteor");
            Destroy(other.gameObject, 0.5f);
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


    IEnumerator SpawnStones()
    {
		float baseDelay = 2f;
		float minSpawnDelay = 0.3f;
		while (true)
        {
			float coeff = 1 + GameScore / 10f;
			float spawnDelay = (baseDelay / coeff < minSpawnDelay) ? minSpawnDelay : baseDelay / coeff;
			//Debug.Log($"Stone = {spawnDelay}");
			yield return new WaitForSeconds(spawnDelay);
			Instantiate(stonePrefab, new Vector3(Random.Range(-5.5f, 5.5f), Camera.main.ScreenToWorldPoint(Vector3.zero).y * -1.5f, 1), new Quaternion(0, 0, 0, 0));
        }
    }

    IEnumerator SpawnMeteorites()
    {
		float baseDelay = 3f;
		float minSpawnDelay = 0.5f;
		while (true)
        {
			float coeff = 1 + GameScore / 13f;
			float spawnDelay = (baseDelay / coeff < minSpawnDelay) ? minSpawnDelay : baseDelay / coeff;
			//Debug.Log($"Meteor = {spawnDelay}");
			yield return new WaitForSeconds(spawnDelay);
			Instantiate(meteoritePrefab, new Vector3(Random.Range(-5.5f, 5.5f), Camera.main.ScreenToWorldPoint(Vector3.zero).y * -1.5f, 1), new Quaternion(0, 0, 0, 0));
        }
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
        DestroyBombs();
        ChangeScore -= GameUI.Instance.DrawScore;
        GeneralUI.Instance.ToEndMatchMenu(GameScore);
        StopAllCoroutines();
        }

    public IEnumerator ExitGame()
    {
        //StopCoroutine(IncreaseScore());
        //StopCoroutine(SpawnMeteorites());
        //StopCoroutine(SpawnStones());
        Destroy(Platform);
        Destroy(Ball);
        World.SetActive(false);
        DestroyBombs();
        ChangeScore -= GameUI.Instance.DrawScore;
        GeneralUI.Instance.toMainMenu();
        StopAllCoroutines();
        yield return new WaitForEndOfFrame();
    }

    public void DestroyBombs()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("Meteor"))
            Destroy(item);
        foreach (var item in GameObject.FindGameObjectsWithTag("Stone"))
            Destroy(item);
    }

}


