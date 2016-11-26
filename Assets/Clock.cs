using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

    public AudioClip strikeSound;
    private AudioSource audio;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (strikeSound)
            {
                audio.PlayOneShot(strikeSound);
                audio.PlayDelayed(strikeSound.length);
            }
                
        }
	}


}
