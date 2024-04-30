using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour
{
    [Header("Time (in seconds) For Each Cycle")] 
    [SerializeField] public int dayBase, nightBase;
    [SerializeField] public int currDayTime, currNightTime;
    [SerializeField] public int timeIncrement; // how many seconds to increment both timers after each round
    
    [SerializeField] public bool isNight;

    [SerializeField] private Timer timer;
    [SerializeField] private MansionController mansion;

    // Start is called before the first frame update
    void Start()
    {
        isNight = false;

        currDayTime = dayBase;
        currNightTime = nightBase;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.secondsLeft == 0){
            changeCycle();
        }
    }

    public void changeCycle(){
        if(isNight){

        }
        isNight = !isNight;
    }

    private void setupDaytime(){
        foreach(Room room in mansion.currRooms){

        }
    }

    public void increaseTime(){
        currDayTime += timeIncrement;
        currNightTime += timeIncrement;
    }

    public void startNewCycle(){
        if(isNight){
            timer.startCountDown(currNightTime);
        }
        else{
            timer.startCountDown(currDayTime);
        }
    }

}
