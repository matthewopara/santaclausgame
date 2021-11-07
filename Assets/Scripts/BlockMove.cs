using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    private Vector2 targetPosition, mDir;
    [SerializeField] private float speed = 2;
    private Rigidbody2D rb;
    [SerializeField] private TilemapChecker checker;
    [SerializeField] private BlockChecker blockChecker;
    private PuzzleChecker puzzleChecker;

    public bool MoveInDirection(Direction direction)
    {
        var vecDir = Utils.DirectionToVector(direction);
        if(!checker.CanMove(rb.position, direction) || blockChecker.BlockExists(rb.position, direction) != null)
        {
            return false;
        }
        targetPosition += vecDir;
        StartCoroutine(CheckPuzzle());
        return true;
    }

    private void Start()
    {
        // Set this so we don't wander off at the start
        puzzleChecker = FindObjectOfType<PuzzleChecker>();
        rb = GetComponent<Rigidbody2D>();
        targetPosition = new Vector2(rb.position.x, rb.position.y);
        rb.MovePosition(targetPosition);
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MoveTowardsTargetPosition();
    }

    private void MoveTowardsTargetPosition()
    {
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime));
    }

    private IEnumerator CheckPuzzle()
    {
        yield return new WaitForSeconds(.2f);
        Debug.Log("Check Puzzle");
        puzzleChecker.CheckPuzzle();
    }
}
