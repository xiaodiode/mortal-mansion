using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MansionController : MonoBehaviour
{
    [SerializeField] public List<Room> rooms = new();
    [SerializeField] public int roomCount;

    // Start is called before the first frame update
    void Start()
    {
        roomCount = rooms.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addRoom(){
        Room newRoom = new();

        newRoom.setupRoom();
        rooms.Add(newRoom);

        roomCount++;
    }
}
