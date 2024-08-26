using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class IntroSoundScript : MonoBehaviour
{
    public AudioClip FirstClip;
    public AudioClip SecondClip;
    public AudioClip ThirdClip;
    
    void Start()
    {
        StartCoroutine(playSound());
    }
    IEnumerator playSound()
    {
        GetComponent<AudioSource>().clip = FirstClip;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(FirstClip.length - 1);
        GetComponent<AudioSource>().clip = SecondClip;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(SecondClip.length - 1.6f);
        //GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().clip = ThirdClip;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = true;
        DontDestroyOnLoad(this.ThirdClip);
    }

    
}

