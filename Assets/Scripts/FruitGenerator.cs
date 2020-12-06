using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FruitGenerator : MonoBehaviour
{
    public GameObject[] fruits;
    public GameObject basket;

    public float minDistanceFromBorders;
    public float spawnRate;
    public int maxFruitsToSpawn;
    public float distanceFromBasketTop;

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
        InvokeRepeating(nameof(Spawn), startTime, spawnRate);

        fruitVarietyCount = fruits.Length;
        zeroVector = new Vector3(0, 0, 0);
        spawnPos = zeroVector;
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Z_POSITION));

        yPositionOfBasketTop = basket.transform.position.y + basket.GetComponent<BoxCollider2D>().size.y;
    }

    void Spawn()
    {
        int fruitNumber = Random.Range(0, fruitVarietyCount);
        spawnPos = FindSpawnPosition();
        SelectFruitToSpawn(fruitNumber,spawnPos);
    }

    private void SelectFruitToSpawn(int fruitNumber,Vector3 spawnPos)
    {
        if (GameManager.instance.generatedFruitCount < maxFruitsToSpawn)
        {
            var _fruit=Instantiate(fruits[fruitNumber], spawnPos, Quaternion.identity);
            _fruit.GetComponent<FruitDragAndDrop>().OnFruitCollectedEvent += OnFruitCollected;
            GameManager.instance.CountIncrease();
        }
    }

    private void OnFruitCollected()
    {
        GameManager.instance.AddFruit();
    }

    public Vector3 FindSpawnPosition()
    {
        float xPosition = Random.Range(-Mathf.Abs(stageDimensions.x) + minDistanceFromBorders, Mathf.Abs(stageDimensions.x) - minDistanceFromBorders);
        float yPosition = Random.Range(-Mathf.Abs(stageDimensions.y) + yPositionOfBasketTop + distanceFromBasketTop, Mathf.Abs(stageDimensions.y) - minDistanceFromBorders);
        spawnPos = new Vector3(xPosition, yPosition, Z_POSITION);
        return spawnPos;
    }

}
