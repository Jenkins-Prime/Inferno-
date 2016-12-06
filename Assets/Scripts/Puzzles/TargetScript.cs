using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SwitchActive() 
	{
		anim.SetBool ("Active", true);

	}

	public void SwitchInactive()
	{
		anim.SetBool ("Active", false);
	}

}
