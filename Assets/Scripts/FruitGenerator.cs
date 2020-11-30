using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FruitGenerator : MonoBehaviour
{
    public GameObject blueberry;
    public GameObject strawberry;
    public GameObject peach;
    public GameObject blueberryBasket;
    public GameObject peachBasket;
    public GameObject strawberryBasket;
    public float distanceFromBasket;
    public float distanceWithinBasket;
    public float distanceFromBorders;
    public float spawnRate;
    public int maxFruitsToSpawn;

    private const int Z_POSITION = 0;
    private const int DIRECTIONS_COUNT = 2;
    private int startTime;
    private int fruitVarietyCount;
    private Vector3 spawnPos;
    private Vector3 stageDimensions;
    private Vector3 zeroVector;

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
        spawnPos = FindSpawnPosition();
        SelectFruitToSpawn(fruitNumber,spawnPos);
    }

    private void SelectFruitToSpawn(int fruitNumber,Vector3 spawnPos)
    {
        if (GameManager.instance.count < maxFruitsToSpawn)
        {
            switch (fruitNumber)
            {
                case 0:
                    Instantiate(blueberry, spawnPos, Quaternion.identity);
                    GameManager.instance.CountIncrease();
                break;

                case 1:
                    Instantiate(peach, spawnPos, Quaternion.identity);
                    GameManager.instance.CountIncrease();
                break;

                case 2:
                    Instantiate(strawberry, spawnPos, Quaternion.identity);
                    GameManager.instance.CountIncrease();
                break;
            }
        }
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
            xPosition = Random.Range(0, DIRECTIONS_COUNT) ==
                0 ? peachBasket.transform.position.x - distanceFromBasket : peachBasket.transform.position.x + distanceFromBasket;
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
}
