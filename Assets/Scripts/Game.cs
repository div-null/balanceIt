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
    public GameObject coinPrefab;
    public GameObject growthPrefab;
    public GameObject fragmentPrefab;

    [SerializeField]
	public GameObject World;

    public GameObject PlatformPrefab;
    public GameObject BallPrefrab;

    public GameObject Platform;
    public GameObject Ball;

    public AudioSource explosionAudio;
    public AudioSource stoneImpactAudio;
    public AudioSource ballImpactAudio;
    public AudioSource coinsAudio;


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
        //Не влияет на переход в MainMenu
        GameScore = 0;
        StartCoroutine(StartGame());
		ChangeScore += GameUI.Instance.DrawScore;
        // поставить палку
        Platform = Instantiate(PlatformPrefab, World.transform);
        // заспавнить мяч
        Ball = Instantiate(BallPrefrab, World.transform);
    }

	private void Start ()
	{
        UnityEngine.Cursor.visible = false;
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

       if (other.tag == "Meteor" || other.tag == "Fragment" || other.tag =="Stone" || other.tag == "Coin" || other.tag == "Growth")
       {
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
		float minSpawnDelay = 1f;
		while (true)
        {
			float coeff = 1 + GameScore / 5f;
			float spawnDelay = (baseDelay / coeff < minSpawnDelay) ? minSpawnDelay : baseDelay / coeff;
			//Debug.Log($"Stone = {spawnDelay}");
			yield return new WaitForSeconds(spawnDelay);

            int various = Random.Range(1, 11);

            if (various == 1)
                Instantiate(coinPrefab, new Vector3(Random.Range(-5.5f, 5.5f), Camera.main.ScreenToWorldPoint(Vector3.zero).y * -1.5f, 1), new Quaternion(0, 0, 0, 0));
            else if (various == 2)
                Instantiate(growthPrefab, new Vector3(Random.Range(-5.5f, 5.5f), Camera.main.ScreenToWorldPoint(Vector3.zero).y * -1.5f, 1), new Quaternion(0, 0, 0, 0));
            else
                Instantiate(stonePrefab, new Vector3(Random.Range(-5.5f, 5.5f), Camera.main.ScreenToWorldPoint(Vector3.zero).y * -1.5f, 1), new Quaternion(0, 0, 0, 0));
        }
    }

    IEnumerator SpawnMeteorites()
    {
		float baseDelay = 3f;
		float minSpawnDelay = 3f;
        int various = 0;
        while (true)
        {
			float coeff = 1 + GameScore / 20f;
			float spawnDelay = (baseDelay / coeff < minSpawnDelay) ? minSpawnDelay : baseDelay / coeff;
			//Debug.Log($"Meteor = {spawnDelay}");
			yield return new WaitForSeconds(spawnDelay);

            if (GameScore > 30)
                various = Random.Range(1, 11);

            if (various < 8)
                Instantiate(meteoritePrefab, new Vector3(Random.Range(-5.5f, 5.5f), Camera.main.ScreenToWorldPoint(Vector3.zero).y * -1.5f, 1), new Quaternion(0, 0, 0, 0));
            else
                Instantiate(fragmentPrefab, new Vector3(Random.Range(-5.5f, 5.5f), Camera.main.ScreenToWorldPoint(Vector3.zero).y * -1.5f, 1), new Quaternion(0, 0, 0, 0));
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


