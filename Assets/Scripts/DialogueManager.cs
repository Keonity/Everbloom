using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Animator animator;

    public Text nameText;
    public Text dialogueText;
    public float timeBetweenCharacters;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();    
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        //Debug.Log("Starting conversation with " + dialogue.name);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            // Close dialogue, no sentences left
            EndDialogue();
            return;
        }
        else
        {

            string sentence = sentences.Dequeue();
            StopCoroutine(TypeSentence(sentence));
            StartCoroutine(TypeSentence(sentence));
            //dialogueText.text = sentence;
            //Debug.Log(sentence);
        }

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        animator.SetBool("IsOpen", false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
