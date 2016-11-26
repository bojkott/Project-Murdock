using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

    private float timeToOpen = 6.55f;
    private float timer = 0.0f;
    private float maxRotation = 90.0f;

    public AudioSource openDoorSound;

	// Use this for initialization
	void Start () {
        openDoorSound.Play();
    }
	
	// Update is called once per frame
	void Update () {

        if (timer < timeToOpen)
        {
            timer += Time.deltaTime;

            float rotation = timer / timeToOpen * maxRotation;

            this.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
	}
}
