using UnityEngine;
using System.Collections;

public class ShoeAnimation : MonoBehaviour {
    private float horizontalStepDistance;
    private float speed;
    private float stepHeight;
    private float angle;
    public AudioSource stepSound;
    public GameObject otherShoe;
    private bool nextStep;

    private Vector3 initPos;
    private Vector3 otherInitPos;
	// Use this for initialization
	void Start () {
        this.horizontalStepDistance = 0.1f;
        this.stepHeight = 0.3f;
        this.angle = Mathf.PI / 2;
        this.speed = 0.5f;
        float initShoeDistance = 0.03f;
        this.initPos = this.transform.position;
        this.otherInitPos = this.otherShoe.transform.position = new Vector3(transform.position.x + initShoeDistance, transform.position.y, transform.position.z);
        this.nextStep = true;
	}
	
	// Update is called once per frame
	void Update () {
        this.angle += Mathf.PI * Time.deltaTime * speed;

        if (Mathf.Clamp(Mathf.Sin(angle) * stepHeight, 0, stepHeight) > 0)
        {
            this.transform.position = new Vector3(this.transform.position.x + -horizontalStepDistance * Time.deltaTime, Mathf.Clamp(Mathf.Sin(angle) * stepHeight, 0, stepHeight) + initPos.y, 0);
            if (!stepSound.isPlaying && this.nextStep)
            {
                this.nextStep = false;
                stepSound.Play();
            }
        }
        else
        {
            otherShoe.transform.position = new Vector3(otherShoe.transform.position.x + -horizontalStepDistance * Time.deltaTime, Mathf.Clamp(Mathf.Sin(-angle) * stepHeight, 0, stepHeight) + otherInitPos.y, 0);
            if (!stepSound.isPlaying && !this.nextStep)
            {
                this.nextStep = true;
                stepSound.Play();
            }
        }
    }
}
