using System;
using UnityEngine;

public class FruitGenerator : MonoBehaviour
{

    public GeneratorData data;

    private const int Z_POSITION = 0;
    private int generatedFruitCount;
    private int startTime;
    private int fruitVarietyCount;
    private Vector3 spawnPos;
    private Vector3 stageDimensions;
    private Vector3 zeroVector;
    private float yPositionOfBasketTop;
    private int fruitCount;

    public static event Action<int> FruitsCollected;

    void Start()
    {
        fruitCount = 0;
        startTime = 0;
        InvokeSpawning();

        fruitVarietyCount = data.fruits.Length;
        zeroVector = new Vector3(0, 0, 0);
        spawnPos = zeroVector;
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Z_POSITION));

        yPositionOfBasketTop = data.basket.transform.position.y + data.basket.GetComponent<BoxCollider2D>().size.y;

        Fruit.OnFruitCollectedEvent += CountDecrease;
        GameManager.OnWinEvent += CancelInvoke;
        GameManager.OnRestartEvent += InvokeSpawning;
    }

    private void OnDestroy()
    {
        Fruit.OnFruitCollectedEvent -= CountDecrease;
        GameManager.OnWinEvent -= CancelInvoke;
        GameManager.OnRestartEvent -= InvokeSpawning;
    }

    void SelectAndSpawnFruit()
    {
        int fruitNumber = UnityEngine.Random.Range(0, fruitVarietyCount);
        spawnPos = FindSpawnPosition();
        Spawn(fruitNumber,spawnPos);
    }

    private void Spawn(int fruitNumber,Vector3 spawnPos)
    {
        if (generatedFruitCount < data.maxFruitsToSpawn)
        {
            Instantiate(data.fruits[fruitNumber], spawnPos, Quaternion.identity);
            CountIncrease();
        }
    }

    public Vector3 FindSpawnPosition()
    {
        float xPosition = UnityEngine.Random.Range(-Mathf.Abs(stageDimensions.x) + data.minDistanceFromBorders, Mathf.Abs(stageDimensions.x) - data.minDistanceFromBorders);
        float yPosition = UnityEngine.Random.Range(-Mathf.Abs(stageDimensions.y) + yPositionOfBasketTop + data.distanceFromBasketTop, Mathf.Abs(stageDimensions.y) - data.minDistanceFromBorders);
        spawnPos = new Vector3(xPosition, yPosition, Z_POSITION);
        return spawnPos;
    }

    private void CountDecrease()
    {
        fruitCount++;
        FruitsCollected?.Invoke(fruitCount);
        generatedFruitCount--;
    }

    private void CountIncrease()
    {
        generatedFruitCount++;
    }

    private void InvokeSpawning()
    {
        InvokeRepeating(nameof(SelectAndSpawnFruit), startTime, data.spawnRate);
    }

}
