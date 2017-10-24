using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Triangulator : MonoBehaviour {

	public GameObject mesh;

	private Vector4[] vertices;
	private string[] file,faces;
	private float[] nums=new float[4];
	private string location;
	private int j,k;

	// Use this for initialization
	void Start () 
	{
		location = AssetDatabase.GetAssetPath (mesh);
		file = System.IO.File.ReadAllLines (location);

		for (int i = 0; i < file.Length; i++) {
			if (file [i].Trim ().StartsWith ("f")){
				j++;}}
		nums = new float[j * 4];
		faces = new string[j];
		vertices = new Vector4[j];
		for (int i = 0; i < file.Length; i++) {
			if (file [i].Trim ().StartsWith ("f")){
				faces [k] = file [i].Remove(0,2);;
				k++;}}
		for (int i = 0; i < j; i++) {
			string p = faces[i];
			string[] e = p.Split (' ','/');
			Debug.Log (e [i]);
			for (int q = 0; q < 4; q++)
				nums [q] = float.Parse (e [q]);
			vertices [i].x = nums [0];
			vertices [i].y = nums [1];
			vertices [i].z = nums [2];
			vertices [i].w = nums [3];
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.O)) {
			foreach (Vector4 line in vertices)
				Debug.Log (line);
		}
	}
}
