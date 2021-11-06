using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapChecker : MonoBehaviour
{
    // remove gridLayout & tilemap
    public GridLayout gridLayout;
    public Tilemap tilemap;
    public bool CanMove(Vector2 currentPosition, Direction moveDirection, Tilemap tilemap)
    {
        Vector2 newPosition = currentPosition + DirectionToVector(moveDirection);
        Vector3Int cellPositon = gridLayout.WorldToCell(newPosition);
        TileBase tile = tilemap.GetTile(cellPositon);
        return tile == null;
    }

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

    // remove start and cellposition
    private void Start()
    {
        StartCoroutine(CellPosition());
    }

    IEnumerator CellPosition()
    {
        while (true)
        {
            bool canMove = CanMove(transform.position, Direction.LEFT, tilemap);
            Debug.Log(canMove);
            yield return new WaitForSeconds(1f);
        }
        
    }
}
