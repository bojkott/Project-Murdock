using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public Material waveMaterial;
    public GameObject waveSpherePrefab;

    private List<Wave> waves = new List<Wave>();

    const int MAX_WAVES = 20;
    Vector4[] wavesPos = new Vector4[MAX_WAVES];
    Color[] wavesColor = new Color[MAX_WAVES];
    float[] wavesRadius = new float[MAX_WAVES];

    void Start()
    {
    }

    public void CreateWave(Vector3 pos, float travelSpeed, Color col, float fadeSpeed, float trailModifier)
    {
        Wave wave = new Wave();
        wave.SetPosition(pos);
        wave.setTravelSpeed(travelSpeed);
        wave.SetColor(col);
        wave.SetFadeSpeed(fadeSpeed);
        wave.SetTrailModifier(trailModifier);
        GameObject sphere = (GameObject)Instantiate(waveSpherePrefab, pos, Quaternion.identity);
        wave.SetSphere(sphere);


        waves.Add(wave);

    }

    void Update()
    {
        

        for(int i=0;i<waves.Count;i++)
        {
            waves[i].Update();             
            wavesPos[i] = waves[i].GetPosition();
            wavesColor[i] = waves[i].GetColor();
            wavesRadius[i] = waves[i].GetRadius();
            if (!waves[i].alive)
            {
                Destroy(waves[i].sphere);
                waves.Remove(waves[i]);
            }
        }

        Debug.Log(waves.Count);

        waveMaterial.SetFloat("_WavesCount", waves.Count);
        if(waves.Count > 0)
        {
            waveMaterial.SetFloatArray("_Radius", wavesRadius);
            waveMaterial.SetVectorArray("_Waves", wavesPos);
            waveMaterial.SetColorArray("_Color", wavesColor);
        }

    }
	
}
