using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private PointEffector2D explosion;
    public int _forceMagnitude = 90;
    public float _destroyDelay = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        explosion = this.gameObject.GetComponentInChildren<PointEffector2D>();
        explosion.forceMagnitude = 0;
    }

    private void StartExplosion()
    {
        explosion.forceMagnitude = _forceMagnitude;
        explosion.forceVariation = _forceMagnitude / 2;
        explosion.distanceScale = 0.1f;
        Destroy(this.gameObject, Time.deltaTime);
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
