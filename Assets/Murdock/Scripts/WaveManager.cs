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
    float[] wavesThickness = new float[MAX_WAVES];


    public AnimationCurve growthCurve;
    public AnimationCurve fadeCurve;
    public AnimationCurve thicknessCurve;


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
        wave.SetThickness(1);


        waves.Add(wave);

    }

    void Update()
    {
        

        for(int i=0;i<waves.Count;i++)
        {
            waves[i].Update(growthCurve, fadeCurve, thicknessCurve);             
            wavesPos[i] = waves[i].GetPosition();
            wavesColor[i] = waves[i].GetColor();
            wavesRadius[i] = waves[i].GetRadius();
            wavesThickness[i] = waves[i].GetThickness();
            if (!waves[i].alive)
            {
                Destroy(waves[i].sphere);
                waves.Remove(waves[i]);
            }
        }


        waveMaterial.SetFloat("_WavesCount", waves.Count);
        if(waves.Count > 0)
        {
            waveMaterial.SetFloatArray("_Radius", wavesRadius);
            waveMaterial.SetFloatArray("_Thickness", wavesThickness);
            waveMaterial.SetVectorArray("_Waves", wavesPos);
            waveMaterial.SetColorArray("_Color", wavesColor);
        }

    }
	
}
