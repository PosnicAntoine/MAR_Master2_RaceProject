using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    private Race race;
    int cursor;

    bool isMoving;
    public Vector3 rotation;

    float launchTime;
    void Awake(){
        isMoving = false;
    }

    void Update()
    {
        if(isMoving){
            if (cursor < race.Trajectory.Count){
                float tf = race.GetTime(cursor);
                float currentT = Time.time - launchTime;
                while(currentT > tf){
                    cursor++;
                    if (cursor < race.Trajectory.Count){
                        tf = race.GetTime(cursor);
                    } 
                    else {
                        End(); 
                        return;
                    }
                    
                }
                float ti = race.GetTime(cursor-1);
                float ratio = (currentT-ti) / (tf-ti);
            
                Vector3 posI = race.GetPos(cursor-1);
                Vector3 posF = race.GetPos(cursor);
                Vector3 interpolePos = 
                    Vector3.Lerp(posI,posF,ratio);

                rotation = (interpolePos - transform.position) / Time.deltaTime;
                transform.position = interpolePos; 

                transform.Rotate(rotation);
                
                return;
            }
            End();       
        }
    }

    public void SetUp(Race race){
        
        this.race = race;
    
        if(race.Trajectory.Count > 0){
            Debug.Log("SetUp");
            transform.position = race.GetPos(0);
            Debug.Log(race.Trajectory.Count);
        }
        cursor = 1;
    }

    public void Launch(){
        if(race.Trajectory.Count > 0){
            
            launchTime = Time.time;
            isMoving = true;
        }
    }

    void End(){
        isMoving = false;

        Debug.Log(cursor);
        Debug.Log("End");
    }
}
