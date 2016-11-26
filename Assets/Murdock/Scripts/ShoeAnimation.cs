using UnityEngine;
using System.Collections;

public class ShoeAnimation : MonoBehaviour {
    private float horizontalStepDistance;
    private float speed;
    private float stepHeight;
    private float angle;

    public AudioSource stepSound;
    public AudioSource stepSound2;
    public GameObject otherShoe;

    private bool nextStep;

    private Vector3 initPos;
    private Vector3 otherInitPos;

    private float time;

	// Use this for initialization
	void Start () {
        this.horizontalStepDistance = 0.5f;
        this.stepHeight = 0.6f;
        this.angle = Mathf.PI / 2;
        this.speed = 0.5f;

        this.initPos = this.transform.position;
        this.otherInitPos = this.otherShoe.transform.position;
        this.nextStep = true;
	}
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;
        if(time > 17)
        {
            Main.currentPhase = Main.PhaseID.ONE;
        }

        this.angle += Mathf.PI * Time.deltaTime * speed;

        if (Mathf.Clamp(Mathf.Sin(angle) * stepHeight, 0, stepHeight) > 0)
        {
            this.transform.position = new Vector3(this.initPos.x, Mathf.Clamp(Mathf.Sin(angle) * stepHeight, 0, stepHeight) + initPos.y, this.transform.position.z - horizontalStepDistance * Time.deltaTime);
            if (!stepSound.isPlaying && this.nextStep)
            {
                this.nextStep = false;
                stepSound2.Play();
            }
        }
        else
        {
            otherShoe.transform.position = new Vector3(otherInitPos.x, Mathf.Clamp(Mathf.Sin(-angle) * stepHeight, 0, stepHeight) + otherInitPos.y, otherShoe.transform.position.z - horizontalStepDistance * Time.deltaTime);
            if (!stepSound.isPlaying && !this.nextStep)
            {
                this.nextStep = true;
                stepSound.Play();
            }
        }
    }

    void OnDestroy()
    {
        Destroy(this.otherShoe);
    }

}
