using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
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

    public bool DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            return false;
        }

        string sentence = sentences.Dequeue();
        
        if (currentTypeSentence != null)
        {
            StopCoroutine(currentTypeSentence);
        }
        currentTypeSentence = TypeSentence(sentence);
        StartCoroutine(currentTypeSentence);
        return true;
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

    public void EndDialogue()
    {
        animator.SetBool(IS_OPEN, false);
    }
}
