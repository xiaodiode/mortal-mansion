using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    [Header("Time (in seconds) For Each Cycle")] 
    [SerializeField] public int dayBase, nightBase;
    [SerializeField] public int currDayTime, currNightTime;
    [SerializeField] public int timeIncrement; // how many seconds to increment both timers after each round

    [SerializeField] private DayController dayController; 
    [SerializeField] private int totalSeconds;

    [Header("UI Timer")]
    [SerializeField] private TextMeshProUGUI timeUI;

    private string leadingZeroH, leadingZeroM, leadingZeroS;
    private string timeText = "00:00:00";
    private bool countDown = false;
    private int hours, minutes, seconds;
    private int secondsStarted, secondsPassed;
    
    // Start is called before the first frame update
    void Start()
    {
        leadingZeroH = "0";
        leadingZeroM = "0";
        leadingZeroS = "0";

        currDayTime = dayBase;
        currNightTime = nightBase;

        totalSeconds = currDayTime;

        startCountDown(); //delete this later. will need to start counting down after player starts game
    }

    // Update is called once per frame
    void Update()
    {
        if(countDown){
            updateTimerUI();
        }
        
    }

    public void updateTimerUI(){
        secondsPassed = totalSeconds - (Mathf.FloorToInt(Time.time) - secondsStarted);
        timeText = getTimeText(secondsPassed);

        timeUI.text = timeText;
        
        if(secondsPassed == 0){
            // levelClear = true;
            // if(player.selectedLevel == player.levelsUnlocked){
            //     gemController.enableGemPopup("level");
            //     if(player.selectedLevel != 101)
            //         player.levelsUnlocked+=1;
            // }
            // gameManager.gameOver();
            dayController.switchCycles();
            resetTimer();
        }

        
        
        
    }

    public void increaseTime(){
        currDayTime += timeIncrement;
        currNightTime += timeIncrement;
    }

    public void startCountDown(){
        secondsStarted = Mathf.FloorToInt(Time.time);
        countDown = true;
    }

    public void pauseTimer(){
        countDown = false;
    }
    public void resumeTimer(){
        countDown = true;
    }
    public void resetTimer(){
        countDown = false;
        
        if(dayController.isNight){
            totalSeconds = currNightTime;
        }
        else{
            totalSeconds = currDayTime;
        }

        // Debug.Log("Total seconds from reset: " + totalSeconds);

        timeUI.text = getTimeText(totalSeconds);

        // Debug.Log("time.text should be updated to : " + timeUI.text);
    }

    public int getTime(){
        return Mathf.FloorToInt(Time.time);
    }
    public int getGameTime(){
        return secondsPassed;
    }

    private string getTimeText(int timeSeconds){
        seconds = timeSeconds % 60;
        minutes = (timeSeconds/60) % 60;
        if(minutes > 9){
            leadingZeroM = "";
        }
        else{
            leadingZeroM = "0";
        }
        if(seconds > 9){
            leadingZeroS = ":";
        }
        else{
            leadingZeroS = ":0";
        }

        timeText = leadingZeroM + minutes.ToString() + leadingZeroS + seconds.ToString();

        return timeText;
    }

    
}
