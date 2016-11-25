using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public Material waveMaterial;
    public GameObject waveSpherePrefab;

    private List<Wave2> waves = new List<Wave2>();

    void Start()
    {

    }

    public void CreateWave(Vector3 pos, float travelSpeed, Color col, float fadeSpeed, float trailModifier)
    {
        //Wave wave = gameObject.AddComponent<Wave>();
        //wave.SetPosition(pos);
        //wave.setTravelSpeed(travelSpeed);
        //wave.SetMaterial(waveMaterial);
        //wave.SetColor(col);
        //wave.SetFadeSpeed(fadeSpeed);
        //wave.SetTrailModifier(trailModifier);
        //wave.SetSphere(waveSpherePrefab);

        waves.Add()
       
    }


    class Wave2
    {
        private Vector3 pos;
        private float travelSpeed;
        private float radius;
        private Color col;

        public Wave2(Vector3 pos, float travelSpeed, Color c)
        {
            this.pos = pos;
            this.travelSpeed = travelSpeed;
            this.col = c;
        }

        public void Update()
        {
            radius = travelSpeed * Time.deltaTime;
            GameObject[] go = (GameObject[])GameObject.FindObjectsOfType<GameObject>();

            foreach(GameObject g in go)
            {
                Renderer r = g.GetComponent<Renderer>(); 

                if(r != null)
                {
                    Debug.Log(r.name);
                }
            }
        }
    }


    
	
}
