using UnityEngine;
using System.Collections;

public static class MeshGenerator {

	//heightCurveEnabled allows for "shallower" water ..if true

	public static MeshData GenerateTerrainMesh(float[,] heightMap , float heightMult, AnimationCurve heightCurve, bool heightCurveEnabled, int levelOfDetail){
		int width = heightMap.GetLength (0);
		int height = heightMap.GetLength (1);
		float topLeftX = (width - 1) / -2f;
		float topLeftZ = (height - 1) / 2f;

		int meshSimplification = (levelOfDetail == 0)?1: levelOfDetail * 2;
		int verticiesPerLine = (width - 1) / meshSimplification + 1;

		MeshData meshData = new MeshData (width,height);
		int vertexIndex = 0;

		for (int y = 0; y < height; y+= meshSimplification) {
			for (int x = 0; x < width; x+= meshSimplification) {
				//creating verticies
				if (heightCurveEnabled) {
					meshData.verticies [vertexIndex] = new Vector3 (topLeftX + x, heightCurve.Evaluate (heightMap [x, y]) * heightMult, topLeftZ - y);
				} else {
					meshData.verticies [vertexIndex] = new Vector3 (topLeftX + x, heightMap [x, y] * heightMult, topLeftZ - y);

				}


				meshData.uvs [vertexIndex] = new Vector2 (x / (float)width, y / (float)height);

				if ( x < width - 1 && y <height -1){
					meshData.addTriangle (vertexIndex, vertexIndex + verticiesPerLine + 1, vertexIndex + verticiesPerLine);
					meshData.addTriangle (vertexIndex + verticiesPerLine + 1, vertexIndex, vertexIndex + 1);
				}

				vertexIndex++;
			}
		}
		return meshData;
	}
}

