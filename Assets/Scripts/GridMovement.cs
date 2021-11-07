using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private Vector3 origPos, targetPos;
    [SerializeField] public float timeToMove = 0.2f;

    [SerializeField] private TilemapChecker tilemapChecker;
    [SerializeField] private BlockChecker blockChecker;
    public Animator animator;
    public GameObject GFX;

    private bool nextMoveVertical = true;
    private bool nextMoveHorizontal = true;

    [SerializeField] const KeyCode pullButton = KeyCode.LeftShift;

    [SerializeField] public bool mPulling = false;
    [SerializeField] public bool mPushing = false;
    [SerializeField] public bool mActive = true;
    [SerializeField] public bool isMoving;
    [SerializeField] Direction mDir = Direction.NONE;
    private void Awake()
    {
        tilemapChecker = GetComponent<TilemapChecker>();
        blockChecker = GetComponent<BlockChecker>();
        GFX = transform.GetChild(0).gameObject;
        animator = GFX.GetComponent<Animator>();
    }

    public void DisapleMovement()
    {
        mActive = false;
    }

    public void EnableMovement()
    {
        mActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mActive)
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
                TryToMove(Direction.UP);
                nextMoveVertical = false;
            }

            if (Input.GetAxisRaw("Horizontal") == -1 && !isMoving && nextMoveHorizontal)
            {
               TryToMove(Direction.LEFT);
                nextMoveHorizontal = false;
            }

            if (Input.GetAxisRaw("Vertical") == -1 && !isMoving && nextMoveVertical)
            {
                TryToMove(Direction.DOWN);
                nextMoveVertical = false;
            }

            if (Input.GetAxisRaw("Horizontal") == 1 && !isMoving && nextMoveHorizontal)
            {
                TryToMove(Direction.RIGHT);
                nextMoveHorizontal = false;
            }
        }
        animator.SetBool("Walking", isMoving && !mPulling && !mPushing);
        animator.SetBool("Pushing", mPushing);
        animator.SetBool("Pulling", mPulling);
    }

    private void TryToMove(Direction direction)
    {
        if (tilemapChecker.CanMove(transform.position, direction))
        {
            GameObject block = blockChecker.BlockExists(transform.position, direction);
            if ((block != null && (mPushing = block.GetComponent<BlockMove>().MoveInDirection(direction))) || block == null)
            {
                if (Input.GetKey(pullButton) && !mPushing)
                {
                    block = blockChecker.BlockExists(transform.position, Utils.GetOppisiteDir(direction));
                    if (block != null)
                    {
                        block.GetComponent<BlockMove>().MoveInDirection(direction);
                        mPulling = true;
                    }
                }
                mDir = direction;
                var dirVec = Utils.DirectionToVector(mDir);
                isMoving = true;
                animator.SetFloat("x", dirVec.x);
                animator.SetFloat("y", dirVec.y);
                StartCoroutine(MovePlayer(direction));
            }
        }
    }

    private IEnumerator MovePlayer(Direction direction)
    {
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
        if (mPulling)
        {
            mPulling = false;
            var dirVec = Utils.DirectionToVector(Utils.GetOppisiteDir(mDir));
            animator.SetFloat("x", dirVec.x);
            animator.SetFloat("y", dirVec.y);
        }
        if (mPushing)
        {
            mPushing = false;
        }
    }
}
