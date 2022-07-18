using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource DuckSound;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayDuckSound()
    {
        DuckSound.Play();
    }
}