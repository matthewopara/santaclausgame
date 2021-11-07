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

    public bool MoveInDirection(Direction direction)
    {
        var vecDir = Utils.DirectionToVector(direction);
        if(!checker.CanMove(rb.position, direction) || blockChecker.BlockExists(rb.position, direction) != null)
        {
            return false;
        }
        targetPosition += vecDir;
        return true;
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //    if (box.IsTouching(player.GetComponent<BoxCollider2D>()))
    //    { 
    //        switch (CollisonSide.ColSide(box, (BoxCollider2D)collision.collider))
    //        {
    //            case Direction.UP:
    //                targetPosition += Vector2.down;
    //                break;
    //            case Direction.LEFT:
    //                targetPosition += Vector2.right;
    //                break;
    //            case Direction.RIGHT:
    //                targetPosition += Vector2.left;
    //                break;
    //            case Direction.DOWN:
    //                targetPosition += Vector2.up;
    //                break;
    //        } 
    //    }
    //}

    private void Start()
    {
        // Set this so we don't wander off at the start
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
}
