using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[RequireComponent(typeof(Text))]
public class NPCScript : MonoBehaviour
{
    public Animator bblanim;
    public Sprite npcImage;
    public string npcName;

    private Text dialogue;

    private DialogueSystem dialogueSystem;

	void Awake ()
    {
        dialogueSystem = GameObject.FindGameObjectWithTag("Dialogue Holder").GetComponent<DialogueSystem>();
        dialogue = GetComponent<Text>();
	}




    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            dialogueSystem.npcSprite.sprite = npcImage;
            //dialogueSystem.npcName.text = npcName;
            dialogueSystem.npcDialogue = dialogue.text;
            bblanim.SetBool("IsColliding", true);
            dialogueSystem.canInteract = true;

        }
    }


    void OnTriggerExit2D()
    {
       bblanim.SetBool("IsColliding", false);
        dialogueSystem.canInteract = false;
    }

}

