﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

	[SerializeField]
	private Transform[] backgrounds; // things to be parallaxed
	private float[] parallaxScales; // proportion of the camera's movement to move the backgrounds by.
	[SerializeField]
	private float smoothing = 1f; // how smooth the parallax is going to be. (Make sure to set above 0.)

	private Transform cam; // reference to main camera's transform.
	private Vector3 previousCamPos; // Stores the camera's position in the previous frame.

	void Awake () 
	{
		cam = Camera.main.transform;
	}


	// Use this for initialization
	void Start () 
	{
		previousCamPos = cam.position;

		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++) 
		{
			parallaxScales [i] = backgrounds [i].position.z * -1;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < backgrounds.Length; i++) 
		{
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales [i];

			float backgroundTargetPosX = backgrounds [i].position.x + parallax;

			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds [i].position.y, backgrounds [i].position.z);

			backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		previousCamPos = cam.position;

	}
}
