using UnityEngine;
using System.Collections;

public class NewWave {

    public Vector3 position;
    public float radius;
    public Color color;

    private float nextRadius;
    private float prevNextRadius;

    private Color nextColor;
    private Color prevNextColor;


    private float time = 0;

	public NewWave(Vector3 pos, Color color)
    {
        this.position = pos;
        this.radius = 0;
        this.nextRadius = 0;
        this.color = color;

    }

    public void SetNextRadius(float radius)
    {
        prevNextRadius = nextRadius;
        nextRadius = radius;
    }

    public void SetNextColor(Color color)
    {
        prevNextColor = nextColor;
        nextColor = color;
    }



    public void Update()
    {

        time = (time+Time.deltaTime)%0.5f;
        this.radius = SuperSmoothFloatLerp(this.radius, prevNextRadius, nextRadius, Time.deltaTime, 1);
        this.color = SuperSmoothColorLerp(this.color, prevNextColor, nextColor, Time.deltaTime, 1);
    }

    float SuperSmoothFloatLerp(float pastRadius, float pastTargetRadius, float targetRadius, float time, float speed)
    {
        float f = pastRadius - pastTargetRadius + (targetRadius - pastTargetRadius) / (speed * time);
        return targetRadius - (targetRadius - pastTargetRadius) / (speed * time) + f * Mathf.Exp(-speed * time);
    }

    Color SuperSmoothColorLerp(Color pastColor, Color pastTargetColor, Color targetColor, float time, float speed) {


        float r = SuperSmoothFloatLerp(pastColor.r, pastTargetColor.r, targetColor.r, time, speed);
        float g = SuperSmoothFloatLerp(pastColor.g, pastTargetColor.g, targetColor.g, time, speed);
        float b = SuperSmoothFloatLerp(pastColor.b, pastTargetColor.b, targetColor.b, time, speed);

        return new Color(r, g, b);
    }

}
