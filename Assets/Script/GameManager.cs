using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;             //This script, like MouseLocation, has a public static reference to itself to that other scripts
                                                    //can access it from anywhere without needing to find a reference to it
    [HideInInspector] public bool gamemodePlay;
    [HideInInspector] public bool gamemodeReplay;
    [HideInInspector] public bool gamemodeGhostRace;
    GameObject car;
    GameObject ghost;
    [HideInInspector]
    public int score = 0;                                  //The player's current score

    void Awake()
    {
        //This is a common approach to handling a class with a reference to itself.
        //If instance variable doesn't exist, assign this object to it
        if (Instance == null)
            Instance = this;
        //Otherwise, if the instance variable does exist, but it isn't this object, destroy this object.
        //This is useful so that we cannot have more than one GameManager object in a scene at a time.
        else if (Instance != this)
            Destroy(this);
    }

    public void PlayRace()
    {
        gamemodePlay = true;
        gamemodeGhostRace = false;
        gamemodeReplay = false;
        Debug.Log("gamemodeplay : "+ gamemodePlay.ToString());
    }

    public void GhostRace()
    {
        gamemodeGhostRace = true;
        gamemodePlay = false;
        gamemodeReplay = false;
    }

    public void RePlay()
    {
        gamemodeReplay = true;
        gamemodePlay = false;
        gamemodeGhostRace = false;
    }

}
