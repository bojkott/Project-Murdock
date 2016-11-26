using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour {

    private float timer = 0.0f;
    private const float startUpTime = 2.31f;

    private const float fanMaxRotationSpeed = 2.0f * Mathf.PI * 2.5f;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {

        timer += Time.deltaTime;

        float rotationSpeed = fanMaxRotationSpeed;

        if (timer < startUpTime)
        {
            rotationSpeed = timer / startUpTime * fanMaxRotationSpeed;
        }

        this.transform.RotateAround(this.transform.up, rotationSpeed * Time.deltaTime);

    }
}
