using UnityEngine;
using System.Collections;

public class PreScene : MonoBehaviour {

    public float waitTime = 5.0f;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        waitTime -= Time.deltaTime;
        if (waitTime < 0)
            Main.currentPhase = Main.PhaseID.ONE;
	}
}
