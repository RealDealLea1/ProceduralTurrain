using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (MapGenerator))]
public class MapGenEditor : Editor {

	public override void OnInspectorGUI(){
		MapGenerator mapG = (MapGenerator)target;

		if (DrawDefaultInspector ()) {
			if (mapG.autoUpdate) {
				mapG.GenMap ();
			}
		}
		if (GUILayout.Button ("Generate")) {
			mapG.GenMap ();
		}

	}
}
