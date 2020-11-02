using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFruit : MonoBehaviour
{
    private bool isDragging;
    
    public GameObject correctBasket;
    private Vector2 initialPos;
    private Vector3 screenPoint;
    private Vector3 offset;
    public float distanceWithinBasket;

    public FillProgressBar progressBar;
    public FruitGenerator generator;
    public GameManager gameManager;

    private void Start()
    {
        initialPos = transform.position;
    }
    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }
    private void OnMouseDown()
    {
        if(gameManager.canDrag)
        isDragging = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (Mathf.Abs(transform.position.x - correctBasket.transform.position.x) <= distanceWithinBasket &&
            (Mathf.Abs(transform.position.y - correctBasket.transform.position.y) <= distanceWithinBasket))
        {
            SoundManager.PlaySound("put_into_basket");
            Destroy(this.gameObject);
            generator.CountDecrease();
            progressBar.AddFruit();
        }
        else
        {
            transform.position = initialPos;
        }
    }
}
