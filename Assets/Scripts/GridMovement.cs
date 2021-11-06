using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") == 1 && !isMoving)
            StartCoroutine(MovePlayer(Vector3.up));

        if (Input.GetAxisRaw("Horizontal") == -1 && !isMoving)
            StartCoroutine(MovePlayer(Vector3.left));

        if (Input.GetAxisRaw("Vertical") == -1 && !isMoving)
            StartCoroutine(MovePlayer(Vector3.down));

        if (Input.GetAxisRaw("Horizontal") == 1 && !isMoving)
            StartCoroutine(MovePlayer(Vector3.right));
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
