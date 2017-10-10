using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Reading : MonoBehaviour {

	public GameObject mesh;

	private Vector4[] vertices;
	private string v,location;
	private string[] file,verts;
	private float[] nums = new float[4];
	private int j=0,k=0,l=0;
	// Use this for initialization
	void Start () 
	{
		location = AssetDatabase.GetAssetPath (mesh);
		file = System.IO.File.ReadAllLines (location);
		//counting verts to allocate array size
		for (int i=0;i<file.Length;i++) {
			if (file [i].Trim ().StartsWith ("v") && !(file [i].Trim ().StartsWith ("vn"))) {
				j++;
			}
		}
		vertices = new Vector4[j];
		verts = new string[j];
		//loading vertex value from file
		for (int i=0;i<file.Length;i++) {
			if (file [i].Trim ().StartsWith ("v") && !(file [i].Trim ().StartsWith ("vn"))) {
				verts [k] = file [i].Remove(0,2);
				verts [k] = verts [k].PadRight (verts [k].Length+1, ' ');
				k++;
			}
		}
		//most absurd loding of vectors possible...
		for (int i = 0; i < j; i++) {
			string p = verts[i];
			string[] e = p.Split (new char[]{ ' ' });
			for (int q = 0; q < 4; q++)
				nums [q] = float.Parse (e [q]);
			vertices [i].x = nums [l];
			vertices [i].y = nums [l+1];
			vertices [i].z = nums [l+2];
			vertices [i].w = nums [l+3];
		}
		/*v = String.Join ("", verts);
		numbers = v.Split (new char[]{' '});
		for (int i = 0; i < numbers.Length; i++)
			nums[i] = float.Parse(numbers[i]);*/
		}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			foreach (string line in verts)
				Debug.Log (line);
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			foreach (Vector4 v in vertices)
				Debug.Log (v);
		}
	}
}