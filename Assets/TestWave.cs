using UnityEngine;
using System.Collections;

public class TestWave : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaveManager>().CreateWave(transform.position, 10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
