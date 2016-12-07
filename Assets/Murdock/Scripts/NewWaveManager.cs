using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NewWaveManager : MonoBehaviour {

    public Material waveMaterial;
    public Material waveMaterial2;
    public GameObject waveSpherePrefab;
    private List<NewWave> waves = new List<NewWave>(); // One wave per sound

    const int MAX_WAVES = 200;
    Vector4[] wavesPos = new Vector4[MAX_WAVES];
    Color[] wavesColor = new Color[MAX_WAVES];
    float[] wavesRadius = new float[MAX_WAVES];
    float[] wavesThickness = new float[MAX_WAVES];

    // Use this for initialization
    void Start () {
	
	}

    public NewWave CreateWave(Vector3 pos, Color col)
    {
        NewWave wave = new NewWave(pos, col);
        waves.Add(wave);

        return wave;
    }

    public void CreateSphere(Vector3 pos, Color col, float radius)
    {
        GameObject sphere = (GameObject)Instantiate(waveSpherePrefab, pos, waveSpherePrefab.transform.rotation);
        sphere.GetComponent<SoundSphere>().targetRadius = radius;
        sphere.GetComponent<SoundSphere>().startColor = col;
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < waves.Count; i++)
        {
            waves[i].Update();
            wavesPos[i] = waves[i].position;
            wavesColor[i] = waves[i].color;

            wavesRadius[i] = waves[i].radius;
            wavesThickness[i] = waves[i].radius;
        }

        UpdateMaterial(waveMaterial);
        UpdateMaterial(waveMaterial2);
    }

    void UpdateMaterial(Material mat)
    {
        mat.SetFloat("_WavesCount", waves.Count);
        if (waves.Count > 0)
        {
            mat.SetFloatArray("_Radius", wavesRadius);
            mat.SetFloatArray("_Thickness", wavesThickness);
            mat.SetVectorArray("_Waves", wavesPos);
            mat.SetColorArray("_Color", wavesColor);
        }
    }
}
