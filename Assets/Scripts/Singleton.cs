using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : MonoBehaviour
{
	static T _instance;
	public static T Instance { get
			{
			if ( _instance == null )
				_instance = GameObject.FindObjectOfType<T>();
			return _instance;
			} }
}
