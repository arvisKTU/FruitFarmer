 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButtonMovement : MonoBehaviour
{

    public GameObject restartButton;
    public Button restart;

    private Vector3 initialPos;
    private Vector3 startPosition;
    private float startXPosition;
    private float startYPosition;
    private float moveSpeed;
    private float moveDelay;

    void Start()
    {
        restart.interactable = false;
        startXPosition = 100f;
        startYPosition = 0f;
        moveDelay = 2f;
        moveSpeed = 2000f;
        initialPos = restartButton.transform.position;

        startPosition = new Vector3(startXPosition, startYPosition, 0);
        restartButton.transform.position = startPosition;
        restartButton.SetActive(false);

    }

    void Update()
    {
        Invoke(nameof(MoveRestartButton), moveDelay);
    }

    private void MoveRestartButton()
    {
        restartButton.SetActive(true);
        startPosition = Vector3.MoveTowards(startPosition, initialPos, moveSpeed * Time.deltaTime);
        restartButton.transform.position = startPosition;
        if(initialPos==restartButton.transform.position)
        {
            restart.interactable = true;
        }
    }
}
