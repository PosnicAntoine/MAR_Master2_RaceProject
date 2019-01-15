using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData 
{
    public Race bestRace;
    public Race lastRace;

    public GameData(){
        bestRace = new Race();
        lastRace = new Race();
    }

}
