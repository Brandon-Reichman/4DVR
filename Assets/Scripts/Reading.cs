using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Reading : MonoBehaviour {

	public GameObject mesh;
	public Material material;

	private Vector2[] uv;
	private Mesh stuff;
	private GameObject cube;
	private Vector3[] vertices;
	private string location;
	private string[] file,verts;
	private float[] nums = new float[4];
	private int j=0,k=0;
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
		vertices = new Vector3[j];
		verts = new string[j];
		uv = new Vector2[j];
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
			for (int q = 0; q < 3; q++)
				nums [q] = float.Parse (e [q]);
			vertices [i].x = nums [0];
			vertices [i].y = nums [1];
			vertices [i].z = nums [2];
			//vertices [i].w = nums [3];
		}
		/*v = String.Join ("", verts);
		numbers = v.Split (new char[]{' '});
		for (int i = 0; i < numbers.Length; i++)
			nums[i] = float.Parse(numbers[i]);*/

		for (int i = 0; i < uv.Length; i++) {
			uv [i] = new Vector2 (vertices [i].x, vertices [i].z);
		}


		stuff = new Mesh ();
		cube=new GameObject("drawn cube");
		int[] triangles=new int[] {1,0,3,1,2,3,1,5,2,1,6,2,0,3,7,0,4,7,1,5,0,1,4,0,5,6,7,5,4,7,2,6,3,2,7,3};
		cube.transform.gameObject.AddComponent<MeshRenderer> ();
		cube.transform.gameObject.AddComponent<MeshFilter> ().sharedMesh = stuff;
		cube.GetComponent<MeshRenderer> ().material = material;
		stuff.vertices = vertices;
		stuff.triangles = triangles;
		stuff.uv = uv;
		stuff.RecalculateNormals ();
		cube.transform.gameObject.GetComponent<MeshFilter> ().mesh = stuff;
		//Instantiate (cube);


	

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