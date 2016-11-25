using UnityEngine;
using System.Collections;

public class TestSphereCollide : MonoBehaviour {

    public Color color;
    private WaveManager waveManager;
    public float fadeSpeed = 1.0f;

    // Use this for initialization
    void Start () {
        waveManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision col)
    {
        waveManager.CreateWave(col.contacts[0].point, col.relativeVelocity.magnitude, color, fadeSpeed, 0.8f);
    }
}
