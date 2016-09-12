using UnityEngine;
using System.Collections;

public class GuideTalkBubble : MonoBehaviour
{
    public GameObject dialogue;
    public Animator bblanim;
    public DialogueSystem dialogueSystem;

	void Awake ()
    {
        dialogueSystem = GameObject.FindGameObjectWithTag("NPC").GetComponent<DialogueSystem>();
	}




    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            bblanim.SetBool("IsColliding", true);
            StartCoroutine(dialogueSystem.StartDialogue());

        }
    }

    void OnTriggerExit2D()
    {
       bblanim.SetBool("IsColliding", false);
       
    }

}

