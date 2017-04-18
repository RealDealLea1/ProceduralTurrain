using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfiniteTerrain : MonoBehaviour {

	public const float maxViewDistance = 450; //must be larger than 240 
	public Transform player;

	public static Vector2 playerPos; //CHANGE TO VECTOR 3 LATER
	int chunkSize; //keep in mind actual chunksize ( x - 1)
	int chunksVisible;

	Dictionary<Vector2,TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
	List<TerrainChunk> terrainChunksVisibleLastUpdate = new List <TerrainChunk>();

	void Start(){
		chunkSize = MapGenerator.mapChunkSize - 1;
		chunksVisible = Mathf.RoundToInt (maxViewDistance / chunkSize);
	}

	void Update(){
		playerPos = new Vector2 (player.position.x, player.position.z);
		UpdateVisibleChunks ();
	}

	void UpdateVisibleChunks(){

		for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++) {
			terrainChunksVisibleLastUpdate [i].setVisible (false);
		}
		terrainChunksVisibleLastUpdate.Clear ();

		int currentChunkCoordX = Mathf.RoundToInt (playerPos.x / chunkSize);
		int currentChunkCoordY = Mathf.RoundToInt (playerPos.y / chunkSize);

		for (int yOffset = -chunksVisible; yOffset <= chunksVisible; yOffset++) {
			for (int xOffset = -chunksVisible; xOffset <= chunksVisible; xOffset++) {
				Vector2 viewedChunkCoord = new Vector2 (currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

				if (terrainChunkDictionary.ContainsKey (viewedChunkCoord)) {
					terrainChunkDictionary [viewedChunkCoord].UpdateTerrainChunk ();
					if (terrainChunkDictionary [viewedChunkCoord].isVisible ()) {
						terrainChunksVisibleLastUpdate.Add (terrainChunkDictionary [viewedChunkCoord]);
					}
				} else {
					terrainChunkDictionary.Add (viewedChunkCoord, new TerrainChunk (viewedChunkCoord,chunkSize,transform));
				}

			}
		}
				



	}

	public class TerrainChunk{
		GameObject meshObj;
		Vector2 position;
		Bounds bounds;

		public TerrainChunk(Vector2 coord , int size, Transform parent){
			position = coord * size;
			bounds = new Bounds(position, Vector2.one *size);
			Vector3 positionV3 = new Vector3 (position.x, 0, position.y);

			meshObj = GameObject.CreatePrimitive(PrimitiveType.Plane);
			meshObj.transform.position = positionV3;
			meshObj.transform.localScale = Vector3.one * size/10f;
			meshObj.transform.parent = parent;
			setVisible(false);
		}

		public void UpdateTerrainChunk(){
			float viewDistance = Mathf.Sqrt (bounds.SqrDistance (playerPos));
			bool visible = viewDistance <= maxViewDistance;
			setVisible (visible);
		}

		public void setVisible(bool visible){
			meshObj.SetActive (visible);
		}

		public bool isVisible(){
			return meshObj.activeSelf;
		}
	}

}
	


