using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static AudioSource audioSrc;

    private const string PUT_INTO_BASKET_CLIP_NAME = "put_into_basket";
    private const string LEVEL_COMPLETED_CLIP_NAME = "level_completed";
    private const float AUDIO_SOURCE_STOP_DELAY = 2.7f;
    private AudioClip putIntoBasket, levelCompleted, soundtrack;

    void Start()
    {
        putIntoBasket = Resources.Load<AudioClip>(PUT_INTO_BASKET_CLIP_NAME);
        levelCompleted = Resources.Load<AudioClip>(LEVEL_COMPLETED_CLIP_NAME);

        audioSrc = GetComponent<AudioSource> ();

        GameManager.OnWinEvent += HandleWin;
        Fruit.OnFruitCollectedEvent += PlayCollectedFruitSound;
        GameManager.OnRestartEvent += audioSrc.Play;
    }

    private void OnDestroy()
    {
        GameManager.OnWinEvent -= HandleWin;
        Fruit.OnFruitCollectedEvent -= PlayCollectedFruitSound;
        GameManager.OnRestartEvent -= audioSrc.Play;
    }

    private void HandleWin()
    {
        PlaySound(LEVEL_COMPLETED_CLIP_NAME);
        Invoke(nameof(StopSounds), AUDIO_SOURCE_STOP_DELAY);
    }

    private void PlayCollectedFruitSound()
    {
        PlaySound(PUT_INTO_BASKET_CLIP_NAME);
    }

    private void PlaySound(string clip)
    {
        switch(clip)
        {
            case PUT_INTO_BASKET_CLIP_NAME:
                audioSrc.PlayOneShot(putIntoBasket);
                break;

            case LEVEL_COMPLETED_CLIP_NAME:
                audioSrc.PlayOneShot(levelCompleted);
                break;
        }

    }

    private void StopSounds()
    {
        audioSrc.Stop();
    }

}
