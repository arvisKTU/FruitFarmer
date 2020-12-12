using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Generator Data", menuName = "Generator Data")]
public class GeneratorData : ScriptableObject
{
    public GameObject[] fruits;
    public GameObject basket;

    public float minDistanceFromBorders;
    public float spawnRate;
    public int maxFruitsToSpawn;
    public float distanceFromBasketTop;
}
