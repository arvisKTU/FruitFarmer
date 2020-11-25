using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDragAndDrop : MonoBehaviour
{
    private const string PUT_INTO_BASKET_CLIP_NAME = "put_into_basket";
    public bool isDragging;
    
    public GameObject correctBasket;
    private Vector2 initialPos;
    private Vector3 screenPoint;
    private Vector3 offset;
    public float distanceWithinBasket;

    public FillProgressBar progressBar;
    public FruitGenerator generator;
    public GameManager gameManager;

    private Camera mainCamera;

    private void Start()
    {
        initialPos = transform.position;
        mainCamera = Camera.main;
    }
    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = mainCamera.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }
    private void OnMouseDown()
    {
        if (!gameManager.gameEnded)
        {
            isDragging = true;
            screenPoint = mainCamera.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }
    private void OnMouseUp()
    {
        isDragging = false;
        if (Mathf.Abs(transform.position.x - correctBasket.transform.position.x) <= distanceWithinBasket &&
            (Mathf.Abs(transform.position.y - correctBasket.transform.position.y) <= distanceWithinBasket))
        {
            SoundManager.PlaySound(PUT_INTO_BASKET_CLIP_NAME);
            generator.CountDecrease();
            progressBar.AddFruit();
            this.gameObject.SetActive(false);
        }
        else
        {
            transform.position = initialPos;
        }
    }
}
