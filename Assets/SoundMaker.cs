using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        
        /*
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.clip = null;
        */

        audioSource.PlayOneShot(clip);
    }
}
