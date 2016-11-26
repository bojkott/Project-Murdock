using UnityEngine;
using System.Collections;
using VRTK;

public class Bink : VRTK_InteractableObject
{
    public AudioSource binkSource;

    private const float maxCooldown = 0.5f;
    private float cooldown = 0.0f;
    private bool triggered = false;
    private bool firstTime = true;
    private float transitionTime = 5.0f;

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (this.IsTouched() && !triggered && !this.binkSource.isPlaying)
        {
            cooldown = maxCooldown;
            triggered = true;
            this.binkSource.Play();
            firstTime = false;
        }
        if (triggered)
        {
            float k = Mathf.Cos((maxCooldown - cooldown) / maxCooldown * Mathf.PI);

            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - .00305f * k, this.transform.position.z);

            cooldown -= Time.deltaTime;
            if (cooldown < 0.0f)
            {
                triggered = false;
            }
        }
        if (!firstTime)
            transitionTime -= Time.deltaTime;

        if (transitionTime < 0.0f)
            Main.currentPhase = Main.PhaseID.THREE;
    }
}
