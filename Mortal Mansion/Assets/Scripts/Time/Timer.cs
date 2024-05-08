using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private int totalSeconds;

    [Header("UI Timer")]
    [SerializeField] private TextMeshProUGUI timeUI;

    private string leadingZeroM, leadingZeroS;
    private string timeText = "00:00:00";
    private bool countDown = false;
    private int minutes, seconds;
    public int secondsStarted, secondsLeft;
    
    // Start is called before the first frame update
    void Start()
    {
        leadingZeroM = "0";
        leadingZeroS = "0";

        // currDayTime = dayBase;
        // currNightTime = nightBase;

        // totalSeconds = currDayTime;

        startCountDown(15); //delete this later. will need to start counting down after player starts game
    }

    // Update is called once per frame
    void Update()
    {
        if(countDown){
            updateTimerUI();
        }
        
    }

    public void updateTimerUI(){
        secondsLeft = totalSeconds - (Mathf.FloorToInt(Time.time) - secondsStarted);
        timeText = getTimeText(secondsLeft);

        timeUI.text = timeText;
        
        // if(secondsPassed == 0){
        //     dayController.endCycle();
        //     resetTimer();
        // }

    }


    public void startCountDown(int newTotalSeconds){
        totalSeconds = newTotalSeconds;

        secondsStarted = Mathf.FloorToInt(Time.time);
        countDown = true;
    }

    public void pauseTimer(){
        countDown = false;
    }
    public void resumeTimer(){
        countDown = true;
    }
    public void resetTimer(int newTotalSeconds){
        countDown = false;
        
        // if(dayController.isNight){
        //     totalSeconds = currNightTime;
        // }
        // else{
        //     totalSeconds = currDayTime;
        // }

        totalSeconds = newTotalSeconds;

        // Debug.Log("Total seconds from reset: " + totalSeconds);

        timeUI.text = getTimeText(totalSeconds);

        // Debug.Log("time.text should be updated to : " + timeUI.text);
    }

    public int getTime(){
        return Mathf.FloorToInt(Time.time);
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
