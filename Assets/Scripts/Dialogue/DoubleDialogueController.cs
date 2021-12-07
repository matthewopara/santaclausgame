using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDialogueController : MonoBehaviour
{
    [SerializeField] private float delayBeforeDialogue = 1.5f;
    [SerializeField] private DialogueTrigger trigger1;
    [SerializeField] private DialogueTrigger trigger2;
    [SerializeField] private bool endingChoice = false;

    [SerializeField] private GameObject joinText;
    [SerializeField] private GameObject arrestText;
    [SerializeField] private GameObject nameText;
    [SerializeField] private GameObject continueText;
    [SerializeField] private GameObject dialogueText;
    private bool showingChoices = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartConversation());
    }

    private IEnumerator StartConversation()
    {
        yield return new WaitForSeconds(delayBeforeDialogue);

        // call a grid movement function to ignore inputs
        while (!trigger1.allDialoguesComplete && !trigger2.allDialoguesComplete)
        {
            yield return StartCoroutine(trigger1.NextDialogue(true));
            yield return StartCoroutine(trigger2.NextDialogue(false));
        }

        if (endingChoice)
        {
            // show choices
            joinText.SetActive(true);
            arrestText.SetActive(true);
            nameText.SetActive(false);
            continueText.SetActive(false);
            dialogueText.SetActive(false);
            showingChoices = true;
        }
        else
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
        // call a grid movement function to reenable inputs
    }

    private void Update()
    {
        if (showingChoices && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Join Her");
            joinText.SetActive(false);
            arrestText.SetActive(false);
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
        else if (showingChoices && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Arrest Her");
            joinText.SetActive(false);
            arrestText.SetActive(false);
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }
}
