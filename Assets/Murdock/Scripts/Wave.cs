using UnityEngine;
using System.Collections;

public class Wave {

    private Vector3 position;
    private float radius;
    private float travelSpeed;
    private Material mat;

    private float trailWidth;

    public Wave(Vector3 startPos, float travelSpeed, Material mat)
    {
        this.position = startPos;
        this.travelSpeed = travelSpeed;
        this.radius = 0;
        this.mat = mat;
    }

    public void Update()
    {
        this.radius += travelSpeed * Time.deltaTime;
        this.trailWidth = radius * 0.8f;
    }

    public Material GetMaterial()
    {
        return mat;
    }


    public void UpdateMaterial()
    {
        if(mat)
        {
            mat.SetVector("_WorldSpaceScannerPos", position);
            mat.SetFloat("_ScanDistance", radius);
        }
        
    }
}
