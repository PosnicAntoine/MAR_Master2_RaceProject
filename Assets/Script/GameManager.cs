using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;             //This script, like MouseLocation, has a public static reference to itself to that other scripts
                                                    //can access it from anywhere without needing to find a reference to it
    [HideInInspector] public bool gamemodePlay;
    [HideInInspector] public bool gamemodeReplay;
    [HideInInspector] public bool gamemodeGhostRace;
    public GameObject car;
    public GameObject ghost;
    public Camera run;
    public Camera replay1;
    public Camera replay2;
    public Camera replay3;
    public Camera replay4;
    public Camera replay5;
    public Camera replay6;
    public Text scoreui;
    public Text speedtext;
    public Text timetext;
    public Text textresults;
    public GameObject panel;
    [HideInInspector]
    public int score = 0;
    private float time;
    List<float> timetours;    

    public DataController dataController;

    void Awake()
    {
       
        if (Instance == null)
            Instance = this;
       
        else if (Instance != this)
            Destroy(this);

        AudioSource[] sounds = car.GetComponents<AudioSource>() ;
        foreach (AudioSource i in sounds)
        {
            i.enabled=false;
        }
        HidePlayUI();
        
    }

    private void Update()
    {
        time += Time.deltaTime;
        UpdateTime();
        UpdateSpeed();
    }

    public void PlayRace()
    {   
        car.GetComponent<VehicleBehavior>().enabled = true;
        ShowPlayUI();
        gamemodePlay = true;
        gamemodeGhostRace = false;
        gamemodeReplay = false;
        score = 0;
        time = 0;
        timetours = new List<float>();
        UpdateScore();
        AudioSource[] sounds = car.GetComponents<AudioSource>();
        foreach (AudioSource i in sounds)
        {
            i.enabled = true;
        }
        Debug.Log("gamemodeplay : "+ gamemodePlay.ToString());
        run.enabled = true;
        replay1.enabled = false;
        replay2.enabled = false;
        replay3.enabled = false;
        replay4.enabled = false;
        replay5.enabled = false;
        replay6.enabled = false;

        Recorder.Instance.StartRecording();
     }

    public void GhostRace()
    {
        car.GetComponent<VehicleBehavior>().enabled = true;
        ShowPlayUI();
        gamemodeGhostRace = true;
        gamemodePlay = false;
        gamemodeReplay = false;
        score = 0;
        time = 0;
        UpdateScore();
        ghost.SetActive(true);
        run.enabled = true;
        replay1.enabled = false;
        replay2.enabled = false;
        replay3.enabled = false;
        replay4.enabled = false;
        replay5.enabled = false;
        replay6.enabled = false;

        ghost.GetComponent<GhostBehavior>().SetUp(dataController.GetBestRace());
        Recorder.Instance.StartRecording();
        ghost.GetComponent<GhostBehavior>().Launch();
    }

    public void RePlay()
    {
        gamemodeReplay = true;
        gamemodePlay = false;
        gamemodeGhostRace = false;
        run.enabled = false;
        replay1.enabled = true;
        replay2.enabled = false;
        replay3.enabled = false;
        replay4.enabled = false;
        replay5.enabled = false;
        replay6.enabled = false;

        car.GetComponent<VehicleBehavior>().SetUpReplay(dataController.GetLastRace());
        car.GetComponent<VehicleBehavior>().Launch();
    }

    public void Enable1()
    {
        if (gamemodeReplay)
        {
            run.enabled = false;
            replay1.enabled = true;
            replay2.enabled = false;
            replay3.enabled = false;
            replay4.enabled = false;
            replay5.enabled = false;
            replay6.enabled = false;
        }
    }

    public void Enable2()
    {
        if (gamemodeReplay)
        {
            run.enabled = false;
            replay1.enabled = false;
            replay2.enabled = true;
            replay3.enabled = false;
            replay4.enabled = false;
            replay5.enabled = false;
            replay6.enabled = false;
        }
    }

    public void Enable3()
    {
        if (gamemodeReplay)
        {
            run.enabled = false;
            replay1.enabled = false;
            replay2.enabled = false;
            replay3.enabled = true;
            replay4.enabled = false;
            replay5.enabled = false;
            replay6.enabled = false;
        }
    }

    public void Enable4()
    { if (gamemodeReplay)
        {
            run.enabled = false;
            replay1.enabled = false;
            replay2.enabled = false;
            replay3.enabled = false;
            replay4.enabled = true;
            replay5.enabled = false;
            replay6.enabled = false;
        }
    }

    public void Enable5()
    { if (gamemodeReplay)
        {
            run.enabled = false;
            replay1.enabled = false;
            replay2.enabled = true;
            replay3.enabled = false;
            replay4.enabled = false;
            replay5.enabled = true;
            replay6.enabled = false;
        }
    }
    public void Enable6()
    {
        if (gamemodeReplay)
        {
            run.enabled = false;
            replay1.enabled = false;
            replay2.enabled = true;
            replay3.enabled = false;
            replay4.enabled = false;
            replay5.enabled = false;
            replay6.enabled = true;
        }
    }

    public void SaveData(Race race){
        if (!gamemodeReplay){
            dataController.SaveLastRace(race);
            dataController.SubmitNewBestRace(race);
            dataController.UpdateDataFile();
        }
        
    }

    void ShowPlayUI(){
        scoreui.enabled = true;
        timetext.enabled = true;
        speedtext.enabled = true;
    }

    void HidePlayUI(){
        scoreui.enabled = false;
        timetext.enabled = false;
        speedtext.enabled = false;
    }


    public void UpdateScore()
    {
        scoreui.text = score.ToString() + " / " + Recorder.Instance.tour_number.ToString() + " Tours";
    }

    public void UpdateTime()
    {
        string x = time.ToString("0.0");
        timetext.text = x.ToString()+" seconds";
    }

    public void UpdateSpeed()
    {
        string x = ((car.GetComponent<Rigidbody>().velocity.magnitude)*3.6).ToString("0.0");
        speedtext.text= x + " km/h";
    }

    public void SaveTourTime() 
    {
        timetours.Add(time);
    }

    public void EndRace()
    {
        car.GetComponent<VehicleBehavior>().enabled = false;
        panel.SetActive(true);
        string x = "";
        float r = 0;
        for (int i=0; i<(timetours.Count); i++)
        {
            x += "Tour " + i.ToString() + " in " + (timetours[i] - r).ToString("0.00") + " seconds \n";
            r = timetours[i];

        }
        HidePlayUI();
        textresults.text = x;

        gamemodePlay = false;
        gamemodeGhostRace = false;
        gamemodeReplay = false;
        // disable script vehicule behavior
        // display panel avec resultats
        //play feu d'artifice
    }
}
