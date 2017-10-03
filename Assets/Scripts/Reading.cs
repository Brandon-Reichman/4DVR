using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reading : MonoBehaviour {

	private string[] file;
	// Use this for initialization
	void Start () 
	{
		file = System.IO.File.ReadAllLines (@"C:\Users\breich5\Documents\cube.txt");
		//System.IO.File.Move ("cube.obj", "cube.txt");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			foreach (string line in file)
				Debug.Log ("\t" + line);
		}
	}
}
