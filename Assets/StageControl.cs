﻿using UnityEngine;
using System.Collections;

public class StageControl : MonoBehaviour {

    // Use this for initialization
    public float time = 30;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;

        if (time < 0)
            Main.currentPhase = Main.PhaseID.ONE;
	}
}
