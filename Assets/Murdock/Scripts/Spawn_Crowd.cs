using UnityEngine;
using System.Collections;

public class Spawn_Crowd : MonoBehaviour {

    public Rect space;
    public int crowd_size;

	// Use this for initialization
	void Start () {

        for(int i = 0; i < crowd_size; i++)
        {

            GameObject man = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Man"), transform.position + new Vector3(Random.Range(-space.width, +space.width), Random.Range(-1, 1), Random.Range(-space.height, +space.height)), Quaternion.identity);
            man.transform.parent = transform;

        }

	}

    float time = 0;
	// Update is called once per frame
	void Update () {
	

	}
}
