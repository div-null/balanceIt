using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private PointEffector2D explosion;

    // Start is called before the first frame update
    void Start()
    {
        explosion = this.gameObject.GetComponentInChildren<PointEffector2D>();
        explosion.forceMagnitude = 0;
    }

    private void StartExplosion()
    {
        explosion.forceMagnitude = 100;
        Destroy(this.gameObject, 0.5f);
        Debug.Log("detonate");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Player")
            StartExplosion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
