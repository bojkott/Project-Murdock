using UnityEngine;
using System.Collections;

public class ShoeAnimation : MonoBehaviour {
    private float horizontalStepDistance;
    private float speed;
    private float stepHeight;
    private float angle;
    private AudioSource stepSound;
    private GameObject otherShoe;
    private bool nextStep;

    private Vector3 initPos;
    private Vector3 otherInitPos;

	// Use this for initialization
	void Start () {
        this.horizontalStepDistance = 0.1f;
        this.stepHeight = 0.3f;
        this.angle = Mathf.PI / 2;
        this.speed = 0.5f;

        this.otherShoe = (GameObject)Instantiate(Resources.Load("prefabs/ShoeRight"));
        this.initPos = this.transform.position;
        this.otherInitPos = this.otherShoe.transform.position;
        this.nextStep = true;
	}
	
	// Update is called once per frame
	void Update () {
        this.angle += Mathf.PI * Time.deltaTime * speed;

        if (Mathf.Clamp(Mathf.Sin(angle) * stepHeight, 0, stepHeight) > 0)
        {
            this.transform.position = new Vector3(0, Mathf.Clamp(Mathf.Sin(angle) * stepHeight, 0, stepHeight) + initPos.y, this.transform.position.z - horizontalStepDistance * Time.deltaTime);
            if (!stepSound.isPlaying && this.nextStep)
            {
                this.nextStep = false;
                stepSound.Play();
            }
        }
        else
        {
            otherShoe.transform.position = new Vector3(0, Mathf.Clamp(Mathf.Sin(-angle) * stepHeight, 0, stepHeight) + otherInitPos.y, otherShoe.transform.position.z - horizontalStepDistance * Time.deltaTime);
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
