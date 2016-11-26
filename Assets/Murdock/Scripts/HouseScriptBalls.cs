using UnityEngine;
using System.Collections;

public class HouseScriptBalls : MonoBehaviour {
    public AudioSource doorAudioSrouce;

    private float timer;
    private float openSpeed;
    private Vector3 temp;

    

	// Use this for initialization
	void Start () {
        this.timer = 0.7f;
        this.temp = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);

    }
	
	// Update is called once per frame
	void Update () {
        
         
        
        

	}
}
