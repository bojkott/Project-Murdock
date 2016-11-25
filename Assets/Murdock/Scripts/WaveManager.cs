using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public Material waveMaterial;
    public GameObject waveSpherePrefab;

    void Start()
    {

    }

    public void CreateWave(Vector3 pos, float travelSpeed, Color col, float fadeSpeed, float trailModifier)
    {
        Wave wave = gameObject.AddComponent<Wave>();
        wave.SetPosition(pos);
        wave.setTravelSpeed(travelSpeed);
        wave.SetMaterial(waveMaterial);
        wave.SetColor(col);
        wave.SetFadeSpeed(fadeSpeed);
        wave.SetTrailModifier(trailModifier);
        wave.SetSphere(waveSpherePrefab);
       
    }


    
	
}
