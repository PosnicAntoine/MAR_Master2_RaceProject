using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRotorBehavior : MonoBehaviour
{

    public float speed;
    public bool isSpinning;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSpinning)
        {
            Transform t = GetComponent<Transform>();
            t.Rotate(speed * Time.deltaTime,0,0);
        }

    }


}