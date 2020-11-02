using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FruitGenerator : MonoBehaviour
{
    public GameObject blueberry;
    public GameObject strawberry;
    public GameObject peach;
    private Vector3 spawnPos;
    public GameObject blueberryBasket;
    public GameObject peachBasket;
    public GameObject strawberryBasket;
    public float spawnRate;
    public float distanceFromBasket;
    public float distanceWithinBasket;
    public int maxFruitsGenerated;
    private int count = 0;

    public float distanceFromBorders;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, spawnRate);
        spawnPos = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void Spawn()
    {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        int fruitNumber = Random.Range(1, 4);
        float xPosition = Random.Range(-Mathf.Abs(stageDimensions.x)+distanceFromBorders, Mathf.Abs(stageDimensions.x)-distanceFromBorders);
        float yPosition = Random.Range(-Mathf.Abs(stageDimensions.y) + distanceFromBorders, Mathf.Abs(stageDimensions.y) - distanceFromBorders);
        spawnPos = new Vector3(xPosition, yPosition,0);
        if (Mathf.Abs(spawnPos.x - blueberryBasket.transform.position.x) <= distanceWithinBasket &&
            (Mathf.Abs(spawnPos.y - blueberryBasket.transform.position.y) <= distanceWithinBasket))
        {
            xPosition = blueberryBasket.transform.position.x + distanceFromBasket;
            spawnPos= new Vector3(xPosition, yPosition, 0);
        }
        else if (Mathf.Abs(spawnPos.x - peachBasket.transform.position.x) <= distanceWithinBasket &&
           (Mathf.Abs(spawnPos.y - peachBasket.transform.position.y) <= distanceWithinBasket))
        {
            xPosition = Random.Range(0, 2) == 0 ? peachBasket.transform.position.x - distanceFromBasket : peachBasket.transform.position.x + distanceFromBasket;
            spawnPos = new Vector3(xPosition, yPosition, 0);
        }
        else if (Mathf.Abs(spawnPos.x - strawberryBasket.transform.position.x) <= distanceWithinBasket &&
           (Mathf.Abs(spawnPos.y - strawberryBasket.transform.position.y) <= distanceWithinBasket))
        {
            xPosition = strawberryBasket.transform.position.x - distanceFromBasket;
            spawnPos = new Vector3(xPosition, yPosition, 0);
        }

        switch (fruitNumber)
        {
            case 1:
                if (count < maxFruitsGenerated)
                {
                    Instantiate(blueberry, spawnPos, Quaternion.identity);
                    count++;
                }
                break;

            case 2:
                if (count < maxFruitsGenerated)
                {
                    Instantiate(peach, spawnPos, Quaternion.identity);
                    count++;
                }
                break;
            case 3:
                if (count < maxFruitsGenerated)
                {
                    Instantiate(strawberry, spawnPos, Quaternion.identity);
                    count++;
                }
                break;
        }
    }
    public void CountDecrease()
    {
        count--;
    }
}
