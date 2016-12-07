using UnityEngine;
using System.Collections;

public class NewSoundWaveEmitter : MonoBehaviour {

    [Range(0.0f, 1.0f)]
    public float SphereSpawnThreshold;

    [Range(0.0f, 1.0f)]
    public float SphereSpawnInterval;

    public Transform origin;
    [Range(0.0f, 1.0f)]
    public float saturation = 0.5f;
    [Range(0.0f, 1.0f)]
    public float vibrance = 1.0f;
    [Range(1.0f, 1000.0f)]
    public float amp = 1.0f;

    public Color colorMod = Color.white;

    [Range(1.0f, 1000.0f)]
    public float sensitivity = 100;
    [Range(1.0f, 10.0f)]
    public float maxRadius = 10;
    private AudioSource _audio;
    private AudioSource sourceAudio;
    private NewWaveManager wm;
    private float time;

    private NewWave wave;


    void Start()
    {

        sourceAudio = GetComponent<AudioSource>();
        _audio = gameObject.AddComponent<AudioSource>();
        UpdateAudio();
        _audio.spatialBlend = 0;
        _audio.outputAudioMixerGroup = sourceAudio.outputAudioMixerGroup;
        sourceAudio.outputAudioMixerGroup = null;

        if (_audio.playOnAwake)
            _audio.Play();

        wm = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<NewWaveManager>();
        if (!origin)
            origin = transform;
        wave = wm.CreateWave(origin.position, colorMod);


    }
    void Update()
    {
        UpdateAudio();


        time += Time.deltaTime;

        float loudness = GetAveragedVolume(0);
        loudness *= sensitivity;

        if (loudness > 0)
        {

            float hue = GetSpectrum(0);




            Color color = Color.HSVToRGB(hue*amp, saturation, vibrance) + colorMod;


            float roundedHue = ((int)(hue*amp * 10));
            roundedHue /= 10;
            float radius = loudness;

            radius = Mathf.Clamp(radius, 0, maxRadius);
            radius *= roundedHue;
            color *= (radius / maxRadius); 

            if(roundedHue > SphereSpawnThreshold && time > SphereSpawnInterval)
            {
                wm.CreateSphere(origin.position, color, radius);
                time = 0;
                
            }

            wave.SetNextRadius(radius);  
            wave.SetNextColor(color);
            wave.position = origin.position;

        }
    }
    float GetAveragedVolume(int channel)
    {


        float[] data = new float[1024];
        float a = 0;
        _audio.GetOutputData(data, channel);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 1024;


    }


    float GetSpectrum(int channel)
    {
        float hue = 0;
        float[] spectrum = _audio.GetSpectrumData(1024, 0, FFTWindow.Hamming);
        for (int i = 0; i < 1024; i++)
        {
            hue += (spectrum[i]*spectrum[i]);
            
        }

        hue = Mathf.Sqrt(hue / 1024);
        return hue;
    }


    void UpdateAudio()
    {

        _audio.clip = sourceAudio.clip;
        _audio.volume = sourceAudio.volume;
        _audio.pitch = sourceAudio.pitch;
        _audio.playOnAwake = sourceAudio.playOnAwake;
        _audio.loop = sourceAudio.loop;

        if (sourceAudio.isPlaying && !_audio.isPlaying)
            _audio.Play();
    }
}
