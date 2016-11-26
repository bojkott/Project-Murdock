using UnityEngine;
using System.Collections;

public class SoundWaveEmitter : MonoBehaviour
{

    public float interval;
    public Transform origin;

    public float saturation = 0.5f;
    public float vibrance = 1.0f;
    public float amp = 1.0f;

    public Color colorMod = Color.white;

    public float sphereMaxSize = 0;

    public int samples = 1024;
    public float sensitivity = 100;
    public int Highfreqs = 128;
    public int LowFreqs = 0;
    public FFTWindow fftWindow;
    public float loudness = 0;
    private AudioSource _audio;
    private WaveManager wm;
    private float time;



    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        wm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaveManager>();
        
    }
    void Update()
    {
        time += Time.deltaTime;
        loudness = GetAveragedVolume() * sensitivity;
        if (loudness > 1 && time > interval)
        {
            float h = 0;
            float waveSpeed = 0;
            float[] spectrum = _audio.GetSpectrumData(samples, 0, fftWindow);

            for(int i=LowFreqs; i< Highfreqs; i++)
            {
                h += spectrum[i];
                waveSpeed += spectrum[i]*amp;
            }

            waveSpeed /= (Highfreqs-LowFreqs);

            

            Color color = Color.HSVToRGB(h, saturation, vibrance)+ colorMod;

            if(wm != null)
            {
              //  loudness = Mathf.Clamp(loudness, 1, 3);
                Vector3 spawnPos = transform.position;
                if(origin != null)
                {
                    spawnPos = origin.position;
                }
                wm.CreateWave(spawnPos, waveSpeed, color, loudness, sphereMaxSize);
            }

            time = 0;
        }
    }
    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

}