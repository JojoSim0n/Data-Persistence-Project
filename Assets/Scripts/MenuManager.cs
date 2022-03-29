using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance;
    public string Name;
    public int Score;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        loadGameState();
    }

    [System.Serializable]
    class SaveData
    {
        public string Name;
        public int Score;
    }

    public void SaveGameState()
    {
        SaveData data = new SaveData();
        data.Name = Name;
        data.Score = Score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void loadGameState()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Name = data.Name;
            Score = data.Score;
        }
    }
}
