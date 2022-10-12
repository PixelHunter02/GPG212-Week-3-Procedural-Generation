using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacement : MonoBehaviour
{
    [SerializeField]
    Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    TileBase[] floorTiles, wallTile;

    public void PlaceFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PlaceTiles(floorPositions, floorTilemap, floorTiles);
    }

    private void PlaceTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase[] tile)
    {
        foreach(var position in positions)
        {
            PlaceSingleTile(tileMap, tile[Random.Range(0,floorTiles.Length)], position);
        }
    }

    private void PlaceSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    internal void PlaceSingleBasicWall(Vector2Int wallPositions)
    {
        PlaceSingleTile(wallTilemap, wallTile[Random.Range(0,wallTile.Length)], wallPositions);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
    }
}
