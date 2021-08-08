﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    private PointEffector2D explosion;
    public GameObject _explosionAnim;
    public AudioSource _fallingAudio;
    public int _forceMagnitude = 50;
    public float _destroyDelay = 0.05f;
    Rigidbody2D body;

    public GameObject stonePrefab;
    public GameObject coinPrefab;
    public GameObject growthPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _fallingAudio = this.gameObject.GetComponentInChildren<AudioSource>();

        explosion = this.gameObject.GetComponentInChildren<PointEffector2D>();
        explosion.forceMagnitude = 0;
    }

    private void StartExplosion()
    {
        //StartCoroutine(FallEcho());
        _fallingAudio.Stop();

        //Создание камней
        int numberOfStones = Random.Range(3, 6);
        Vector3 stonePosition;
        int various;
        for (int i = 0; i < numberOfStones; i++)
        {
            stonePosition = this.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.6f, 0.6f), 0);
            various = Random.Range(1, 11);
            if (various == 1)
                Instantiate(coinPrefab, stonePosition, new Quaternion(0, 0, 0, 0));
            else if (various == 2)
                Instantiate(growthPrefab, stonePosition, new Quaternion(0, 0, 0, 0));
            else
                Instantiate(stonePrefab, stonePosition, new Quaternion(0, 0, 0, 0));
        }

        explosion.forceMagnitude = _forceMagnitude;
        explosion.forceVariation = _forceMagnitude;
        explosion.distanceScale = 0.1f;
        var obj = Instantiate(_explosionAnim, transform.position + Vector3.back, Quaternion.identity);
        Game.Instance.explosionAudio.Play();
        Destroy(obj, 1);
        Destroy(this.gameObject, Time.deltaTime);
    }

    IEnumerator FallEcho()
    {
        float echoTime = 0.3f;
        float ellapsed = 0f;
        while (ellapsed < echoTime)
        {
            _fallingAudio.volume *= 0.8f;
            yield return new WaitForEndOfFrame();
            ellapsed += Time.fixedDeltaTime;
        }
        _fallingAudio.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Player")
            StartExplosion();
    }

    public void Dispose()
    {
        _fallingAudio.Stop();
    }
}
