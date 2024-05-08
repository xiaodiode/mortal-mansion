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

    
    [SerializeField] private CoinController coinController;
    [SerializeField] private Coins coinData;
    [SerializeField] private GameObject coin;
    [SerializeField] private int baseCoinsSpawned;


    [SerializeField] private NormGhostAI normGhostAI;

    private int randRoomInd;

    // Start is called before the first frame update
    void Start()
    {
        roomCount = currRooms.Count;

        if(baseCoinsSpawned > roomCount){
            baseCoinsSpawned = roomCount;
        }

        // setupCoins();
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

    // private void setupCoins(){
    //     GameObject newCoin;
    //     coinController.totalCoins = 0;

        


    //     foreach(Room room in currRooms){
    //         Debug.Log("floor: " + room.floor + "| coinData.width, coinData.height: " + coinData.width + ", " + coinData.height + " | coin: " + coin);
    //         newCoin = room.floor.createAtRandomPosition(coinData.width, coinData.height, coin);
    //         coinController.coins.Add(newCoin);
    //         coinController.totalCoins++;

    //         NormGhostAI newGhost = new();
    //         newGhost = normGhostAI;
    //         room.floor.ghost = newGhost;
    //         room.floor.setGhost();
    //     }

    //     coinController.updateCoinsUI();

    // }
}
