using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleChecker : MonoBehaviour
{
    private PuzzlePiece[] puzzlePieces;
    [SerializeField] private Animator evidenceAnimator;
    private readonly int SHOW_IMAGE = Animator.StringToHash("ShowImage");
    private LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        puzzlePieces = FindObjectsOfType<PuzzlePiece>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void CheckPuzzle()
    {
        Debug.Log("Checking Puzzle...");
        if (puzzlePieces == null)
        {
            puzzlePieces = FindObjectsOfType<PuzzlePiece>();
        }
        foreach (PuzzlePiece piece in puzzlePieces)
        {
            if (!piece.IsInPosition())
            {
                Debug.Log("Not in position");
                return;
            }
        }

        Debug.Log("Puzzle Finished");
        evidenceAnimator.SetTrigger(SHOW_IMAGE);
        StartCoroutine(StartNextScene());
        Debug.Log("Level Complete");
        
        // freeze player
    }

    private IEnumerator StartNextScene()
    {
        yield return new WaitForSeconds(3f);
        levelLoader.LoadNextLevel();
    }
}
