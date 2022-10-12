using System;
using System.Runtime.Serialization;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    protected RandomWalkData randomWalkParameters;
    [SerializeField]
    private GameObject enemyPrefab;
    
    protected override void GenerateFloor()
    {
        HashSet<Vector2Int> floorPositions = StartRandomWalk(randomWalkParameters, startPosition);
        tilePlacement.Clear();
        tilePlacement.PlaceFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions,tilePlacement);
    }

    protected HashSet<Vector2Int> StartRandomWalk(RandomWalkData randomWalkParameters, Vector2Int position)
    {
        Debug.Log("Running");
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for(int i = 0; i< randomWalkParameters.steps; i++)
        {
            var path = RandomWalk.SimpleRandomWalk(currentPosition, randomWalkParameters.length);
            floorPositions.UnionWith(path);
            foreach(var floorPosition in floorPositions)
            {
                int random = UnityEngine.Random.Range(0,2000);
                if(random == 0 && floorPosition != new Vector2Int(0,0))
                {
                    Instantiate(enemyPrefab, new Vector3(floorPosition.x, floorPosition.y, 0f),Quaternion.identity);
                }
            }
            if(randomWalkParameters.startRandomlyEachIteration)
            {
                floorPositions.ElementAt(UnityEngine.Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }

}
