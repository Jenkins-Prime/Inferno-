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

    private Text dialogue;
    private int characterCount;

    private bool hasComplete;

    void Awake()
    {
        dialogue = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(3).GetComponent<Text>();
    }

    void Start()
    {
        GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(1).GetComponent<Image>().sprite = npcSprite;
        GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(2).GetComponent<Text>().text = npcName;
        textSpeed = 0.5f;
        speedMultiplier = 0.1f;
        characterCount = 0;
        hasComplete = false;
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
                characterCount++;
                StartCoroutine(ShowDialogue());
                
            }
            else
            {
                characterCount = dialogueText.Length - 1;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    dialogueBox.SetActive(false);
                }
            }
        }
    }

    public IEnumerator ShowDialogue()
    {
        for (int index = 0; index < (dialogueText[characterCount].Length + 1); index++)
        {
            hasComplete = false;
            dialogue.text = dialogueText[characterCount].Substring(0, index);

            if (Input.GetKey(KeyCode.Return))
            {
                yield return new WaitForSeconds(textSpeed * speedMultiplier);
            }
            else
            {
                yield return new WaitForSeconds(textSpeed);

            }
        }

        hasComplete = true;

    }

}
