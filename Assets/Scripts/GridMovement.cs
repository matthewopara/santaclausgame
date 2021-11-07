using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;

    [SerializeField] private TilemapChecker tilemapChecker;
    [SerializeField] private BlockChecker blockChecker;

    private bool nextMoveVertical = true;
    private bool nextMoveHorizontal = true;

    private void Awake()
    {
        tilemapChecker = GetComponent<TilemapChecker>();
        blockChecker = GetComponent<BlockChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            nextMoveVertical = true;
        }

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            nextMoveHorizontal = true;
        }

        if (Input.GetAxisRaw("Vertical") == 1 && !isMoving && nextMoveVertical)
        {
            StartCoroutine(TryToMove(Direction.UP));
            nextMoveVertical = false;
        }

        if (Input.GetAxisRaw("Horizontal") == -1 && !isMoving && nextMoveHorizontal)
        {
            StartCoroutine(TryToMove(Direction.LEFT));
            nextMoveHorizontal = false;
        }

        if (Input.GetAxisRaw("Vertical") == -1 && !isMoving && nextMoveVertical)
        {
            StartCoroutine(TryToMove(Direction.DOWN));
            nextMoveVertical = false;
        }

        if (Input.GetAxisRaw("Horizontal") == 1 && !isMoving && nextMoveHorizontal)
        {
            StartCoroutine(TryToMove(Direction.RIGHT));
            nextMoveHorizontal = false;
        }
    }

    private IEnumerator TryToMove(Direction direction)
    {
        if (tilemapChecker.CanMove(transform.position, direction))
        {
            GameObject block = blockChecker.BlockExists(transform.position, direction);
            if ((block != null && block.GetComponent<BlockMove>().MoveInDirection(direction)) || block == null)
            {
                yield return StartCoroutine(MovePlayer(direction));
            }
        }
    }

    private IEnumerator MovePlayer(Direction direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + (Vector3)Utils.DirectionToVector(direction);

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}
