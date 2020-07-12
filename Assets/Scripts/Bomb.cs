using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private PointEffector2D explosion;
    public GameObject _explosionAnim;
    public int _forceMagnitude = 50;
    public float _destroyDelay = 0.05f;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        explosion = this.gameObject.GetComponentInChildren<PointEffector2D>();
        explosion.forceMagnitude = 0;
    }

    private void StartExplosion()
    {

        explosion.forceMagnitude = _forceMagnitude;
        explosion.forceVariation = _forceMagnitude;
        explosion.distanceScale = 0.1f;
        var obj = Instantiate(_explosionAnim, transform.position, Quaternion.identity);

        Destroy(obj, 1);
        Destroy(this.gameObject, Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Player")
            StartExplosion();
    }
}
