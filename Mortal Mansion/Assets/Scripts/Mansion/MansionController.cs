using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MansionController : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public List<Room> currRooms = new();
    [SerializeField] private List<Room> roomTypes = new();
    [SerializeField] private Room bedroom;
    [SerializeField] public int roomCount;
    [SerializeField] private int bedroomFactor; // one bedroom every bedroomFactor number of rooms
    [SerializeField] private Bed bed;

    private int randRoomInd;

    // Start is called before the first frame update
    void Start()
    {
        roomCount = currRooms.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addRoom(){
        randRoomInd = Random.Range(0, roomTypes.Count);

        Room newRoom = roomTypes[randRoomInd];

        newRoom.setupRoom();
        currRooms.Add(newRoom);

        roomCount++;
    }
}
