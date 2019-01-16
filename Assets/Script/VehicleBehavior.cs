using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{

    public float speed = 3;
    private Transform transform;

    void Start()
    {
            transform = GetComponent<Transform>();
    }

    void Update()
    {
       if (GameManager.Instance != null && (GameManager.Instance.gamemodePlay || GameManager.Instance.gamemodeGhostRace)){
            if (Input.GetKey("space")) {
                Vector3 orientation = transform.rotation.eulerAngles;
                orientation.x = -90f;
                transform.eulerAngles = orientation;
            }

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(0.0f, moveVertical, 0.0f);
            Vector3 rotation = new Vector3(0.0f, 0.0f, moveHorizontal);

            transform.Translate(movement * speed * Time.deltaTime);
            transform.Rotate(rotation * speed * 30 * Time.deltaTime);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Check")){
            Recorder.Instance.Check(other);
        }
    }
}
