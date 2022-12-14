using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using MoreMountains.TopDownEngine;
using System.Xml.Serialization;

public class GenerateCorridor : DungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;
    public static int roomsCreated = 0;

    public TMP_Text enemiesText;
    public static int enemiesLeft;
    public static int enemiesThisGame;
    public GameObject gameOver;
    [SerializeField]
    GameObject playerWins;
    [SerializeField]
    GameObject mainGameUI;

    private void OnEnable()
    {
        RemoveEnemiesAtSpawn.textSetup += SetupText;
        RemoveEnemiesAtSpawn.textSetup += UpdateText;
        Health.enemyDead += UpdateText;
        Health.enemyDead += CheckUpdate;
    }

    private void OnDisable()
    {
        RemoveEnemiesAtSpawn.textSetup -= SetupText;
        RemoveEnemiesAtSpawn.textSetup -= UpdateText;
        Health.enemyDead -= UpdateText;
        Health.enemyDead -= CheckUpdate;
    }

    private void Start() {
        roomsCreated = 0;
        tilePlacement.Clear();
        CorridorFirstGeneration();
    }

    protected override void GenerateFloor()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        floorPositions.UnionWith(roomPositions);

        tilePlacement.PlaceFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilePlacement);

    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = StartRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
            roomsCreated ++;
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = RandomWalk.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }

    private void UpdateText()
    {
        enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Count();
        enemiesText.text = "Enemies Left: " + enemiesLeft + " / " + enemiesThisGame;
    }

    private void SetupText()
    {
        enemiesThisGame = GameObject.FindGameObjectsWithTag("Enemy").Count();
    }
    private void CheckUpdate()
    {
        if(enemiesLeft <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            mainGameUI.SetActive(false);
            playerWins.SetActive(true);
        }
    }
}