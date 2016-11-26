using UnityEngine;
using System.Collections;
using VRTK;

public class PhoneRingingScript : VRTK_InteractableObject
{
    private float angle;
    private int speed;
    private float flipFactor;
    private float ringTimer;
    private float pauseTimer;
    private Vector3 startPos;

    public GameObject phoneBody;

    private bool colideOnce = true;
    public AudioSource collisionSound;

    private float syncTimer;
    private bool hasAnswered;

    private float transtionTimer = 5.0f;
    // Use this for initialization
    void Start()
    {

        this.angle = Mathf.PI; // initial angle set to half way through the animation

        /*Pendle factors*/
        this.speed = 25;
        this.flipFactor = 10;

        this.ringTimer = 0.6f;
        pauseTimer = 1.729f;

        this.syncTimer = 0.3f;

        this.hasAnswered = false;

        phoneBody.GetComponent<Collider>().enabled = false;
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.grabbedSnapHandle)
        {
            
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            AudioSource a = this.gameObject.GetComponent<AudioSource>();
            if (!a.isPlaying && !hasAnswered) 
                a.Play();
            phoneBody.GetComponent<AudioSource>().Stop();
            phoneBody.GetComponent<Collider>().enabled = true;
            hasAnswered = true;

            transtionTimer -= Time.deltaTime;
            if (transtionTimer < 0.0f && Main.currentPhase != Main.PhaseID.TWO)
                Main.currentPhase = Main.PhaseID.TWO; 
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
                    this.transform.RotateAround(this.transform.position, this.transform.forward, Mathf.Cos(angle) * this.flipFactor);
                    this.pauseTimer = 1.729f;

                }
                else if (this.pauseTimer < 0)
                {
                    transform.position = startPos;
                    this.ringTimer = 0.6f;
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, 0);
                }
            }
        }
    }

    void OnCollisionEnter(Collision x)
    {
        if (hasAnswered && colideOnce)
        {
            this.collisionSound.Play();
            colideOnce = false;
        }
    }
}
