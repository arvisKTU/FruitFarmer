using System;
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
    public int generatedFruitCount;
    public FruitGenerator fruitGenerator;
    public int maxFruits;
    public GameObject[] baskets;

    private int fruitCount;
    private const string LEVEL_COMPLETED_CLIP_NAME = "level_completed";
    private const string PUT_INTO_BASKET_CLIP_NAME = "put_into_basket";
    private const string MAIN_SCENE                = "FruitFarmer";
    private const float WIN_SCREEN_DELAY           = 1f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        generatedFruitCount++;
        mask.fillAmount = 0f;
        gameEnded = false;
        progressBar.SetActive(true);
        winMenu.gameObject.SetActive(false);
    }


    public void AddFruit()
    {
        SoundManager.PlaySound(PUT_INTO_BASKET_CLIP_NAME);
        CountDecrease();
        fruitCount++;
        float fillAmount = (float)fruitCount / (float)maxFruits;
        mask.fillAmount = fillAmount;
        if (fruitCount == maxFruits)
        {
            SoundManager.PlaySound(LEVEL_COMPLETED_CLIP_NAME);
            EndGame();
        }
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

    public void CountDecrease()
    {
        generatedFruitCount--;
    }

    public void CountIncrease()
    {
        generatedFruitCount++;
    }


}
