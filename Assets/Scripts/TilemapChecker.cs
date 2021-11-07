using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapChecker : MonoBehaviour
{
    // remove gridLayout & tilemap
    public GridLayout gridLayout;
    public Tilemap tilemap;
    public bool CanMove(Vector2 currentPosition, Direction direction)
    {
        Vector2 newPosition = currentPosition + Utils.DirectionToVector(direction);
        Vector3Int cellPositon = gridLayout.WorldToCell(newPosition);
        TileBase tile = tilemap.GetTile(cellPositon);
        return tile == null;
    }

    // remove start and cellposition
    /*private void Start()
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
        
    }*/
}
