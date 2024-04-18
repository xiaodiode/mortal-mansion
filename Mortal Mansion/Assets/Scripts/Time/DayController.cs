using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour
{
    [SerializeField] public bool isNight;
    [SerializeField] private MansionController mansion;

    // Start is called before the first frame update
    void Start()
    {
        isNight = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endCycle(){
        if(isNight){

        }
        isNight = !isNight;
    }

    private void setupDaytime(){
        foreach(Room room in mansion.currRooms){
            
        }
    }
}
