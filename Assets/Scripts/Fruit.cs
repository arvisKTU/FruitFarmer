using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public FruitData data;

    private Vector3 screenPoint;
    private Camera mainCamera;
    private bool isDragging;
    private Vector3 initialPos;
    private bool fruitInCorrectBasket;
    private float scaleChangeSpeed;
    private float currentShrinkScale;
    private float currentExpandScale;
    private Vector3 initialScale;
    private bool isMaximized;
    private float currTime;
    private float returnTime;
    private Vector3 offset;
    private float maxVelocity;
    private GameManager gm;

    public event Action OnFruitCollectedEvent;

    private void Start()
    {
        gm = GameManager.instance;
        returnTime = 1f;
        maxVelocity = 100f;
        fruitInCorrectBasket = false;
        isMaximized = false;
        scaleChangeSpeed = 0.02f;
        initialPos = transform.position;
        mainCamera = Camera.main;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (isDragging)
        {
            currTime = 0;
        }
        else if(!fruitInCorrectBasket)
        {
            ReturnToStartPos();
        }
        if (fruitInCorrectBasket)
        {
            ShrinkFruit();
        }
        if (!fruitInCorrectBasket && !isMaximized)
        {
            ExpandFruit();
        }
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
        if (!gm.gameEnded)
        {
            isDragging = true;
            screenPoint = mainCamera.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            Instantiate(data.fruitClickParticles, transform.position, Quaternion.identity);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (IsFruitInBasket(data.correctBasket))
        {
            fruitInCorrectBasket = true;
            OnFruitCollectedEvent?.Invoke();
            Instantiate(data.correctParticles, transform.position, Quaternion.identity);
        }
        else
        {
            for (int i=0;i<gm.baskets.Length;i++)
            {
                if (IsFruitInBasket(gm.baskets[i])&& (gm.baskets[i]!=data.correctBasket))
                {
                    Instantiate(data.wrongParticles, transform.position, Quaternion.identity);
                }
            }
        }
    }

    private bool IsFruitInBasket(GameObject basket)
    {
        return Mathf.Abs(transform.position.x - basket.transform.position.x) <= data.maxDistanceWithinBasket &&
            (Mathf.Abs(transform.position.y - basket.transform.position.y) <= data.maxDistanceWithinBasket);
    }

    private void ReturnToStartPos()
    {

        Vector3 _currPos = transform.position;
        currTime += Time.deltaTime;
        float _fraction = currTime / returnTime;
        float _velocity = data.returnFruitToInitialPos.Evaluate(_fraction) / maxVelocity;
        _currPos = Vector3.Lerp(_currPos, initialPos, _velocity);
        transform.position = _currPos;

    }

    private void ShrinkFruit()
    {
        currentShrinkScale += scaleChangeSpeed;
        transform.localScale = new Vector3(initialScale.x- currentShrinkScale, initialScale.y - currentShrinkScale, initialScale.z - currentShrinkScale);
        if (transform.localScale.x <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void ExpandFruit()
    {
        if (currentExpandScale<initialScale.x)
        {
            currentExpandScale += scaleChangeSpeed;
            transform.localScale = new Vector3(currentExpandScale, currentExpandScale, currentExpandScale);
        }
        else
        {
            isMaximized = true;
        }
    }
}
