using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChecker : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(BlockExists(transform.position, Direction.UP));
        }
    }
    public bool BlockExists(Vector2 currentPosition, Direction direction)
    {
        //RaycastHit2D[] hits = Physics2D.RaycastAll(currentPosition, DirectionToVector(direction), 1f);

        /*foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Block") && hit.collider.gameObject != gameObject)
            {
                return true;
            }
        }

        return false;*/

        // replace with above code when blocks are done
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, DirectionToVector(direction), 1f);
        return hit.collider != null;
    }

    // replace with the function in utils
    private Vector2 DirectionToVector(Direction direction)
    {
        switch (direction)
        {
            case Direction.NONE:
                return Vector2.zero;
            case Direction.UP:
                return Vector2.up;
            case Direction.DOWN:
                return Vector2.down;
            case Direction.LEFT:
                return Vector2.left;
            case Direction.RIGHT:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }
}

/*
 * 
Create a script (eventually add it to every block)
When a block wants to move, raycast in the direction of its movement by 1.2 units
If the raycast collides with a block, dont move (return false). If it doesn't, move (return true)

Each block has a circle collider trigger in its literal position
Each block also has the Block tag
 * 
 * 
 * 
 */