using UnityEngine;
using System.Collections;

public class MicophoneWaveSpawner : MonoBehaviour {

    
    private AudioSource _audio;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();

       
    }
    void Start()
    {
        _audio.clip = Microphone.Start("Built-in Microphone", true, 1, 44100);

    }
    void Update()
    {


        if (_audio.isPlaying)
        {

        }
        else if (_audio.clip.isReadyToPlay)
            _audio.Play();
        else
        {
            _audio.clip = Microphone.Start("Built-in Microphone", true, 1, 44100);
        }

        }
    }

}
