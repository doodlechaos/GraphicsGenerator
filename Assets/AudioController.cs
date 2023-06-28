using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController inst;

    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        inst = this;
    }


    public void PlaySound(AudioClip audioClip, float pitchMin, float pitchMax)
    {
        float pitch = Random.Range(pitchMin, pitchMax);
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(audioClip);
    }


}