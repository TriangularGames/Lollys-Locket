using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem: MonoBehaviour
{

    public SavedGame[] saveSlots;

    void Awake()
    {
        foreach (SavedGame saved in saveSlots)
        {
            ShowSavedFiles(saved);
        }

        //TODO  SET Default values if no saved game is found
    }

    private void ShowSavedFiles(SavedGame savedGame)
    {
        if (File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".hippo"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".hippo", FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            PlayerData player = data.MyPlayerData;
            savedGame.ShowInfo(player);
            stream.Close();

        }
    }

    //Add player script thing here
    public void SaveGame(SavedGame savedGame)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".hippo", FileMode.OpenOrCreate);

        //Add call to player script here
        GameData data = new GameData();

        SaveData(data);

        formatter.Serialize(stream, data);
        stream.Close();
        savedGame.ShowInfo(data.MyPlayerData);
        ShowSavedFiles(savedGame);

    }

    private void SaveData(GameData data)
    {
        string[] a = new string[5];
        data.MyPlayerData = new PlayerData(new Vector2(0, 0), a);
    }

    //TODO: IMPLEMENT LOADING SHIT GARBAGE
    public void LoadGame(SavedGame savedGame)
    {

        if (File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".hippo"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".hippo", FileMode.Open);
            try
            {
                GameData data = formatter.Deserialize(stream) as GameData;
                LoadData(data);
            }
            finally
            {
                stream.Close();
            }
            
        }

    }

    private void LoadData(GameData data)
    {
        //set player data to data given
    }

}
