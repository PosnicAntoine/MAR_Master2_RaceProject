using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Table RACE in sqlite database:
[id,name,time,x,y,z]
with:
- id: numeric
- name: text
- time: numeric
- x: numeric
- y: numeric
- z: numeric
 */

[Serializable]
public class Race 
{
    public string name;

    [SerializeField]
    public List<Vector4> Trajectory; // Vector4 :(time,coord x, coord y, coord z)

    public Race(){
        Trajectory = new List<Vector4>();
    }

    public float GetTime(int index){
        return Trajectory[index].x;
    }

    public Vector3 GetPos(int index){
        Vector3 pos = new Vector3(){
            x = Trajectory[index].y,
            y = Trajectory[index].z,
            z = Trajectory[index].w
        };
        return pos;
    }

    

}
