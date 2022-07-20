using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource DuckSound;
    public AudioSource AmbientalMusic;
    public AudioSource EndGameMusic;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayDuckSound()
    {
        DuckSound.Play();
    }

    public void PlayAmbientalMusic()
    {
        AmbientalMusic.Play();
    }

    public void PlayEndGameMusic()
    {
        EndGameMusic.Play();
    }
}