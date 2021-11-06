using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueManager dialogueManager;
    public Dialogue dialogue;

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogueManager.StartDialogue(dialogue);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            dialogueManager.DisplayNextSentence();
        }
    }
}
