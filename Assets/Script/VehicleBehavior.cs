﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{

    public float speed = 3;
    float fwdSpeed = 30f;
    private Transform transform;
    private Rigidbody rb;
    public AudioSource audioDataAvance;
    public AudioSource audioDataTourne;

    void Start()
    {
            transform = GetComponent<Transform>();
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
       if (GameManager.Instance != null && (GameManager.Instance.gamemodePlay || GameManager.Instance.gamemodeGhostRace)){
            if (Input.GetKey("space")) {
                Vector3 orientation = transform.rotation.eulerAngles;
                orientation.x = -90f;
                transform.eulerAngles = orientation;
            } 

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                audioDataAvance.UnPause();
            } else
            {
                audioDataAvance.Pause();
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                audioDataTourne.UnPause();
            }
            else
            {
                audioDataTourne.Pause();
            }

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(0.0f, moveVertical, 0.0f);
            Vector3 rotation = new Vector3(0.0f, 0.0f, moveHorizontal);

            rb.AddForce(transform.up * fwdSpeed * moveVertical,ForceMode.Acceleration);
            //transform.Translate(movement * speed * Time.deltaTime);
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
