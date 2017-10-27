using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class Triangulator : MonoBehaviour {

	public GameObject mesh;

	private Vector4[] vertices;
	private float[] nums=new float[4];
	private string location;
	private int j,k;
	private List<int> triangles;

	List<int> ParseFaceLine(string faceline)
	{
		List<int> verts;
		faceline = faceline.Remove (0, 1).Trim();
		foreach (string s in faceline.Split (' ')) {
			verts.Add(int.Parse(s.Split ("/") [0]));//TODO inefficient

		}
		return verts;
		// Input "f 1//3 2//4 5//7"
		// Ouput {1, 2, 5}

	}

	List<int[]> Triangulate(List<int> verts)
	{
		// Input 1 2 3 4 5
		// Ouput { 1 2 3, 1 3 4, 1 4 5 }

		List<int[]> L;
		int v0 = verts [0];
		int v1 = verts [1];
		foreach (int v2 in verts.Skip(2))
		{
			L.Add( new int[] {v0,v1,v2} );
		}
		// Todo: make sure this works if Length of verts is 3.
		return(L);
	}

	bool isFaceLine(string s)
	{
		return s.StartsWith("f ");
	}

	// Use this for initialization
	void Start () 
	{
		string[] lines;
		//List<int[]> triangles;

		location = AssetDatabase.GetAssetPath (mesh);
		lines = System.IO.File.ReadAllLines (location);

		foreach (string s in lines) {
			if (isFaceLine(s)) {
				List<int> faceverts = ParseFaceLine(s);
				triangles.AddRange (Triangulate (faceverts));
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.O)) {
				Debug.Log (triangles);
		}
	}
}

