using UnityEngine;
using System.Collections;
using System.Collections.Generic;



struct WaveObjet
{
    public List<Wave> waves;
}


public class WaveManager : MonoBehaviour
{


    public Material waveMaterial;
    public Material waveMaterial2;
    public GameObject waveSpherePrefab;

    private List<Wave> waves = new List<Wave>();

    private Dictionary<GameObject, WaveObjet> waveDict = new Dictionary<GameObject, WaveObjet>();

    const int MAX_WAVES = 500;
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

    public void CreateWave(GameObject spawnedFrom, Vector3 pos, float maxRadius, Color col, float fadeSpeed, bool spawnSphere)
    {


        if(!waveDict.ContainsKey(spawnedFrom))
        {
            WaveObjet waveObj = new WaveObjet();
            waveObj.waves = new List<Wave>();
            waveDict.Add(spawnedFrom, waveObj);
        }

        List<Wave> waves = waveDict[spawnedFrom].waves;

        Wave wave = new Wave();
        wave.SetPosition(pos);
        wave.SetMaxRadius(maxRadius);
        wave.SetColor(col);
        wave.SetFadeSpeed(fadeSpeed);

        if(spawnSphere)
        {
            GameObject sphere = (GameObject)Instantiate(waveSpherePrefab, pos, Quaternion.identity);
            wave.SetSphere(sphere);
            
        }
        wave.SetThickness(2);
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
                if(waves[i].sphere)
                    Destroy(waves[i].sphere);
                waves.Remove(waves[i]);
            }
        }


        int offset = 0;
        List<GameObject> keys = new List<GameObject>(waveDict.Keys);
        foreach (GameObject key in keys)
        {
            List<Wave> waves = waveDict[key].waves;
            for (int i = 0; i < waves.Count; i++)
            {
                
                waves[i].Update(growthCurve, fadeCurve, thicknessCurve);

                wavesPos[offset] = waves[i].GetPosition();
                wavesColor[offset] = waves[i].GetColor();
                wavesRadius[offset] = waves[i].GetRadius();
                wavesThickness[offset] = waves[i].GetThickness();

                if (!waves[i].alive)
                {
                    if (waves[i].sphere)
                        Destroy(waves[i].sphere);
                    waves.Remove(waves[i]);
                }
                                
                offset++;
            }

            if (waves.Count == 0)
                waveDict.Remove(key);
        }



        UpdateMaterial(waveMaterial);
        UpdateMaterial(waveMaterial2);

    }


    void UpdateMaterial(Material mat)
    {
        mat.SetFloat("_WavesCount", waveDict.Count);
        if (waveDict.Count> 0)
        {
            float[] waveObjCount = GetWaveObjCounts();
            
            mat.SetFloatArray("_SpawnedIdWaveCount", waveObjCount);
            mat.SetFloatArray("_Radius", wavesRadius);
            mat.SetFloatArray("_Thickness", wavesThickness);
            mat.SetVectorArray("_Waves", wavesPos);
            mat.SetColorArray("_Color", wavesColor);
        }
    }



    float[] GetWaveObjCounts()
    {
        float[] waveObjCounts = new float[100];
        int i = 0;
        foreach (WaveObjet waveObj in waveDict.Values)
        {
            waveObjCounts[i] = waveObj.waves.Count;
            i++;

        }
        return waveObjCounts;
    }

}
