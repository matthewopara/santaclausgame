using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementOld : MonoBehaviour
{
    private bool isMoving;
    private bool nextMove = true;
    private Direction mDir;
    public TilemapChecker checker;
    float elapsedTime = 0;
    public Rigidbody2D rb;
    Vector2 origPos;

    bool mW = false, mA = false, mD = false, mS = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            var blockMove = collision.gameObject.GetComponent<BlockMove>();
            if(!blockMove.MoveInDirection(mDir))
            {
                targetPosition = origPos;
            }
        }
    }

    private Vector2 targetPosition;
    public float speed = 4;

    private void Start()
    {
        // Set this so we don't wander off at the start
        targetPosition = rb.position;
    }

    private void Update()
    {
        var moving = rb.position != targetPosition;

        if (moving)
        {
            MoveTowardsTargetPosition();
        }
        else
        {
            SetNewTargetPositionFromInput();
        }
    }

    private void MoveTowardsTargetPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void SetNewTargetPositionFromInput()
    {
        if (!mW && Input.GetKey(KeyCode.W) && checker.CanMove(transform.position, Direction.UP))
        {
            mDir = Direction.UP;
            origPos = rb.position;
            targetPosition += Vector2Int.up;
        }
        else if (!mA && Input.GetKey(KeyCode.A) && checker.CanMove(transform.position, Direction.LEFT))
        {
            mDir = Direction.LEFT;
            origPos = rb.position;
            targetPosition += Vector2Int.left;
        }
        else if (!mD && Input.GetKey(KeyCode.D) && checker.CanMove(transform.position, Direction.RIGHT))
        {
            mDir = Direction.RIGHT;
            origPos = rb.position;
            targetPosition += Vector2Int.right;
        }
        else if (!mS && Input.GetKey(KeyCode.S) && checker.CanMove(transform.position, Direction.DOWN))
        {
            mDir = Direction.DOWN;
            origPos = rb.position;
            targetPosition += Vector2Int.down;
        }
        mS = Input.GetKey(KeyCode.S);
        mD = Input.GetKey(KeyCode.D);
        mW = Input.GetKey(KeyCode.W);
        mA = Input.GetKey(KeyCode.A);
    }
}
