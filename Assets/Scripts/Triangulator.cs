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
	private List<int[]> triangles;
	private List<int> verts;
	private List<int[]> L;

	List<int> ParseFaceLine(string faceline)
	{
		faceline = faceline.Remove (0, 1).Trim();
		foreach (string s in faceline.Split(' ')) {
			Debug.Log (s);
			int b = int.Parse (s);
			Debug.Log (b);
			//verts.AddRange (int.Parse(s));
		}
		//Debug.Log("Verts: "+verts);
		return verts;
		// Input "f 1//3 2//4 5//7"
		// Ouput {1, 2, 5}

	}

	List<int[]> Triangulate(List<int> vert)
	{
		// Input 1 2 3 4 5
		// Ouput { 1 2 3, 1 3 4, 1 4 5 }

		int v0 = vert[0];
		int v1 = vert [1];
		foreach (int v2 in vert.Skip(2))
		{
			L.Add( new int[] {v0,v1,v2} );
		}
		// Todo: make sure this works if Length of verts is 3.
		Debug.Log("Triangles: "+L);
		return(L);
	}

	bool isFaceLine(string s)
	{
		return s.Trim().StartsWith("f ");
	}

	// Use this for initialization
	void Start () 
	{
		string[] lines;

		location = AssetDatabase.GetAssetPath (mesh);
		lines = System.IO.File.ReadAllLines (location);

		foreach (string s in lines) {
			if (isFaceLine(s)) {
				List<int> faceverts = ParseFaceLine(s);
				//Debug.Log (faceverts);
				//List<int[]> T=Triangulate(faceverts);
				//triangles.AddRange (T);
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.P)) {
		}
		if (Input.GetKey (KeyCode.O)) {
			triangles.ForEach (Console.WriteLine);
		}
	}
}

