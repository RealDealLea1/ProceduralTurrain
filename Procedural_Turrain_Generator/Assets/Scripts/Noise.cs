using UnityEngine;
using System.Collections;

public static class Noise {

	public static float[,] genNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset){
		float[,] noiseMap = new float[mapWidth, mapHeight];
		System.Random rand = new System.Random (seed);
		Vector2[] octaveOffset = new Vector2[octaves];
		for (int i = 0; i < octaves; i++) {
			float offsetX = rand.Next (-100000, 100000) + offset.x;
			float offsetY = rand.Next (-100000, 100000) + offset.y;
			octaveOffset [i] = new Vector2 (offsetX, offsetY);
		}


		if (scale <= 0) {
			scale = 0.0001f;
		}
		float maxNoise = float.MinValue;//starts at min b/c chnces are the first value will be higher...creating the first max
		float minNoise = float.MaxValue;//starts at max b/c chnces are the first value will be lower...creating the first min

		float halfWidth = mapWidth / 2;
		float halfHeight = mapHeight / 2;

		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				float amplitude = 1;
				float frequency = 1;

				float noiseHeight = 0;
				for (int i = 0; i < octaves; i++) {
					float sampleX = (x - halfWidth) / scale * frequency + octaveOffset[i].x;
					float sampleY = (y - halfHeight) / scale * frequency + octaveOffset[i].y;

					float perlinVal = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinVal * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}
				if (noiseHeight > maxNoise) {
					maxNoise = noiseHeight;
				} else if (noiseHeight < minNoise) {
					minNoise = noiseHeight;
				}

				noiseMap [x,y] = noiseHeight;

			}

		}
		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				noiseMap [x, y] = Mathf.InverseLerp (minNoise, maxNoise, noiseMap [x, y]);
			}
		}


		return noiseMap;
	}
		
}
