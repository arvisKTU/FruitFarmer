using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioClip putInBasket, levelCompleted;
    static AudioSource audioSrc;
    void Start()
    {
        putInBasket = Resources.Load<AudioClip>("put_into_basket");
        levelCompleted = Resources.Load<AudioClip>("level_completed");

        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "put_into_basket":
                audioSrc.PlayOneShot(putInBasket);
                break;

            case "level_completed":
                audioSrc.PlayOneShot(levelCompleted);
                break;
        }

    }
}
