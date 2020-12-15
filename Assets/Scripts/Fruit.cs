using System;
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
    private bool wentOutsideBasket;

    public static event Action OnFruitCollectedEvent;

    private void Start()
    {
        wentOutsideBasket = true;
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
        if (!IsFruitInAnyBasket())
        {
            wentOutsideBasket = true;
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
        if (!GameManager.gameEnded && !IsFruitInAnyBasket())
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
        if (IsFruitInDefinedBasket(data.correctBasket)&& !fruitInCorrectBasket)
        {
            fruitInCorrectBasket = true;
            OnFruitCollectedEvent?.Invoke();
            Instantiate(data.correctParticles, transform.position, Quaternion.identity);
        }
        else if(!IsFruitInDefinedBasket(data.correctBasket))
        {
            for (int i=0;i<GameManager.baskets.Length;i++)
            {
                if (IsFruitInDefinedBasket(GameManager.baskets[i])&& (GameManager.baskets[i]!=data.correctBasket) && wentOutsideBasket)
                {
                    Instantiate(data.wrongParticles, transform.position, Quaternion.identity);
                    wentOutsideBasket = false;
                }
            }
        }
    }

    private bool IsFruitInDefinedBasket(GameObject basket)
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

    private bool IsFruitInAnyBasket()
    {
        float _count = 0;

        for (int i = 0; i < GameManager.baskets.Length; i++)
        {
            if (IsFruitInDefinedBasket(GameManager.baskets[i]))
            {
                _count++;
            }
        }

        if(_count>0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
