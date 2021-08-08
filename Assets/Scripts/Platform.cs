using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Span
	{
	public float min;
	public float max;
	}

public class Platform : MonoBehaviour
{
	public Camera camera;
	public Rigidbody2D platform;
	private float? input;
	private float deltaPush;
	private float prevMousePos;
	static Vector2 screenCenter;

	[SerializeField]
	private Span angleBoudaries = new Span() { min = -30, max = 30 };

	[SerializeField]
	private Span mouseBoudaries = new Span() { min = -300, max = 300 };

	[SerializeField]
	private float forcePush=2f;

	[SerializeField]
	private float pushDeadZone = 0.2f;

	[SerializeField]
	private float staticAngularDrag = 5f;

	[SerializeField]
	private float maxStaticAngularVelocity = 60;

    [SerializeField]
    private float sensivity = 5;

	private void Start()
    {
		camera = Camera.main;
		screenCenter = new Vector2(Screen.width/ 2, Screen.height/ 2);
		platform = transform.GetComponentInChildren<Rigidbody2D>();
    }

    private void Update()
    {
		float newInput = 0;

        //Управление с помощью стика на геймпаде
        //float stickAngle;
        //float deltaStickAngle;
        if (Input.GetAxis("gamepad_vertical") > 0.1f)
        {
            //stickAngle = (Mathf.Acos(Input.GetAxis("gamepad_horizontal")) * 180 / Mathf.PI - 90) * -1;
            //stickAngle = (Mathf.Acos(Input.GetAxis("gamepad_horizontal")) - Mathf.PI / 2) * -1;
            //deltaStickAngle = Mathf.Abs(platform.transform.rotation.z - stickAngle);
            //Debug.Log(stickAngle * 180 / Mathf.PI + "  rotation: " + platform.transform.rotation.z + "  delta degrees: " + deltaStickAngle);
            //platform.AddTorque(deltaStickAngle, ForceMode2D.Impulse);

            //platform.rotation = stickAngle * 180 / Mathf.PI;

            /*
            // С применением физики
            if (platform.transform.rotation.z * 0.99f < stickAngle)
            {
                platform.AddTorque(deltaStickAngle * 15, ForceMode2D.Impulse);
                Debug.Log("Up");
            }
            else if (platform.transform.rotation.z * 0.99f > stickAngle)
            {
                platform.AddTorque(-deltaStickAngle * 15, ForceMode2D.Impulse);
                Debug.Log("Down");
            }
            else
                platform.angularDrag = staticAngularDrag;
            */
        }
        //

        /*
        //Управление с помощью мыши (по окружности)
        if (Input.GetMouseButton(1))
        {
            float radius;
            float angle;
            Vector2 mousePos;
            mousePos = mouseToScreen(Input.mousePosition);
            radius = mousePos.magnitude;

            angle = Vector2.Angle(mousePos, Vector2.up);

            float platformRotationZ = platform.transform.localRotation.z * 180 / Mathf.PI;
            float deltaAngle = Mathf.Abs(angle - platformRotationZ);

            Debug.Log("mouse angle = " + angle + " platform angle = " + platformRotationZ + " deltaAngle = " + deltaAngle);

            platform.rotation = angle * Mathf.Sign(mousePos.x);
            
            /*
            if (platformRotationZ * 0.9f > angle)
            {
                platform.AddTorque(deltaAngle, ForceMode2D.Impulse);
            }
            else if (platformRotationZ * 0.9f < angle)
            {
                platform.AddTorque(-deltaAngle, ForceMode2D.Impulse);
            }
            else
                platform.angularDrag = staticAngularDrag;
            */
            /*
        }
        */
        //Управление с помощью мыши (по прямой)
        if ( Input.GetMouseButton(0) )
		{
		    newInput = getMousePosition().x;
		    //Debug.Log(newInput);

		    //if ( newInput > mouseBoudaries.max )
		    //	newInput = mouseBoudaries.max;

		    //if ( newInput < mouseBoudaries.min )
		    //	newInput = mouseBoudaries.min;

		    if ( input == null )
		    	{
		    	input = newInput;
		    	return;
		    }
		    deltaPush += newInput - (float)input;
		    input = newInput;
		}

		// кнопка отжата
		if ( Input.GetMouseButtonUp(0))
		{
		newInput = 0;
		input = null;
		deltaPush = 0;
		}

        if (Mathf.Abs(deltaPush) < pushDeadZone)
        {
            platform.angularDrag = staticAngularDrag;
        }
        else
        {
            deltaPush = deltaPush / 10 * sensivity;
            // измеряю трение вращения платформы от величины перемещения
            platform.angularDrag = staticAngularDrag / Mathf.Abs(deltaPush);

            // прилагаю момент инерции
            platform.AddTorque(forcePush * deltaPush, ForceMode2D.Impulse);
            //Если сделать квадрат deltaPush (forcePush * Mathf.Abs(deltaPush) * deltaPush), то при быстром свайпе поворот платформы > поворота при медленном при преодолении того же расстояния

            // измеряю максимальную угловую скорость платформы от величины перемещения
            float maxAngularVelocity = maxStaticAngularVelocity * Mathf.Sqrt(Mathf.Abs(deltaPush));
            if (Mathf.Abs(platform.angularVelocity) > maxAngularVelocity)
                platform.angularVelocity = maxAngularVelocity * Math.Sign(platform.angularVelocity);
        }
    }

	private void FixedUpdate ()
		{
		


		if ( platform.rotation > angleBoudaries.max )
			{
			float coeff = 1+ deltaAngle(platform.rotation, angleBoudaries.max) / 30;
			platform.AddTorque(-4 * coeff * platform.angularDrag * forcePush, ForceMode2D.Force);
			//platform.rotation = angleBoudaries.max;
			}

		if ( platform.rotation < angleBoudaries.min )
			{
			float coeff = 1 + deltaAngle(platform.rotation, angleBoudaries.min) / 30;
			platform.AddTorque(4 * coeff * platform.angularDrag * forcePush, ForceMode2D.Force);
			//platform.rotation = angleBoudaries.min;
			}

		deltaPush = 0;
		}

	public static Vector2 getMousePosition ()
		{
		return mouseToScreen(Input.mousePosition);
		}

	public static Vector2 mouseToScreen(Vector2 mouseCoords)
		{
		return new Vector2(mouseCoords.x - screenCenter.x, -mouseCoords.y + screenCenter.y);
		}

	public static float deltaAngle (float one, float two) => Mathf.Abs(one) - Mathf.Abs(two);
	}
