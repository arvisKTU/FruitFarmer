using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const string MAIN_SCENE = "FruitFarmer";

    public GameObject progressBar;
    public GameObject winMenu;
    public static GameManager instance;
    public bool gameEnded;
    public FruitGenerator fruitGenerator;

    private void Start()
    {
        gameEnded = false;
        progressBar.SetActive(true);
        winMenu.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        gameEnded = true;
        progressBar.SetActive(false);
        winMenu.gameObject.SetActive(true);
        fruitGenerator.CancelInvoke();
    }

    public void Restart()
    {
        gameEnded =  false;
        progressBar.SetActive(true);
        winMenu.gameObject.SetActive(false);
        SceneManager.LoadScene(MAIN_SCENE);
    }

}
