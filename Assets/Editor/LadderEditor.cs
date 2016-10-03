using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Ladder))]
public class LadderScriptEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		Ladder ladder = (Ladder)target;
		if(GUILayout.Button("Build Ladder")) {
			ladder.BuildLadder();
		}
	}
}
