using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogueBox;
	public GameObject npcBox;
    public string npcDialogue;
    [HideInInspector]
    public bool canInteract;
   	//public Text npcName;
    public Image npcSprite;

    private Text dialogue;
    private bool isTalking;
    private Player player;

    void Awake() {
        //npcName = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(2).GetComponent<Text>();
        //npcSprite = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(1).GetComponent<Image>();
        //dialogue = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(3).GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start() {
        isTalking = false;
        canInteract = false;
        dialogueBox.SetActive(false);
		npcBox.SetActive (false);
    }

    void Update() { //Move those to player input side
        if (Input.GetKeyDown(KeyCode.Return) && isTalking) {
			StopDialogue();
        } else if(Input.GetKeyDown(KeyCode.Return) && canInteract) { 
			StartDialogue();
        }
    }

    public void StartDialogue() {
        dialogueBox.SetActive(true);
		npcBox.SetActive (true);
        dialogue.text = npcDialogue;
        isTalking = true;
       	Time.timeScale = 0.0f;
		player.canMove = false;
    }

    public void StopDialogue() {
        dialogueBox.SetActive(false);
		npcBox.SetActive (false);
        dialogue.text = "";
        isTalking = false;
        Time.timeScale = 1.0f;
		player.canMove = true;
    }





}
