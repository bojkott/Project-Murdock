using UnityEngine;
using System.Collections;

public class ReceptionChecker : MonoBehaviour {

    public RadioControl radioControl;
    public Bink bellController;
    public float timer = 30;

	
	// Update is called once per frame
	void Update () {

        if(!bellController.firstTime)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                    Main.currentPhase = Main.PhaseID.THREE;
            }

	}
}
