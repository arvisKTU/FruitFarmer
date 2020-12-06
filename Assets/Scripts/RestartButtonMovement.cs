 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButtonMovement : MonoBehaviour
{

    public GameObject restartButton;
    public Button restart;

    private Vector3 finalPosition;
    private Vector3 initialPosition;
    private float initialXPosition;
    private float initialYPosition;
    private float speed;

    void Start()
    {
        restart.interactable = false;
        initialXPosition = 100f;
        initialYPosition = 0f;
        speed = 2000f;
        finalPosition = restartButton.transform.position;

        initialPosition = new Vector3(initialXPosition, initialYPosition, 0);
        restartButton.transform.position = initialPosition;
        restartButton.SetActive(false);

    }

    void Update()
    {
        MoveRestartButton();
    }

    private void MoveRestartButton()
    {
        restartButton.SetActive(true);
        initialPosition = Vector3.MoveTowards(initialPosition, finalPosition, speed * Time.deltaTime);
        restartButton.transform.position = initialPosition;
        if (finalPosition==restartButton.transform.position)
        {
            restart.interactable = true;
        }
    }
}
