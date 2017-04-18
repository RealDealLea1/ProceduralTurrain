using UnityEngine;
using System.Collections;

public class MeshData{
	public Vector3[] verticies;
	public int[] triangles;
	public Vector2[] uvs; 

	int triangleIndex;

	public MeshData (int meshWidth, int meshHeight){
		verticies = new Vector3[meshWidth * meshHeight];
		uvs = new Vector2[meshWidth * meshHeight];
		triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6 ]; 
	}

	public void addTriangle(int x, int y, int z){
		triangles [triangleIndex] = x;
		triangles [triangleIndex + 1] = y;
		triangles [triangleIndex + 2] = z;
		triangleIndex += 3;
	}

	public Mesh CreateMesh(){
		Mesh mesh = new Mesh ();
		mesh.vertices = verticies;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateNormals ();
		return mesh;

	}

}
