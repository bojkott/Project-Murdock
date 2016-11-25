using UnityEngine;
using System.Collections;

public class PhoneRingingScript : MonoBehaviour
{

    private float angle;
    private int speed;
    private float flipFactor;
    private float ringTimer;
    private float pauseTimer;
    private bool phase1;
    // Use this for initialization
    void Start()
    {

        this.angle = Mathf.PI / 2; // initial angle set to half way through the animation

        /*Pendle factors*/
        this.speed = 25;
        this.flipFactor = 10;

        this.ringTimer = 0.6f;
        
        this.phase1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.angle += Mathf.PI * Time.deltaTime * speed;

        this.ringTimer -= Time.deltaTime;
        this.pauseTimer -= Time.deltaTime;

        if (ringTimer > 0)
        {
            this.transform.RotateAround(this.transform.position, this.transform.forward, Mathf.Cos(angle) * this.flipFactor);
            this.pauseTimer = 1.729f;
           
        }
        else if (pauseTimer < 0)
        {
            this.ringTimer = 0.6f;
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, 0);
        }

    }
}
