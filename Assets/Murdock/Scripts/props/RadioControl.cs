using UnityEngine;
using System.Collections;
using VRTK;
public class RadioControl : MonoBehaviour {

    private Transform lowPoint;
    private Transform highPoint;
    private Transform volumeIndicator;
    private float volume=1.0f;
    private float pitch=1.0f;

    public bool hasInteracted = false;

    public VRTK_Knob volumeKnob;
    public VRTK_Knob pitchKnob;

    private AudioSource audio;
	// Use this for initialization
	void Start () {
        lowPoint = transform.FindChild("Low point");
        highPoint = transform.FindChild("High point");
        volumeIndicator = transform.FindChild("Volume tick");
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {


        float newVolume = volumeKnob.GetValue();

        if (Mathf.Abs(volume - newVolume) < 0.1f)
            volume = newVolume;
            

        float newPitch = pitchKnob.GetValue();
            pitch = newPitch;


        if(!hasInteracted)
        {
            if (volume != 1.0f || pitch != 1.0f)
                hasInteracted = true;
        }

        if (pitch > 3)
            pitch -= 6;


        volumeIndicator.transform.position = Vector3.Lerp(lowPoint.position, highPoint.position, volume);


        audio.volume = volume;
        audio.pitch = pitch;
    }
}
