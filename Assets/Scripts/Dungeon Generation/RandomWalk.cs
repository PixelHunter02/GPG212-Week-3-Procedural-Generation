using System.Collections.Generic;
using UnityEngine;


public class RandomWalk
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int randomWalkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for(int i = 0; i < randomWalkLength; i++)
        {
            var newPosition = previousPosition + Direction.randomDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction.randomDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);
        for(int i = 0; i<corridorLength; i++)
        { 
            // Debug.Log(currentPosition);
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
}

public static class Direction
{
    public static List<Vector2Int> directions = new List<Vector2Int>
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public static Vector2Int randomDirection()
    {
        return directions[Random.Range(0,directions.Count)];
    }
}