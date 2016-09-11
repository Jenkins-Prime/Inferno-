using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public float textSpeed;
    private Text dialogueText;

    public string[] testData;

    private int characterCount;

    void Awake()
    {
        dialogueText = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(3).GetComponent<Text>();

    }

    void Update()
    {
       
    }

    void Start()
    {
        textSpeed = 0.3f;
        characterCount = 0;
        StartCoroutine(Text());

    }

    private IEnumerator Text()
    {
        for (int index = 0; index < (testData[characterCount].Length + 1); index++)
        {
            dialogueText.text = testData[characterCount].Substring(0, index);

            yield return new WaitForSeconds(textSpeed);
        }
    }
    
	
}
