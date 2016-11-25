using UnityEngine;
using System.Collections;

public class TestWave : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaveManager>().CreateWave(transform.position, 1, Random.ColorHSV(), 1, 0.8f);
    }
}
