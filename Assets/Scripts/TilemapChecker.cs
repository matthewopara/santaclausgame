using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapChecker : MonoBehaviour
{
    // remove gridLayout & tilemap
    [HideInInspector] public GridLayout gridLayout;
    [HideInInspector] public Tilemap wallTilemap;
    [HideInInspector] public Tilemap obstacleTilemap;

    private void Awake()
    {
        gridLayout = FindObjectOfType<Grid>();
        wallTilemap = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
        obstacleTilemap = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<Tilemap>();
    }
    public bool CanMove(Vector2 currentPosition, Direction direction)
    {
        Vector2 newPosition = currentPosition + Utils.DirectionToVector(direction);
        Vector3Int cellPositon = gridLayout.WorldToCell(newPosition);
        TileBase wallTile = wallTilemap.GetTile(cellPositon);
        TileBase obstacleTile = obstacleTilemap.GetTile(cellPositon);
        Debug.Log(wallTile + " : " + obstacleTile);
        return wallTile == null && obstacleTile == null;
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
