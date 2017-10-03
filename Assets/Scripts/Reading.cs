using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reading : MonoBehaviour {

	public GameObject mesh;

	private string[] file;
	private string[] verts;
	private int j=0,k=0;
	// Use this for initialization
	void Start () 
	{
		
		file = System.IO.File.ReadAllLines (@"C:\Users\breich5\Documents\cube.obj");
		for (int i=0;i<file.Length;i++) {
			if (file [i].Trim ().StartsWith ("v") && !(file [i].Trim ().StartsWith ("vn"))) {
				j++;
			}
		}
		verts = new string[j];
		for (int i=0;i<file.Length;i++) {
			if (file [i].Trim ().StartsWith ("v") && !(file [i].Trim ().StartsWith ("vn"))) {
				verts [k] = file [i];
				k++;
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKeyDown (KeyCode.P)) {
			foreach (string line in verts)
				Debug.Log (line);
		}
	}
}