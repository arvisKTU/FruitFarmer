using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FruitGenerator : MonoBehaviour
{
    private const int Z_POSITION = 0;
    private const int DIRECTIONS_COUNT = 2;
    public float spawnRate;
    private int count = 0;
    private int startTime;
    private int fruitVarietyCount;

    public GameObject blueberryBasket;
    public GameObject peachBasket;
    public GameObject strawberryBasket;
    public float distanceFromBasket;
    public float distanceWithinBasket;
    public float distanceFromBorders;

    private Vector3 spawnPos;
    Vector3 stageDimensions;

    private Vector3 zeroVector;

    public FruitPooler[] fruitPools;

    void Start()
    {
        startTime = 0;
        InvokeRepeating(nameof(Spawn), startTime, spawnRate);

        fruitVarietyCount = 3;
        zeroVector = new Vector3(0, 0, 0);
        spawnPos = zeroVector;
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Z_POSITION));
    }

    void Spawn()
    {
        int fruitNumber = Random.Range(0, fruitVarietyCount);
        SelectFruitToSpawn(fruitNumber);
    }
    private void SelectFruitToSpawn(int fruitNumber)
    {
            GameObject newFruit = fruitPools[fruitNumber].GetPooledFruit();
            newFruit.transform.position = FindSpawnPosition();
            newFruit.SetActive(true);
            count++;
    }
    public Vector3 FindSpawnPosition()
    {
        float xPosition = Random.Range(-Mathf.Abs(stageDimensions.x) + distanceFromBorders, Mathf.Abs(stageDimensions.x) - distanceFromBorders);
        float yPosition = Random.Range(-Mathf.Abs(stageDimensions.y) + distanceFromBorders, Mathf.Abs(stageDimensions.y) - distanceFromBorders);
        spawnPos = new Vector3(xPosition, yPosition, Z_POSITION);
        spawnPos = FindOtherSpawnPosition(xPosition, yPosition, spawnPos);
        return spawnPos;
    }
    private Vector3 FindOtherSpawnPosition(float xPosition, float yPosition, Vector3 spawnPosPrevious)
    {
        Vector3 spawnPos;
        if (Mathf.Abs(spawnPosPrevious.x - blueberryBasket.transform.position.x) <= distanceWithinBasket &&
          (Mathf.Abs(spawnPosPrevious.y - blueberryBasket.transform.position.y) <= distanceWithinBasket))
        {
            xPosition = blueberryBasket.transform.position.x + distanceFromBasket;
            spawnPos = new Vector3(xPosition, yPosition, Z_POSITION);
        }
        else if (Mathf.Abs(spawnPosPrevious.x - peachBasket.transform.position.x) <= distanceWithinBasket &&
           (Mathf.Abs(spawnPosPrevious.y - peachBasket.transform.position.y) <= distanceWithinBasket))
        {
            xPosition = Random.Range(0, DIRECTIONS_COUNT) == 0 ? peachBasket.transform.position.x - distanceFromBasket : peachBasket.transform.position.x + distanceFromBasket;
            spawnPos = new Vector3(xPosition, yPosition, Z_POSITION);
        }
        else if (Mathf.Abs(spawnPosPrevious.x - strawberryBasket.transform.position.x) <= distanceWithinBasket &&
           (Mathf.Abs(spawnPosPrevious.y - strawberryBasket.transform.position.y) <= distanceWithinBasket))
        {
            xPosition = strawberryBasket.transform.position.x - distanceFromBasket;
            spawnPos = new Vector3(xPosition, yPosition, Z_POSITION);
        }
        else
        {
            spawnPos = spawnPosPrevious;
        }
        return spawnPos;
    }
    public void CountDecrease()
    {
        count--;
    }
}
