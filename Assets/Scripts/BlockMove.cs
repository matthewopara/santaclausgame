using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public GameObject player;
    private Vector2 targetPosition, mDir;
    const float speed = 2;
    public Rigidbody2D rb;
    public BoxCollider2D box;
    bool mMoving = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (box.IsTouching(player.GetComponent<BoxCollider2D>()))
        { 
            switch (CollisonSide.ColSide(box, (BoxCollider2D)collision.collider))
            {
                case Direction.UP:
                    targetPosition += Vector2.down;
                    break;
                case Direction.LEFT:
                    targetPosition += Vector2.right;
                    break;
                case Direction.RIGHT:
                    targetPosition += Vector2.left;
                    break;
                case Direction.DOWN:
                    targetPosition += Vector2.up;
                    break;
            } 
        }
    }

    private void Start()
    {
        // Set this so we don't wander off at the start
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
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
