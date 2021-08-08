using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneImpact : MonoBehaviour
{
    Rigidbody2D stone;
    float currentVelocity;
    bool isImpacted = false;
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isImpacted)
            StartCoroutine(PlayImpact());
    }

    IEnumerator PlayImpact()
    {
        isImpacted = true;
        
        yield return new WaitForSeconds(0.4f);
        isImpacted = false;
    }
    */
    private void Update()
    {
        if (currentVelocity > stone.velocity.magnitude + 2.5f)
            Game.Instance.stoneImpactAudio.Play();

        currentVelocity = stone.velocity.magnitude;
    }

    private void Start()
    {
        currentVelocity = -1;
        stone = gameObject.GetComponent<Rigidbody2D>();
    }
}
