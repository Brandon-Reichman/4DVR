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

	List<int> ParseFaceLine(string faceline)
	{
		// Input "f 1//2 3//4 5//6"
		// Output {1,3,5}

		List<int> verts = new List<int> ();
		// Split the line on spaces to get fields, and drop the first one.
		foreach (string field in faceline.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries).Skip(1)) {
			// Each field consists of slash-separated ints.
			// The first one is the index of the vertex to use.
			// Extract that and ignore the rest.
			int idx = int.Parse(field.Split('/')[0]);
			verts.Add (idx);
		}
		Debug.Log ("Parsed vertex line to: " + string.Join (", ", verts.Select (i => i.ToString ()).ToArray ()));
		return verts;
	}

	List<int[]> Triangulate(List<int> verts)
	{
		// Input { 1, 2, 3, 4, 5 }
		// Ouput { {1,2,3}, {1,3,4}, {1,4,5} }

		// TODO: Throw an exception if verts too short
		List<int[]> tris = new List<int[]> ();
		for (int i = 2; i < verts.Count; i++) {
			tris.Add (new int[] { verts [0], verts [i - 1], verts [i] });
		}
		// TODO: change to a more readable way of making a string like 1,2,3,4 -> {1,2,3},{1,3,4}
	    Debug.Log(
			string.Join (", ", verts.Select (i => i.ToString ()).ToArray ())
			+ " -> "
			+ string.Join(", ", tris.Select (t => "{" + string.Join(", ",t.Select(i => i.ToString ()).ToArray()) + "}").ToArray())
		);
		return tris;
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

		triangles = new List<int[]> ();
		foreach (string s in lines) {
			if (isFaceLine(s)) {
				List<int> faceverts = ParseFaceLine(s);
				List<int[]> T=Triangulate(faceverts);
				triangles.AddRange (T);
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

