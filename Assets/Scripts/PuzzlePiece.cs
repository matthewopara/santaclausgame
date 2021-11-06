using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private GameObject up;
    [SerializeField] private GameObject down;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;

    public bool IsInPosition()
    {
        return CheckPosition(Vector2.up) && CheckPosition(Vector2.down) && CheckPosition(Vector2.left) && CheckPosition(Vector2.right);
    }

    private bool CheckPosition(Vector2 direction)
    {
        GameObject correctBlock = null;
        if (direction == Vector2.up)
        {
            correctBlock = up;
        }
        else if (direction == Vector2.down)
        {
            correctBlock = down;
        }
        else if (direction == Vector2.left)
        {
            correctBlock = left;
        }
        else if (direction == Vector2.right)
        {
            correctBlock = right;
        }
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 1f);
        if (correctBlock == null)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.CompareTag("Block") && hit.transform.gameObject != gameObject)
                {
                    return false;
                }
            }
            return true;
        }
        
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject == correctBlock)
            {
                return true;
            }
        }

        return false;
    }
}
