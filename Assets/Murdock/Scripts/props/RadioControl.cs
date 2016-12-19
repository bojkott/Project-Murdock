using UnityEngine;
using System.Collections;
using VRTK;
public class RadioControl : MonoBehaviour {

    private Transform lowPoint;
    private Transform highPoint;
    private Transform frequencyIndicator;
    [Range(0.0f, 1.0f)]
    public float volume=1.0f;
    [Range(0.0f, 1.0f)]
    public float frequecy=1.0f;

    

    public VRTK_Knob frequencyKnob;
    public VRTK_Knob volumeKnob;

    [Range(0.0f, 1.0f)]
    public float correctFrequency;

    public float staticBlurRange;

    private AudioSource StaticAudioSource;
    private AudioSource correctAudioSource;

    private float baseStaticVolume;
    private float baseCorrectVolume;

	// Use this for initialization
	void Start () {
        lowPoint = transform.FindChild("Low point");
        highPoint = transform.FindChild("High point");
        frequencyIndicator = transform.FindChild("Frequency tick");

        StaticAudioSource = transform.FindChild("Static audio").GetComponent<AudioSource>();
        correctAudioSource = transform.FindChild("Correct audio").GetComponent<AudioSource>();

        baseStaticVolume = StaticAudioSource.volume;
        baseCorrectVolume = correctAudioSource.volume;
    }
	
	// Update is called once per frame
	void Update () {

        bool volumeChanged = false;
        float staticVolume = 0;
        float correctVolume = 0;

        float newFrequency = frequencyKnob.GetValue();

        if (Mathf.Abs(frequecy - newFrequency) < 0.1f)
            frequecy = newFrequency;


        float newVolume = volumeKnob.GetValue();
        if (Mathf.Abs(volume - newVolume) < 0.1f)
            volume = newVolume;


        float freqDist = Mathf.Abs(frequecy - correctFrequency);

        if(freqDist == 0)
        {
            staticVolume = 0;
            correctVolume = 1;
            volumeChanged = true;
        }
        else
        {
            float vol = freqDist / staticBlurRange;
            vol = Mathf.Clamp(vol, 0, 1);

            staticVolume = vol;
            correctVolume = (1 - vol);
            volumeChanged = true;

        }

        if (volumeChanged)
        {
            StaticAudioSource.volume = baseStaticVolume * staticVolume * volume;
            correctAudioSource.volume = baseCorrectVolume * correctVolume * volume;
        }

        frequencyIndicator.transform.position = Vector3.Lerp(lowPoint.position, highPoint.position, frequecy);

    }
}
