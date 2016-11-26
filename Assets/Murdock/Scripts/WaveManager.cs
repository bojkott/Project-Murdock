using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public Material waveMaterial;
    public GameObject waveSpherePrefab;

    private List<Wave> waves = new List<Wave>();

    const int MAX_WAVES = 200;
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

    public void CreateWave(Vector3 pos, float travelSpeed, Color col, float fadeSpeed, float sphereMaxScale)
    {

 
        Wave wave = new Wave();
        wave.SetPosition(pos);
        wave.setTravelSpeed(travelSpeed);
        wave.SetColor(col);
        wave.SetFadeSpeed(fadeSpeed);
        GameObject sphere = (GameObject)Instantiate(waveSpherePrefab, pos, Quaternion.identity);
        wave.SetSphere(sphere);
        wave.SetSphereMaxScale(sphereMaxScale);
        wave.SetThickness(1);


        waves.Add(wave);

    }

    void Update()
    {

        Color average_color = new Color();

        for(int i=0;i<waves.Count;i++)
        {
            waves[i].Update(growthCurve, fadeCurve, thicknessCurve);             
            wavesPos[i] = waves[i].GetPosition();
            wavesColor[i] = waves[i].GetColor();

            average_color += waves[i].GetColor() / waves.Count;

            wavesRadius[i] = waves[i].GetRadius();
            wavesThickness[i] = waves[i].GetThickness();
            if (!waves[i].alive)
            {
                Destroy(waves[i].sphere);
                waves.Remove(waves[i]);
            }
        }

        Emit_Particles(average_color * 3);

        waveMaterial.SetFloat("_WavesCount", waves.Count);
        if(waves.Count > 0)
        {
            waveMaterial.SetFloatArray("_Radius", wavesRadius);
            waveMaterial.SetFloatArray("_Thickness", wavesThickness);
            waveMaterial.SetVectorArray("_Waves", wavesPos);
            waveMaterial.SetColorArray("_Color", wavesColor);
        }

    }

    void Emit_Particles(Color color) {

        Camera cam = GetComponent<Camera>();
        ParticleSystem ps = GameObject.Find("Particle System").GetComponent<ParticleSystem>();

        ParticleSystem.EmitParams e_params = new ParticleSystem.EmitParams();
        e_params.startLifetime = 0.8f;
        e_params.startSize = 0.01f;

        float min = 0.25f;
        float max = 0.50f;

        for (int x = (int)Random.Range(0, (cam.pixelWidth * max)); x < cam.pixelWidth; x += (int)Random.Range((cam.pixelWidth * min), (cam.pixelWidth * max)))
        {
            for (int y = (int)Random.Range(0, (cam.pixelHeight * max)); y < cam.pixelHeight; y += (int)Random.Range((cam.pixelHeight * min), (cam.pixelHeight * max)))
            {

                RaycastHit hit;

                if (Physics.Raycast(cam.ScreenPointToRay(new Vector3(x, y, 0)), out hit))
                {

                    for (int i = 0; i < waves.Count; i++)
                    {
                        float distance = Vector3.Distance(waves[i].GetPosition(), hit.point) - waves[i].GetRadius();
                        if (Mathf.Abs(distance) > 1f && distance < 0f)
                        {

                            e_params.position = hit.point;
                            e_params.velocity = hit.normal * e_params.velocity.magnitude;
                            e_params.startColor = wavesColor[i];
                            ps.gameObject.GetComponent<ParticleSystemRenderer>().material.color = color;
                            ps.Emit(e_params, 1);

                            break;

                        }

                    }

                }

            }

        }

    }
	
}
