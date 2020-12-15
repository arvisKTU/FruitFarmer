using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject[] baskets;
    public static bool gameEnded;
    public static event Action OnWinEvent;
    public static event Action OnRestartEvent;

    public GameObject[] assignedBaskets;

    private GameManager instance;
    private const string MAIN_SCENE  = "FruitFarmer";

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        baskets = assignedBaskets;
        gameEnded = false;
        UI.OnEnoughFruitsCollectedEvent += EndGame;
    }

    public void EndGame()
    {
        OnWinEvent?.Invoke();
        gameEnded = true;
    }

    public void Restart()
    {
        OnRestartEvent?.Invoke();
        gameEnded = false;
        DeleteFruits();

    }

    private void DeleteFruits()
    {
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        foreach (GameObject fruit in fruits)
        {
            Destroy(fruit);
        }
    }
}
