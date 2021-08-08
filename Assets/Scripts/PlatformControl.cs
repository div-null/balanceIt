using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{
    private GameObject platform;
    private Vector2 rotation;
    private int angleZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        platform = this.gameObject;
    }

    //Стик наклонен вниз - angle z = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("gamepad_horizontal") != 0)
            Debug.Log(180 / Mathf.PI * Mathf.Atan(Input.GetAxis("gamepad_vertical") / Input.GetAxis("gamepad_horizontal")));
        //if (Input.GetAxis("gamepad_vertical") > 0.5f)
    }
}
