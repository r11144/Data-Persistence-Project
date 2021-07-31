using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

   public int highPScore;

   public Text highScoreText;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadDetails();
    }

    private void Start()
    {
        highScoreText.text = "High Score : " + highPScore.ToString();
    }


    [System.Serializable]
    class SaveData
    {
        public int highScore;
    }
    
    public void SaveDetails()
    {
        SaveData data = new SaveData();
        data.highScore = highPScore;
       

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
   
   
    public void LoadDetails()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highPScore = data.highScore;  
        }
    }
}
