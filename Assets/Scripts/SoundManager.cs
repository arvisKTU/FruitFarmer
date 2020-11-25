using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PUT_INTO_BASKET_CLIP_NAME = "put_into_basket";
    private const string LEVEL_COMPLETED_CLIP_NAME = "level_completed";

    public static AudioClip putInBasket, levelCompleted;
    static AudioSource audioSrc;

    void Start()
    {
        putInBasket = Resources.Load<AudioClip>(PUT_INTO_BASKET_CLIP_NAME);
        levelCompleted = Resources.Load<AudioClip>(LEVEL_COMPLETED_CLIP_NAME);

        audioSrc = GetComponent<AudioSource> ();
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case PUT_INTO_BASKET_CLIP_NAME:
                audioSrc.PlayOneShot(putInBasket);
                break;

            case LEVEL_COMPLETED_CLIP_NAME:
                audioSrc.PlayOneShot(levelCompleted);
                break;
        }

    }
}
