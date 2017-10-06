using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Reading : MonoBehaviour {

	public GameObject mesh;

	private string v,location;
	private string[] file,verts,numbers;
	private float[] nums;
	private int j=0,k=0;
	// Use this for initialization
	void Start () 
	{
		location = AssetDatabase.GetAssetPath (mesh);
		file = System.IO.File.ReadAllLines (location);
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
				verts [k] = file [i].Remove(0,2);
				verts [k] = verts [k].PadRight (verts [k].Length+1, ' ');
				k++;
			}
		}
		v = String.Join ("", verts);
		for (int i = 0; i < numbers.Length; i++){
		  	numbers = v.Split (new char[]{' '});
			nums[i] = float.Parse(numbers[i]);
		}

	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.V)) 
				Debug.Log (v);

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