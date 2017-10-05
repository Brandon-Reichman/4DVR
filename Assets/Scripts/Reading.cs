using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reading : MonoBehaviour {

	public GameObject mesh;

	private string[] file;
	private string[] verts;
	private string[] numbers;
	private float[] nums;
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
		numbers = new string[j*4];
		nums = new float[j * 4];
		for (int i=0;i<file.Length;i++) {
			if (file [i].Trim ().StartsWith ("v") && !(file [i].Trim ().StartsWith ("vn"))) {
				verts [k] = file [i];
				k++;
			}
		}
		for (int i = 0; i < verts.Length; i++) {
			verts [i] = verts [i].Remove (0, 2);
		}

		//this for loop is supposed to split each component of the vertex
		for (int i = 0; i < numbers.Length; i++){
			numbers = verts [i].Split (new char[]{' '});
			nums[i] = Single.Parse(numbers[i]);;
		}

	}

	// Update is called once per frame
	void Update () 
	{


		if (Input.GetKeyDown (KeyCode.P)) {
			foreach (string line in verts)
				Debug.Log (line);
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			foreach (string line in numbers)
				Debug.Log (line);
		}
		if (Input.GetKeyDown (KeyCode.N)) {
			foreach (float line in nums)
				Debug.Log (line);
		}
	}
}