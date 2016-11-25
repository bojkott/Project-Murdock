using UnityEngine;
using System.Collections;

public class TestWave : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaveManager>().CreateWave(transform.position, 5, Random.ColorHSV(), 1, 0.8f);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
