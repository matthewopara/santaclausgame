using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    private Vector2Int targetPosition;
    const float speed = 1;

    private void Awake()
    {
        // Set this so we don't wander off at the start
        targetPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        transform.position = (Vector2)targetPosition;
    }

    private void Update()
    {
        var moving = (Vector2)transform.position != targetPosition;

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
        if (GetComponent<BoxCollider2D>().IsTouching(player.GetComponent<BoxCollider2D>()))
        {
            targetPosition += Vector2Int.up;
        }
    }
}
