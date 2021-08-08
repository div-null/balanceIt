using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImpact : MonoBehaviour
{
    Rigidbody2D ball;
    float currentVelocity;
    bool isImpacted = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            GameUI.Instance.IncreaseCoinsAmount();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Growth")
        {
            ball.mass += 0.05f;
            ball.gameObject.transform.localScale += new Vector3(0.05f, 0.05f, 0);
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if (currentVelocity > ball.velocity.magnitude + 2.5f)
            Game.Instance.ballImpactAudio.Play();

        currentVelocity = ball.velocity.magnitude;
    }

    private void Start()
    {
        currentVelocity = -1;
        ball = gameObject.GetComponent<Rigidbody2D>();
    }
}
