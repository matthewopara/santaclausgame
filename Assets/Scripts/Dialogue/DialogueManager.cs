using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogueText;
    private Queue<string> sentences;

    [SerializeField] private Animator animator;
    private int IS_OPEN = Animator.StringToHash("IsOpen");

    private IEnumerator currentTypeSentence = null;

    private void Awake()
    {
        sentences = new Queue<string>();
    }
    
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool(IS_OPEN, true);
        nameText.text = dialogue.name;
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
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        
        if (currentTypeSentence != null)
        {
            StopCoroutine(currentTypeSentence);
        }
        currentTypeSentence = TypeSentence(sentence);
        StartCoroutine(currentTypeSentence);
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        animator.SetBool(IS_OPEN, false);
    }
}
