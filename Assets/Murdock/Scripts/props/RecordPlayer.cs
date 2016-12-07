using UnityEngine;
using System.Collections;
using VRTK;
public class RecordPlayer : MonoBehaviour {

    public RecordPlayerNail nail;
    public GameObject spinPlatter;
    public VRTK_Knob speedKnob;


    private GameObject disk;

    private float discSpinSpeed = 180;

    AudioSource staticAudioSource;
    AudioSource audioSource;



    void Start()
    {

        staticAudioSource = GetComponent<AudioSource>();
        audioSource = staticAudioSource;
        audioSource.volume = 0;
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
        float speed = speedKnob.GetValue();

        speed += 1;

        if (speed > 2)
            speed = 4 - speed;
        else
            speed = (2 - 1.5f*speed);


        spinPlatter.transform.Rotate(new Vector3(0, discSpinSpeed*Time.deltaTime*speed, 0));


        if (disk)
            audioSource = disk.GetComponent<AudioSource>();


        audioSource.timeSamples = 10;
        audioSource.pitch = speed;

        if (nail.touchingPlatter)
            audioSource.volume = 1;
        else
            audioSource.volume = 0;


	}



}
