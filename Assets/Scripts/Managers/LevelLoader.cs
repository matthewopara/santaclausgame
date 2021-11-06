using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator transition;
    private readonly int START = Animator.StringToHash("Start");
    private float transitionTime = 1f;
    private void Awake()
    {
        transition = GetComponentInChildren<Animator>();
    }

    public void LoadNextLevel()
    {
        int sceneIdx = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(sceneIdx + 1);
    }

    public void LoadLevel(int sceneIdx)
    {
        StartCoroutine(LoadLevelCoroutine(sceneIdx));
    }

    private IEnumerator LoadLevelCoroutine(int sceneIdx)
    {
        // play animation
        transition.SetTrigger(START);
        // wait for animation to end
        yield return new WaitForSeconds(transitionTime);
        // load new scene
        SceneManager.LoadScene(sceneIdx);
    }

    // For Testing Purposes
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            LoadNextLevel();
        }
    }
}
