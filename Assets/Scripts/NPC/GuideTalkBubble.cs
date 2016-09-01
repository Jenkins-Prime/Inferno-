using UnityEngine;
using System.Collections;

public class GuideTalkBubble : MonoBehaviour {
    public Animator bblanim;
    BoxCollider2D boxcol;
    public BoxCollider2D playercollider;

	// Use this for initialization
	void Start () {

        boxcol = GetComponent<BoxCollider2D>();

	}
	
	// Update is called once per frame
	void Update () {

        if (boxcol.IsTouching(playercollider ))
             {
            bblanim.SetBool("IsColliding", true);
        }
        else
        { 
            bblanim.SetBool("IsColliding", false);

        }
	}
}
