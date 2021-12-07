using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Image faceImage;
    private Queue<string> sentences;
    private Queue<Sprite> faces;

    [SerializeField] private Animator animator;
    private int IS_OPEN = Animator.StringToHash("IsOpen");

    private IEnumerator currentTypeSentence = null;

    private void Awake()
    {
        sentences = new Queue<string>();
        faces = new Queue<Sprite>();
    }
    
    public void StartDialogue(Dialogue dialogue, bool isClaus)
    {
        animator.SetBool(IS_OPEN, true);
        nameText.text = dialogue.name;
        sentences.Clear();
        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            sentences.Enqueue(dialogue.sentences[i]);
            if (dialogue.faces.Length > i)
            {
                faces.Enqueue(dialogue.faces[i]);
            }
        }
        /*foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }*/

        DisplayNextSentence(isClaus);
    }

    public bool DisplayNextSentence(bool isClaus)
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
        if (faces.Count > 0)
        {
            faceImage.sprite = faces.Dequeue();
        }
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
