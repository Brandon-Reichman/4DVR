using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TetrahedronMesh : MonoBehaviour {

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
		int[] triangles=new int[] {0,1,2,1,2,3,0,1,3,0,2,3,
		0,1,4
		,1,2,4,
		0,2,4,
			0,3,4,
			1,3,4,
			2,3,4
		};
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