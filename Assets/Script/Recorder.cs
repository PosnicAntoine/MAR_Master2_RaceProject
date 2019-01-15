﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public static Recorder Instance;

    private Race race;
    public GameObject player;
    public Collider[] checkpoints;
    private int next;
    private bool isRecording;
    private float startTimeRecording;

    void Awake()
    {
       
        if (Instance == null)
            Instance = this;
       
        else if (Instance != this)
            Destroy(this);

        Init();
    }

    void Update(){
        if (player != null){
            if (isRecording){
                Vector4 position = new Vector4();
                position.x = startTimeRecording - Time.time;
                position.y = player.transform.position.x;
                position.z = player.transform.position.y;
                position.w = player.transform.position.z;
                race.Trajectory.Add(position);
            }
        }
    }

    void Init(){
        isRecording = false;
        race = new Race();
        race.name = "TestRecord";
        next = 0;
    }

    public void StartRecording(){
        isRecording = true;
        startTimeRecording = Time.time;
    }

    void EndRecording(){
        Debug.Log("End Recording");
        isRecording = false;
        GameManager.Instance.SaveData(race);
        Init();
    }

    public void Check(Collider collider){
        if (collider == checkpoints[next]){
            if(next == checkpoints.Length-1){
                EndRecording();
                return;
            }
            next++;
        }
    }

}
