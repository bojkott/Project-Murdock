using UnityEngine;
using System.Collections;
using VRTK;

public class PhoneRingingScript : MonoBehaviour
{
    private float angle;
    private int speed;
    private float flipFactor;
    private float ringTimer;
    private float pauseTimer;
    private Vector3 startPos;

    public GameObject phoneBody;
    public Transform phoneModel;

    private bool colideOnce = true;
    public AudioSource collisionSound;

    private float syncTimer;
    private bool hasAnswered;

    private float transtionTimer = 5.0f;

    private VRTK_InteractableObject interObj;

    // Use this for initialization

    void Start() {
        this.interObj = GetComponent<VRTK_InteractableObject>();
        this.angle = Mathf.PI; // initial angle set to half way through the animation

        /*Pendle factors*/
        this.speed = 25;
        this.flipFactor = 10;

        this.ringTimer = 0.6f;
        pauseTimer = 1.729f;

        this.syncTimer = 0.3f;

        

        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (interObj.IsGrabbed())
        {
            
            AudioSource a = this.gameObject.GetComponent<AudioSource>();
            if (!a.isPlaying && !hasAnswered) 
                a.Play();
            phoneBody.GetComponent<AudioSource>().Stop();
            phoneBody.GetComponent<Collider>().enabled = true;
            hasAnswered = true;
            } 
        else if (!hasAnswered)
        {
            
            syncTimer -= Time.deltaTime;
            if (syncTimer < 0)
            {
                this.angle += Mathf.PI * Time.deltaTime * speed;

                this.ringTimer -= Time.deltaTime;
                this.pauseTimer -= Time.deltaTime;

                if (this.ringTimer > 0)
                {
                    phoneModel.RotateAround(phoneModel.position, phoneModel.forward, Mathf.Cos(angle) * this.flipFactor);
                    this.pauseTimer = 1.729f;

                }
                else if (this.pauseTimer < 0)
                {
                    phoneModel.position = startPos;
                    this.ringTimer = 0.6f;
                }
                else
                {
                    phoneModel.rotation = Quaternion.Euler(phoneModel.rotation.eulerAngles.x, phoneModel.rotation.eulerAngles.y, 0);
                }
            }
        }
        else
        {
            transtionTimer -= Time.deltaTime;
            if (transtionTimer < 0)
                Main.currentPhase = Main.PhaseID.TWO;
        }
    }

    void OnCollisionEnter(Collision x)
    {
        if(!interObj.IsGrabbed() && x.impulse.magnitude > 5)
            this.collisionSound.Play();


    }
}
