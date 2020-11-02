using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillProgressBar : MonoBehaviour
{
    // Start is called before the first frame update

    private Slider slider;
    private int fruits;
    public int maxFruits;

    public GameManager gameManager;

    public void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
        slider.maxValue = maxFruits;
    }
    public void AddFruit()
    {
        fruits++;
        slider.value = fruits;
        if(fruits==maxFruits)
        {
            SoundManager.PlaySound("level_completed");
            gameManager.EndGame();
        }
    }
}
