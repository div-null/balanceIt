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

    private void Start()
    {
		camera = Camera.main;
		screenCenter = new Vector2(Screen.width/ 2, Screen.height/ 2);
		platform = transform.GetComponentInChildren<Rigidbody2D>();
    }

    private void Update()
    {
		float newInput = 0;
		if ( Input.GetMouseButton(0) )
			{
			newInput = getMousePosition().x;

			if ( newInput > mouseBoudaries.max )
				newInput = mouseBoudaries.max;

			if ( newInput < mouseBoudaries.min )
				newInput = mouseBoudaries.min;

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
    }

	private void FixedUpdate ()
		{
		platform.AddTorque(forcePush * deltaPush, ForceMode2D.Force);

		if ( platform.rotation > angleBoudaries.max )
			{
			float coeff = 1+ deltaAngle(platform.rotation, angleBoudaries.max) / 30;
			platform.AddTorque(-4 * coeff * forcePush, ForceMode2D.Force);
			//platform.rotation = angleBoudaries.max;
			}

		if ( platform.rotation < angleBoudaries.min )
			{
			float coeff = 1 + deltaAngle(platform.rotation, angleBoudaries.min) / 30;
			platform.AddTorque(4 * coeff * forcePush, ForceMode2D.Force);
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
