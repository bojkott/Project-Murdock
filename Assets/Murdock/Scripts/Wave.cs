using UnityEngine;
using System.Collections;

public class Wave {

    private Vector3 position;
    private float radius;
    private float maxRadius;
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
    private float thickness;
    private float maxThickness;

    public bool alive = true;

    private Color blackColor = new Color(0, 0, 0, 0);

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

    public void SetMaxRadius(float maxRadius)
    {
        this.maxRadius = maxRadius;
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


    public float GetRadius()
    {
        return radius;
    }

    public void SetSphere(GameObject sphere)
    {
        this.sphere = sphere;
        startSphereColor = sphere.GetComponent<Renderer>().material.color;

    }

    public void SetThickness(float thickness)
    {
        this.maxThickness = thickness;
    }

    public float GetThickness()
    {
        return thickness;
    }


    public void Update(AnimationCurve growthCurve, AnimationCurve fadeCurve, AnimationCurve thicknessCurve)
    {
        life += Time.deltaTime / fadeSpeed;

        radius = maxRadius * growthCurve.Evaluate(life);

       

    

        thickness = thicknessCurve.Evaluate(life) * maxThickness;

        waveColor = Color.Lerp(startWaveColor, blackColor, fadeCurve.Evaluate(life));

        if(sphere)
        {

            sphere.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);


            sphere.GetComponent<Renderer>().material.color = startSphereColor * waveColor;
        }
            

        if (life > 1.1f) // Yep this is a bit weird :P
        {
            alive = false;
           
        }

    }    
}


