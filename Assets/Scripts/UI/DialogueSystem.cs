using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogueBox;

    public string[] dialogueText;
    public float textSpeed;
    public float speedMultiplier;

    public string npcName;
    public Sprite npcSprite;

    public GameObject continueDialogue;
    public GameObject endDialogue;


    private Text dialogue;
    private int characterCount;

    private bool hasComplete;
    private bool hasDialogueCompleted;


    void Awake()
    {
        dialogue = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(3).GetComponent<Text>();
    }

    void Start()
    {
        HideIcons();
        GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(1).GetComponent<Image>().sprite = npcSprite;
        GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(2).GetComponent<Text>().text = npcName;
        textSpeed = 0.5f;
        speedMultiplier = 0.1f;
        characterCount = 0;
        hasComplete = false;
        hasDialogueCompleted = false;
        StartCoroutine(ShowDialogue());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            characterCount = 0;
            dialogueBox.SetActive(true);
            StartCoroutine(ShowDialogue());
        }

        if (Input.GetKeyDown(KeyCode.Return) && hasComplete)
        {
            if (characterCount < dialogueText.Length - 1)
            {
                hasDialogueCompleted = false;
                characterCount++;
                StartCoroutine(ShowDialogue());
                
            }
            else
            {
                hasDialogueCompleted = true;
                characterCount = dialogueText.Length - 1;

                if (Input.GetKeyDown(KeyCode.Return) && hasDialogueCompleted)
                {
                    dialogueBox.SetActive(false);
                    hasDialogueCompleted = false;
                }
            }
        }
    }

    public IEnumerator ShowDialogue()
    {
        for (int index = 0; index < (dialogueText[characterCount].Length + 1); index++)
        {
            HideIcons();
            hasComplete = false;
            dialogue.text = dialogueText[characterCount].Substring(0, index);

            if (characterCount >= dialogueText.Length - 1)
            {
                hasDialogueCompleted = true;
            }

            if (Input.GetKey(KeyCode.Return))
            {
                yield return new WaitForSeconds(textSpeed * speedMultiplier);
            }
            else
            {
                yield return new WaitForSeconds(textSpeed);

            }
        }
        ShowIcon();

        hasComplete = true;
        
        
    }

    private void HideIcons()
    {
        continueDialogue.SetActive(false);
        endDialogue.SetActive(false);
    }

    private void ShowIcon()
    {
        if (hasDialogueCompleted)
        {
            endDialogue.SetActive(true);
            return;
        }

        continueDialogue.SetActive(true);
    }

}
