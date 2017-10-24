using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MeshLoader : MonoBehaviour {

	public GameObject mesh;
	public Material material;

	private Renderer rend;
	private Vector2[] uv;
	private Mesh stuff;
	private GameObject cube;
	private Vector4[] tangents;
	private Vector3[] vertices;
	private string location;
	private string[] file,faces,verts;
	private float[] nums = new float[4];
	private int j=0,k=0;
	private Shader Ortho,Stereo;
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
		tangents = new Vector4[j];
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
		//loading vectors
		for (int i = 0; i < j; i++) {
			string p = verts[i];
			string[] e = p.Split (new char[]{ ' ' });
			for (int q = 0; q < 4; q++)
				nums [q] = float.Parse (e [q]);
			vertices [i].x = nums [0];
			vertices [i].y = nums [1];
			vertices [i].z = nums [2];
			tangents [i].x = nums [3];
			uv [i] = new Vector2 (vertices [i].x, vertices [i].z);
		}



		Vector3 pos = new Vector3 (0, -0.25f,2);
		stuff = new Mesh ();
		cube=new GameObject("drawn cube");
		int[] triangles=new int[] {0,1,3,3,1,2,4,0,3,4,3,7,3,6,7,2,6,3,5,6,2,2,1,5,4,7,6,6,5,4,0,4,1,1,4,5
			,8,9,11,11,9,10,12,8,11,12,11,15,11,14,15,10,14,11,13,14,10,10,9,13,12,15,14,14,13,12,8,12,9,9,12,13,
			2,1,9,9,10,2,10,11,2,2,11,3,0,3,11,11,8,0,1,0,8,8,9,1,
			5,13,9,5,9,1,2,10,6,6,10,14,6,14,13,13,5,6,
			6,14,7,7,14,15,15,12,7,7,12,4,4,12,13,13,5,4,
			0,8,12,12,4,0,7,15,3,3,15,11};
		cube.transform.gameObject.AddComponent<MeshRenderer> ();
		cube.transform.gameObject.AddComponent<MeshFilter> ().sharedMesh = stuff;
		cube.AddComponent(Type.GetType("InitFDTransform"));
		cube.GetComponent<MeshRenderer> ().material = material;
		cube.transform.localPosition = pos;
		stuff.vertices = vertices;
		stuff.triangles = triangles;
		stuff.uv = uv;
		stuff.RecalculateNormals();
		stuff.tangents = tangents;
		}

	/*void Update()
	{
		if (OVRInput.Get (OVRInput.RawButton.LThumbstick))
			rend.material.shader = Shader.Find ("FDStandard");
		if (OVRInput.Get (OVRInput.RawButton.RThumbstick))
			rend.material.shader = Shader.Find ("StereographicStandard");
	}*/
}