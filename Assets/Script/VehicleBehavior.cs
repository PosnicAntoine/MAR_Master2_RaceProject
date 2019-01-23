using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{

    public float rotationsSpeed=0;
    public float fwdSpeed=0;
    public float velocity=0;
    public float maxVelocity=0;
    private Transform transform;
    private Rigidbody rb;
    public AudioSource audioDataAvance;
    public AudioSource audioDataTourne;

    /// Params for replay mode:
    private Race replayedRace;
    int cursor;
    bool isMoving;
    float launchTime;

    void Start()
    {
            transform = GetComponent<Transform>();
            rb = GetComponent<Rigidbody>();
            isMoving = false;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance != null){
            if (GameManager.Instance.gamemodeReplay){
                if (replayedRace != null){
                    MoveReplay();
                }
                return;
            }
            if ((GameManager.Instance.gamemodePlay || GameManager.Instance.gamemodeGhostRace))
            {
                if (Input.GetKey("space")) {
                    Vector3 orientation = transform.rotation.eulerAngles;
                    orientation.x = -90f;
                    transform.eulerAngles = orientation;
                } 

                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                {
                    audioDataAvance.UnPause();
                } 
                else
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

                if (rb.velocity.magnitude < maxVelocity){
                    rb.AddForce(transform.up * fwdSpeed * moveVertical,ForceMode.Acceleration);
                }
                //transform.Translate(movement * speed * Time.deltaTime);
                transform.Rotate(rotation * rotationsSpeed * 30 * Time.deltaTime);
                //rb.AddTorque(rotation);

            }

        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Check")){
            Recorder.Instance.Check(other);
        }
    }

    public void SetUpReplay(Race race){
        this.replayedRace = race;
    
        if(race.Trajectory.Count > 0){
            transform.position = race.GetPos(0);
        }
        cursor = 1;
    }

    public void Launch(){
        if(replayedRace.Trajectory.Count > 0){
            
            launchTime = Time.time;
            isMoving = true;
        }
    }

    private void End(){
        isMoving = false;
    }

    private void MoveReplay(){
        if(isMoving){
            if (cursor < replayedRace.Trajectory.Count){
                float tf = replayedRace.GetTime(cursor);
                float currentT = Time.time - launchTime;
                while(currentT > tf){
                    cursor++;
                    if (cursor < replayedRace.Trajectory.Count){
                        tf = replayedRace.GetTime(cursor);
                    } 
                    else {
                        End(); 
                        return;
                    }
                    
                }
                float ti = replayedRace.GetTime(cursor-1);
                float ratio = (currentT-ti) / (tf-ti);
            
                Vector3 posI = replayedRace.GetPos(cursor-1);
                Vector3 posF = replayedRace.GetPos(cursor);
                Vector3 interpolePos = 
                    Vector3.Lerp(posI,posF,ratio);

                Vector3 vectorDir = interpolePos - transform.position;
                var rotation = Quaternion.LookRotation(vectorDir);
                rotation *= Quaternion.Euler(-90, 180, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationsSpeed); 
                transform.position = interpolePos; 
                
                return;
            }
            End();       
        }
    }
}
