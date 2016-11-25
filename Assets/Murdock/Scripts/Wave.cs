using UnityEngine;
using System.Collections;

public class Wave {

    private Vector3 position;
    private float radius;
    private float travelSpeed;
    private Material mat;

    private float trailWidth;

    private Color startWaveColor = Color.white;
    private Color waveColor;


    private float life;
    private float fadeSpeed;
    private Camera _camera;
    private float trailModifer;

    public GameObject sphere;
    private Color startSphereColor;

    public bool alive = true;

    void Start()
    {
        this.radius = 0;

    }

    public void SetPosition(Vector3 pos)
    {
        position = pos;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public void setTravelSpeed(float speed)
    {
        travelSpeed = speed;
    }

    public void SetColor(Color col)
    {
        startWaveColor *= col;
        waveColor = startWaveColor;
    }

    public Color GetColor()
    {
        return waveColor;
    }

    public void SetFadeSpeed(float fadeSpeed)
    {
        this.fadeSpeed = fadeSpeed;
    }

    public void SetTrailModifier(float modifier)
    {
        trailModifer = modifier;
    }


    public float GetRadius()
    {
        return radius;
    }

    public void SetSphere(GameObject sphere)
    {
        this.sphere = sphere;
        startSphereColor = sphere.GetComponent<Renderer>().material.color;

    }

    public void Update()
    {
        life += Time.deltaTime / fadeSpeed;

        radius += travelSpeed * Time.deltaTime;
        trailWidth = radius * 0.8f;

           sphere.transform.localScale = new Vector3(radius*2, radius*2, radius*2);


        waveColor = Color.Lerp(waveColor, Color.black, life);

        sphere.GetComponent<Renderer>().material.color = startSphereColor*waveColor;

        if (life > 1) // Yep this is a bit weird :P
        {
            alive = false;
           
        }

    }    
}


