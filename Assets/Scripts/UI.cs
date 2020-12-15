using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Image mask;
    public GameObject progressBar;
    public GameObject winMenu;
    public int maxFruits;
    public GameObject restartButton;
    public Button restart;

    private Vector3 finalPosition;
    private Vector3 initialPosition;
    private float initialXPosition;
    private float initialYPosition;
    private float speed;

    private int fruitCount;
    private const float WIN_SCREEN_DELAY = 1f;
    private const float RESTART_BUTTON_DELAY = 3f;
    private UI instance;
    private float currTime;

    public static event Action OnEnoughFruitsCollectedEvent;

    void Start()
    {
        restart.interactable = false;
        initialXPosition = 1800f;
        initialYPosition = 500f;
        speed = 1500f;
        finalPosition = restartButton.transform.position;

        initialPosition = new Vector3(initialXPosition, initialYPosition, 0);
        restartButton.transform.position = initialPosition;

        mask.fillAmount = 0f;
        progressBar.SetActive(true);
        winMenu.gameObject.SetActive(false);
        Fruit.OnFruitCollectedEvent += FillProgressBar;
        GameManager.OnWinEvent +=InvokeWinScreenWithDelay;
        GameManager.OnRestartEvent += HandleRestart;
    }

    private void Update()
    {
        MoveRestartButton();
    }

    void FillProgressBar()
    {
        fruitCount++;
        float fillAmount = (float)fruitCount / (float)maxFruits;
        mask.fillAmount = fillAmount;
        if(IsEnoughFruitsCollected())
        {
            OnEnoughFruitsCollectedEvent?.Invoke();
        }
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

    bool IsEnoughFruitsCollected()
    {
        return fruitCount == maxFruits;
    }

    void HandleRestart()
    {
        mask.fillAmount = 0f;
        fruitCount = 0;
        progressBar.SetActive(true);
        winMenu.gameObject.SetActive(false);
        initialPosition = new Vector3(initialXPosition, initialYPosition, 0);
        restart.interactable = false;
    }

    private void MoveRestartButton()
    {
        initialPosition = Vector3.MoveTowards(initialPosition, finalPosition, speed * Time.deltaTime);
        currTime += Time.deltaTime;
        restartButton.transform.position = initialPosition;
        if (currTime >= RESTART_BUTTON_DELAY)
        {
            restart.interactable = true;
        }
    }

}
