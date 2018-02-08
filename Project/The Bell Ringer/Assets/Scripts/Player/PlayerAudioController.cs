using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour {

    private AudioSource playerAudioSource;

    private void Start()
    {
        playerAudioSource = this.GetComponent<AudioSource>();
    }

    private void PlayFootStep()
    {
        playerAudioSource.Play();
    }
}
