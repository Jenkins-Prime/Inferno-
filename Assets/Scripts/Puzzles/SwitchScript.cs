using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {

	public TargetScript target;
	private Animator anim;
	public bool sticks;



	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D()
	{
		anim.SetBool ("active", true);
		target.SwitchActive();

	}

	void OnTriggerExit2D()
	{
		if (sticks)
			return;
		anim.SetBool ("active", false);
		target.SwitchInactive();
	}

}
