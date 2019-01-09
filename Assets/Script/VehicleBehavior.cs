using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{

    public float speed;
    private Transform transform;
    void Start()
    {
         transform = GetComponent<Transform>();
    }

     void Update ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (0.0f,moveVertical, 0.0f);
        Vector3 rotation = new Vector3 (0.0f,0.0f,moveHorizontal);

        transform.Translate(movement * speed * Time.deltaTime);
        transform.Rotate(rotation);
        
    }
}
