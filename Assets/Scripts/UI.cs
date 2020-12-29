using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GeneratorData settings;
    public Image mask;
    public GameObject progressBar;
    public GameObject winMenu;
    public GameObject restartButton;
    public Button restart;

    private Vector3 finalPosition;
    private Vector3 initialPosition;

    private const float WIN_SCREEN_DELAY = 1f;
    private const float RESTART_BUTTON_DELAY = 3f;
    private const float SPEED = 1500f;
    private const float INITIAL_X_POSITION = 1800f;
    private const float INITIAL_Y_POSITION = 500f;

    private UI instance;
    private float currTime;

    void Start()
    {
        restart.interactable = false;
        finalPosition = restartButton.transform.position;

        initialPosition = new Vector3(INITIAL_X_POSITION, INITIAL_Y_POSITION, 0);
        restartButton.transform.position = initialPosition;

        mask.fillAmount = 0f;
        progressBar.SetActive(true);
        winMenu.gameObject.SetActive(false);
        FruitGenerator.FruitsCollected += FillProgressBar;
        GameManager.OnWinEvent +=InvokeWinScreenWithDelay;
        GameManager.OnRestartEvent += HandleRestart;
    }

    private void OnDestroy()
    {
        FruitGenerator.FruitsCollected -= FillProgressBar;
        GameManager.OnWinEvent -= InvokeWinScreenWithDelay;
        GameManager.OnRestartEvent -= HandleRestart;
    }

    private void Update()
    {
        MoveRestartButton();
    }

    void FillProgressBar(int fruitCount)
    {
        float fillAmount = (float)fruitCount / (float)settings.maxFruits;
        mask.fillAmount = fillAmount;
    }
    void InvokeWinScreenWithDelay()
    {
        Invoke(nameof(WinScreen), WIN_SCREEN_DELAY);
    }

    public void WinScreen()
    {
        winMenu.gameObject.SetActive(true);
        progressBar.SetActive(false);
    }

    void HandleRestart()
    {
        mask.fillAmount = 0f;
        progressBar.SetActive(true);
        winMenu.gameObject.SetActive(false);
        initialPosition = new Vector3(INITIAL_X_POSITION, INITIAL_Y_POSITION, 0);
        restart.interactable = false;
    }

    private void MoveRestartButton()
    {
        initialPosition = Vector3.MoveTowards(initialPosition, finalPosition, SPEED * Time.deltaTime);
        currTime += Time.deltaTime;
        restartButton.transform.position = initialPosition;
        if (currTime >= RESTART_BUTTON_DELAY)
        {
            restart.interactable = true;
        }
    }

}
