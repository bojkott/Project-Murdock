using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

    private float delay = 8;
    private float timeToOpen = 6.55f;
    private float timer = 0.0f;
    private float maxRotation = 90.0f;
    private bool firstTime = true;

    public AudioSource openDoorSound;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        delay -= Time.deltaTime;
        
        if (delay < 0 && timer < timeToOpen)
        {
            if (firstTime)
            {
                openDoorSound.Play();
                firstTime = false;
            }

            timer += Time.deltaTime;

            float rotation = timer / timeToOpen * maxRotation;

            this.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
	}
}
