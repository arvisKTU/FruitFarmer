using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider slider;
    public GameObject winMenu;
    public bool canDrag;

    //public string mainMenuLevel;
    private void Start()
    {
        canDrag = true;
        winMenu.gameObject.SetActive(false);
        slider.gameObject.SetActive(true);
    }
    public void EndGame()
    {
        slider.gameObject.SetActive(false);
        canDrag = false;
        winMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void Restart()
    {
        canDrag = true;
        Time.timeScale = 1;
        winMenu.gameObject.SetActive(false);
        slider.gameObject.SetActive(true);
        SceneManager.LoadScene("FruitFarmer");
    }

}
