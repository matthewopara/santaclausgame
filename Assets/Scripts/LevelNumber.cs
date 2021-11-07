using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelNumber : MonoBehaviour
{
    private Animator animator;
    private readonly int SHOW = Animator.StringToHash("Show");
    private readonly int HIDE = Animator.StringToHash("Hide");
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float displayTime = 2.2f;

    void Start()
    {
        GetComponent<Text>().text = SceneManager.GetActiveScene().name;
        animator = GetComponent<Animator>();
        StartCoroutine(LevelNumberAnimation());
    }

    private IEnumerator LevelNumberAnimation()
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger(SHOW);
        yield return new WaitForSeconds(displayTime);
        animator.SetTrigger(HIDE);
    }
}
