﻿using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
		}
	}
}
