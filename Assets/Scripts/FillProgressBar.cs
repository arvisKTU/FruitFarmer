using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillProgressBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Image mask;
    private int fruits;
    public int maxFruits;
    private const string LEVEL_COMPLETED_CLIP_NAME = "level_completed";

    public GameManager gameManager;

    public void Start()
    {
        mask.fillAmount = 0;
    }
    public void AddFruit()
    {
        fruits++;
        float fillAmount = (float)fruits / (float)maxFruits;
        mask.fillAmount = fillAmount;
        if(fruits==maxFruits)
        {
            SoundManager.PlaySound(LEVEL_COMPLETED_CLIP_NAME);
            gameManager.EndGame();
        }
    }
}
