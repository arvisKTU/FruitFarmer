using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Image mask;
    public GameObject progressBar;
    public GameObject winMenu;
    public bool gameEnded;
    public int count;
    public FruitGenerator fruitGenerator;
    public int maxFruits;

    private int fruits;
    private const string LEVEL_COMPLETED_CLIP_NAME = "level_completed";
    private const string MAIN_SCENE                = "FruitFarmer";
    private const float WIN_SCREEN_DELAY = 1f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        count++;
        mask.fillAmount = 0f;
        gameEnded = false;
        progressBar.SetActive(true);
        winMenu.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        gameEnded = true;
        fruitGenerator.CancelInvoke();
        Invoke(nameof(WinScreen), WIN_SCREEN_DELAY);
    }

    public void WinScreen()
    {
        winMenu.gameObject.SetActive(true);
        progressBar.SetActive(false);
    }

    public void Restart()
    {
        mask.fillAmount = 0f;
        gameEnded =  false;
        progressBar.SetActive(true);
        winMenu.gameObject.SetActive(false);
        SceneManager.LoadScene(MAIN_SCENE);
    }

    public void AddFruit()
    {
        fruits++;
        float fillAmount = (float)fruits / (float)maxFruits;
        mask.fillAmount = fillAmount;
        if (fruits == maxFruits)
        {
            SoundManager.PlaySound(LEVEL_COMPLETED_CLIP_NAME);
            EndGame();
        }
    }

    public void CountDecrease()
    {
        count--;
    }

    public void CountIncrease()
    {
        count++;
    }


}
