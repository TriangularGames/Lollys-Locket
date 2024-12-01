using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public PlayerData MyPlayerData { get; set; }

    public GameData()
    {

    }

}

[Serializable]
public class PlayerData
{
    public DateTime MyTime { get; set; }
    public float[] position { get; set; }
    public string[] items { get; set; }

    public PlayerData(Vector2 position, string[] items)
    {
        this.position = new float[2];
        this.position[0] = position.x;
        this.position[1] = position.y;
        this.items = items;
        MyTime = DateTime.Now;
    }

}
