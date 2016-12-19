using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundWaveEmitter : MonoBehaviour
{

    [Range(0.0f, 1.0f)]
    public float SphereSpawnThreshold;

    [Range(0.0f, 1.0f)]
    public float SphereSpawnInterval;

    public List<Transform> origins;
    [Range(0.0f, 1.0f)]
    public float saturation = 0.5f;
    [Range(0.0f, 1.0f)]
    public float vibrance = 1.0f;
    [Range(1.0f, 200.0f)]
    public float amp = 1.0f;

    public Color colorMod = Color.white;

    [Range(1.0f, 200.0f)]
    public float sensitivity = 100;
    [Range(1.0f, 10.0f)]
    public float maxRadius = 10;
    private AudioSource _audio;
    private AudioSource sourceAudio;
    private WaveManager wm;
    private float Spheretime;
    private float time;

    private float prevLoudness = 0;

    void Awake()
    {

        sourceAudio = GetComponent<AudioSource>();
        if (!sourceAudio)
        {
            Debug.LogWarning("Object: " + gameObject.name + " Is missing audioSource");
            sourceAudio = gameObject.AddComponent<AudioSource>();
        }
            
        //_audio = gameObject.AddComponent<AudioSource>();
        //UpdateAudio();
        //sourceAudio.spatialBlend = 1;
        //_audio.spatialBlend = 0;
        //_audio.outputAudioMixerGroup = sourceAudio.outputAudioMixerGroup;
        sourceAudio.outputAudioMixerGroup = null;

        //if (_audio.playOnAwake)
        //    _audio.Play();

        _audio = sourceAudio;

    }
    void Start()
    {
        wm = WaveManager.instance;
        if (origins.Count == 0)
            origins.Add(transform);


    }
    void Update()
    {
        //UpdateAudio();



        Spheretime += Time.deltaTime;
        time += Time.deltaTime;

        float loudness = GetAveragedVolume(0)+GetAveragedVolume(1);
        loudness *= sensitivity;


        if (loudness > 0 && time > 0.05f)
        {

            float hue = GetSpectrum(0)+GetSpectrum(1);


            float AmpedHue = hue*amp;


            Color color = Color.HSVToRGB(AmpedHue, saturation, vibrance)  + colorMod;


            float radius = loudness;

            radius = Mathf.Clamp(radius, 0, maxRadius);

            color *= (radius / maxRadius);


            foreach(Transform spawnPoint in origins)
            {
                if ((loudness-prevLoudness) > SphereSpawnThreshold && Spheretime > SphereSpawnInterval)
                {
                    wm.CreateWave(gameObject, spawnPoint.position, radius, color, radius, true);
                    Spheretime = 0;
                }
                else
                    wm.CreateWave(gameObject, spawnPoint.position, radius, color, radius, false);
            }


            time = 0;

            
        }
        prevLoudness = loudness;
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
        float[] spectrum = _audio.GetSpectrumData(1024, channel, FFTWindow.Hamming);
        for (int i = 0; i < 1024; i++)
        {
            hue += (spectrum[i] * spectrum[i]);

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