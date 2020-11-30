using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDragAndDrop : MonoBehaviour
{
    public GameObject correctBasket;
    public GameObject[] wrongBaskets;
    public float distanceWithinBasket;

    public GameObject fruitClickParticles;
    public GameObject wrongParticles;
    public GameObject correctParticles;

    private Vector3 screenPoint;
    private Vector3 offset;
    private Camera mainCamera;
    private const string PUT_INTO_BASKET_CLIP_NAME = "put_into_basket";
    private bool isDragging;
    private float velocity;
    private float velocityRate;
    private Vector3 initialPos;
    private bool fruitInCorrectBasket;
    private float scaleChangeSpeed;
    private float currentShrinkScale;
    private float currentExpandScale;
    private Vector3 initialScale;
    private bool isMaximized;
    private float destroyTimeDelay;

    private void Start()
    {
        destroyTimeDelay = 1f;
        fruitInCorrectBasket = false;
        velocityRate = 5f;
        isMaximized = false;
        scaleChangeSpeed = 0.02f;
        initialPos = transform.position;
        mainCamera = Camera.main;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        ReturnToStartPos();
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
        if (!GameManager.instance.gameEnded)
        {
            isDragging = true;
            screenPoint = mainCamera.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            GameObject _touchParticlesInstance = (GameObject)Instantiate(fruitClickParticles, transform.position, Quaternion.identity);
            Destroy(_touchParticlesInstance, destroyTimeDelay);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (Mathf.Abs(transform.position.x - correctBasket.transform.position.x) <= distanceWithinBasket &&
            (Mathf.Abs(transform.position.y - correctBasket.transform.position.y) <= distanceWithinBasket))
        {
            fruitInCorrectBasket = true;
            SoundManager.PlaySound(PUT_INTO_BASKET_CLIP_NAME);
            GameManager.instance.CountDecrease();
            GameManager.instance.AddFruit();

            GameObject _correctParticlesInstance = (GameObject)Instantiate(correctParticles, transform.position, Quaternion.identity);
            Destroy(_correctParticlesInstance, destroyTimeDelay);
        }
        else
        {
            for(int i=0;i<wrongBaskets.Length;i++)
            {
                if (Mathf.Abs(transform.position.x - wrongBaskets[i].transform.position.x) <= distanceWithinBasket &&
            (Mathf.Abs(transform.position.y - wrongBaskets[i].transform.position.y) <= distanceWithinBasket))
                {
                    GameObject _wrongParticlesInstance = (GameObject)Instantiate(wrongParticles, transform.position, Quaternion.identity);
                    Destroy(_wrongParticlesInstance, destroyTimeDelay);
                }
            }
        }
    }

    private void ReturnToStartPos()
    {
        if (isDragging)
        {
            velocity = 0;
            return;
        }
        velocity += velocityRate * Time.deltaTime;

        Vector3 _currentPosition = transform.position;
        _currentPosition = Vector3.MoveTowards(_currentPosition, initialPos, velocity * Time.deltaTime);
        transform.position = _currentPosition;
    }

    private void ShrinkFruit()
    {
        if(transform.localScale.x<=0)
        {
            Destroy(this.gameObject);
        }
        currentShrinkScale += scaleChangeSpeed;
        transform.localScale = new Vector3(initialScale.x- currentShrinkScale, initialScale.y - currentShrinkScale, initialScale.z - currentShrinkScale);
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
