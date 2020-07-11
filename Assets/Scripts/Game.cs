using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	private int gameScore;
	public int GameScore { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }
	// Update is called once per frame
	void Update()
    {
        
    }
	IEnumerable StartGame ()
	{

	yield return new WaitForSeconds(1f);
	}
	IEnumerable EndGame ()
	{
	yield return new WaitForSeconds(1f);
	}

	IEnumerable IncreaseScore ()
	{
	yield return new WaitForSeconds(1f);
	}

}
