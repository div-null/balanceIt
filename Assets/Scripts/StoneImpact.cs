using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneImpact : MonoBehaviour
{
    Rigidbody2D stone;
    bool isImpacted = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isImpacted)
            StartCoroutine(PlayImpact());
    }

    IEnumerator PlayImpact()
    {
        isImpacted = true;
        Game.Instance.stoneImpactAudio.Play();
        yield return new WaitForSeconds(0.4f);
        isImpacted = false;
    }
}
