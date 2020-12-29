using System;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    public FruitData data;
    private const float PARTICLE_DELAY_TIME = 1f;
    private const float RETURN_TIME         = 1f;
    private const float MAX_VELOCITY        = 100f;
    private const float RESIZE_TIME = 1f;

    private Vector3 screenPoint;
    private Camera mainCamera;
    private bool isDragging;
    private Vector3 initialPos;
    private bool fruitInCorrectBasket;
    private Vector3 initialScale;
    private Vector3 zeroVector;
    private bool isMaximized;
    private Vector3 offset;
    private bool returnFruit;
    private bool wentOutsideBasket;
    private float timeFromLastParticle;
    public static event Action OnFruitCollectedEvent;

    private void Start()
    {
        returnFruit = false;
        zeroVector = new Vector3(0, 0, 0);
        timeFromLastParticle = MAX_VELOCITY;
        wentOutsideBasket = true;
        fruitInCorrectBasket = false;
        isMaximized = false;
        initialPos = transform.position;
        mainCamera = Camera.main;
        initialScale = transform.localScale;
        transform.localScale = zeroVector;
    }

    private void Update()
    {
        timeFromLastParticle += Time.deltaTime;

        if(!fruitInCorrectBasket && returnFruit)
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

            if (timeFromLastParticle >= PARTICLE_DELAY_TIME)
            {
                Instantiate(data.fruitClickParticles, transform.position, Quaternion.identity);
                timeFromLastParticle = 0;
            }
        }
    }

    private void OnMouseUp()
    {
        returnFruit = true;
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
        LeanTween.move(gameObject, initialPos, RETURN_TIME).setEase(data.returnFruitToInitialPos).setOnComplete(DisableFruitReturn);
    }

    private void DisableFruitReturn()
    {
        returnFruit = false;
    }

    private void ShrinkFruit()
    {
        LeanTween.scale(gameObject, zeroVector, RESIZE_TIME).setOnComplete(DestroyFruit);
    }

    private void ExpandFruit()
    {
        LeanTween.scale(gameObject, initialScale, RESIZE_TIME);
        isMaximized = true;
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

    void DestroyFruit()
    {
        Destroy(gameObject);
    }
}
