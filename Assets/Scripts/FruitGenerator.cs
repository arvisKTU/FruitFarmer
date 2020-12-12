using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FruitGenerator : MonoBehaviour
{

    public GeneratorData data;

    private const int Z_POSITION = 0;
    private int startTime;
    private int fruitVarietyCount;
    private Vector3 spawnPos;
    private Vector3 stageDimensions;
    private Vector3 zeroVector;
    private float yPositionOfBasketTop;

    void Start()
    {
        startTime = 0;
        InvokeRepeating(nameof(SelectFruitToSpawn), startTime, data.spawnRate);

        fruitVarietyCount = data.fruits.Length;
        zeroVector = new Vector3(0, 0, 0);
        spawnPos = zeroVector;
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Z_POSITION));

        yPositionOfBasketTop = data.basket.transform.position.y + data.basket.GetComponent<BoxCollider2D>().size.y;
    }

    void SelectFruitToSpawn()
    {
        int fruitNumber = Random.Range(0, fruitVarietyCount);
        spawnPos = FindSpawnPosition();
        Spawn(fruitNumber,spawnPos);
    }

    private void Spawn(int fruitNumber,Vector3 spawnPos)
    {
        if (GameManager.instance.generatedFruitCount < data.maxFruitsToSpawn)
        {
            var _fruit=Instantiate(data.fruits[fruitNumber], spawnPos, Quaternion.identity);
            _fruit.GetComponent<Fruit>().OnFruitCollectedEvent += OnFruitCollected;
            GameManager.instance.CountIncrease();
        }
    }

    private void OnFruitCollected()
    {
        GameManager.instance.AddFruit();
    }

    public Vector3 FindSpawnPosition()
    {
        float xPosition = Random.Range(-Mathf.Abs(stageDimensions.x) + data.minDistanceFromBorders, Mathf.Abs(stageDimensions.x) - data.minDistanceFromBorders);
        float yPosition = Random.Range(-Mathf.Abs(stageDimensions.y) + yPositionOfBasketTop + data.distanceFromBasketTop, Mathf.Abs(stageDimensions.y) - data.minDistanceFromBorders);
        spawnPos = new Vector3(xPosition, yPosition, Z_POSITION);
        return spawnPos;
    }

}
