using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DialogueSystem : MonoBehaviour
{
    public string[] dialogueText;
    public float textSpeed;
    public float speedMultiplier;

    private Text dialogue;
    private int characterCount;

    private bool hasComplete;

    void Awake()
    {
        dialogue = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(3).GetComponent<Text>();
    }

    void Start()
    {
        textSpeed = 0.5f;
        speedMultiplier = 0.1f;
        characterCount = 0;
        hasComplete = false;
        StartCoroutine(ShowDialogue());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && hasComplete)
        {
            if (characterCount >= dialogueText.Length)
            {
                characterCount = dialogueText.Length;
            }
            else
            {
                dialogue.text = dialogueText[characterCount++];
                StartCoroutine(ShowDialogue());
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
