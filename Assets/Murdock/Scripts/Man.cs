using UnityEngine;
using System.Collections;

public class Man : MonoBehaviour {

	// Use this for initialization
	void Start () {

        time = Random.Range(0f, 6f);

	}

    float time;
	// Update is called once per frame
	void Update () {

        transform.position += Mathf.Sin(time) * Time.deltaTime * Vector3.up * 0.6f;

        time += Time.deltaTime * 5;

	}
}
