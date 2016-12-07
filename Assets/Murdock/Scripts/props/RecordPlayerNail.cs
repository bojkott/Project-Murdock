using UnityEngine;
using System.Collections;

public class RecordPlayerNail : MonoBehaviour {


    public bool touchingPlatter;

    void Start()
    {
        touchingPlatter = false;
    }

	void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.transform.name == "Disc platter")
        {
            touchingPlatter = true;
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.transform.name == "Disc platter")
        {
            touchingPlatter = false;
        }
    }
}
