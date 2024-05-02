using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public LevelRoom currentRoom;
    public List<LevelRoom> failedRooms = new List<LevelRoom>();
    public List<LevelRoom> finishedRooms = new List<LevelRoom>();
    public GameObject player;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void ChangeCurrentRoom(LevelRoom room)
    {
        currentRoom = room;
    }
    public void ResetCurrentRoom()
    {
        currentRoom = null;
    }
}
