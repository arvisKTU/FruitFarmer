using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPooler : MonoBehaviour
{

    public GameObject pooledFruit;

    public int pooledAmount;

    List<GameObject> pooledFruits;
    void Start()
    {
        pooledFruits = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {


            GameObject fruit = (GameObject)Instantiate(pooledFruit, new Vector3(0, 0, 0), Quaternion.identity);
            fruit.SetActive(false);
            pooledFruits.Add(fruit);
        }
    }
    public GameObject GetPooledFruit()
    {
        for (int i = 0; i < pooledFruits.Count; i++)
        {
            if (!pooledFruits[i].activeInHierarchy)
            {
                return pooledFruits[i];
            }
        }

        //GameObject fruit = (GameObject)Instantiate(pooledFruit, FindSpawnPosition(), Quaternion.identity);
        //fruit.SetActive(false);
        //pooledFruits.Add(fruit);
        return null;
    }
}