using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public static Recorder Instance;

    private Race race;
    public GameObject player;
    //public Collider[] checkpoints;
    public Collider checkpoint1;
    public Collider checkpoint2;
    public Collider checkpoint2bis;
    public Collider checkpoint3;
    public Collider checkpoint4;
    public Collider checkpointArr;
    public int tour_number=3;
    private Collider next;
    private bool isRecording;
    private float startTimeRecording;

    void Awake()
    {
       
        if (Instance == null)
            Instance = this;
       
        else if (Instance != this)
            Destroy(this);

    }

    void Update(){
        if (player != null){
            if (isRecording){
                Vector4 position = new Vector4();
                position.x = Time.time - startTimeRecording;
                position.y = player.transform.position.x;
                position.z = player.transform.position.y;
                position.w = player.transform.position.z;
                race.Trajectory.Add(position);
            }
        }
    }

    public void Init(){
        isRecording = false;
        race = new Race();
        race.name = "TestRecord2";
        next = checkpoint1;
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

        if (collider == checkpoint1 && next == checkpoint1)
        {
            next = checkpoint2;
        }

        if ((collider == checkpoint2 || collider == checkpoint2bis) && next == checkpoint2)
        {
            next = checkpoint3;
        }

        if (collider == checkpoint3 && next == checkpoint3)
        {
            next = checkpoint4;
        }

        if (collider == checkpoint4 && next == checkpoint4)
        {
            next = checkpointArr;
        }

        if (collider == checkpointArr && next == checkpointArr)
        {
            next = checkpoint1;
            GameManager.Instance.score++;
            GameManager.Instance.UpdateScore();
            if (GameManager.Instance.score == tour_number)
            {
                EndRecording();
                GameManager.Instance.EndRace();
                return;
            }
        }


        /* if (collider == checkpoints[next]){
             if(next == checkpoints.Length-1){
                 GameManager.Instance.score ++;

                 GameManager.Instance.UpdateScore();
                 if (GameManager.Instance.score == tour_number)
                 {
                     EndRecording();
                     GameManager.Instance.EndRace();
                     return;
                 }
                 next = 0;
                 return;
             }
             next++;
         }*/
    }

}
