using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject sign;

    private void Start()
    {
        this.gameObject.transform.position = Camera.main.WorldToScreenPoint(new Vector3(sign.transform.position.x, sign.transform.position.y + 1f, 0f));

    }

    private void Update()
    {
        this.gameObject.transform.position = Camera.main.WorldToScreenPoint(new Vector3(sign.transform.position.x, sign.transform.position.y + 1f, 0f));
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
