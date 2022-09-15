using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    List<InputEntry> entries = new List<InputEntry>();

    public string playerName;
    public int playerScore;
    [SerializeField] string filename;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    public void AddUserToList(string name, int score)
    {
        entries.Add(new InputEntry(name, score));
    }

    public void Save(string filename)
    {
        FileHandler.SaveToJSON<InputEntry>(entries, filename);
    }














    [System.Serializable]

    class SaveData
    {
        public string name;
        public int score;
    }

    SaveData data = new SaveData();

    public void SaveName()
    {
        data.name = playerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void SaveScore()
    {
        data.score = playerScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            name = data.name;
        }
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savedata.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerScore = data.score;
        }
    }
}
