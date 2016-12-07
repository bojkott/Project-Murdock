using UnityEngine;
using System.Collections;

public class SimpleObjectCollision : MonoBehaviour {

    AudioSource[] audio;
    public AudioClip[] collisionSounds;
    public float volumeModifier = 2;


    void Start()
    {
        audio = GetComponents<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
    
        float volume = collision.relativeVelocity.magnitude/volumeModifier;

        volume = Mathf.Clamp(volume, 0, 1);

        AudioClip sound = collisionSounds[Random.Range(0, collisionSounds.Length)];


        foreach(AudioSource a in audio)
        {
            a.clip = sound;
            a.volume = volume;
            a.PlayOneShot(sound);
        }
        

    }
}
