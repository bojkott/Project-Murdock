using UnityEngine;
using System.Collections;

public class SoundSphere : MonoBehaviour {

    public float targetRadius;
    private float radius;
    public Color startColor;
    private Color color;
    private float time = 0;

    private Material mat;

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        this.radius = Mathf.Lerp(0, targetRadius, time);
        this.color = Color.Lerp(startColor, Color.black, time);

        mat.color = this.color;
        transform.localScale = new Vector3(radius, radius, radius);

        if (time > 1)
            Destroy(gameObject);
	}
}
