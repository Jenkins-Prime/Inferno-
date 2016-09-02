using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Floor", menuName = "Scriptable Objects/Floor Data", order = 1)]
public class FloorData : ScriptableObject {
	public string floorSceneName;
	public string floorName;

	public List<LevelData> levels;
	//levels
	//level Names
	//bool isUnlocked

}

[System.Serializable]
public class LevelData {
	public string levelName;
	public bool isUnlocked = false;
	public int startTime;
}
