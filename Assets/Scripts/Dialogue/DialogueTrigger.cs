using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueManager dialogueManager;

    public Dialogue[] dialogueArray;
    private int currentDialogueIdx = 0;
    public bool dialogueComplete { get; private set; }
    public bool allDialoguesComplete { get; private set; }

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public IEnumerator NextDialogue(bool isClaus)
    {
        if (currentDialogueIdx < dialogueArray.Length)
        {
            dialogueManager.StartDialogue(dialogueArray[currentDialogueIdx], isClaus);
            currentDialogueIdx++;

            while (true)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                yield return null;
                bool moreDialogue = dialogueManager.DisplayNextSentence(isClaus);
                if (!moreDialogue)
                {
                    break;
                }
            }

        }
        else
        {
            allDialoguesComplete = true;
        }

        dialogueComplete = true;

    }
}
