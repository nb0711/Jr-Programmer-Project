using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static MainManager Instance;
    public Color TeamColor; // new variable declared

    private void Awake()
    {

        // start of new code
        if (Instance != null)  // Eğer scene yeniden yüklenirse mesela alt bir sahne açılıp tekrar geri gelidiğinde bu scrip yeniden oluşur ve 2 tane ayni scrip ten olur. bu sebeple son oluşturulan scrip bu sorguyla yok edilir.
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();

    }

    [System.Serializable]  // JSON'a dönüştürmek için bunu eklemek zorundayız.
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }


    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }


}
