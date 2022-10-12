using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SimpleRandomWalkParameters" ,menuName = "PCG/SimpleRandomWalkData")]
public class RandomWalkData : ScriptableObject
{
    public int steps = 10;
    public int length = 10;
    public bool startRandomlyEachIteration = true;
}
