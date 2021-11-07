using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private bool isMoving;
    private bool nextMove = true;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
            nextMove = true;

        if (Input.GetAxisRaw("Vertical") == 1 && !isMoving && nextMove){
            StartCoroutine(MovePlayer(Vector3.up));
            nextMove = false;
        }

        if (Input.GetAxisRaw("Horizontal") == -1 && !isMoving && nextMove){
            StartCoroutine(MovePlayer(Vector3.left));
            nextMove = false;
        }

        if (Input.GetAxisRaw("Vertical") == -1 && !isMoving && nextMove){
            StartCoroutine(MovePlayer(Vector3.down));
            nextMove = false;
        }

        if (Input.GetAxisRaw("Horizontal") == 1 && !isMoving && nextMove){
            StartCoroutine(MovePlayer(Vector3.right));
            nextMove = false;
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

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
