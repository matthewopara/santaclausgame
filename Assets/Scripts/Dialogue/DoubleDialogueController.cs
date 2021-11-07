using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDialogueController : MonoBehaviour
{
    [SerializeField] private float delayBeforeDialogue = 1.5f;
    [SerializeField] private DialogueTrigger trigger1;
    [SerializeField] private DialogueTrigger trigger2;

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
            yield return StartCoroutine(trigger1.NextDialogue());
            yield return StartCoroutine(trigger2.NextDialogue());
        }
        FindObjectOfType<DialogueManager>().EndDialogue();
        // call a grid movement function to reenable inputs
    }
}
