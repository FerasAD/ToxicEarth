using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startposition;
    public GameObject Camera;
    public float parallaxingEffect;
    void Start()
    {
        startposition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distance = (Camera.transform.position.x * parallaxingEffect);

        transform.position = new Vector3(startposition + distance, transform.position.y, transform.position.z);

    }
}


