using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class InfoManager
{
    public static readonly InfoManager instance = new InfoManager();

    public string bestScorePath = string.Format("{0}/bestScore.json", Application.persistentDataPath);

    public BestScoreInfo BestScoreInfo { get; set; }

    private InfoManager()
    {

    }

    public void LoadBestScoreInfo()
    {
        var json = File.ReadAllText(bestScorePath);
        //역직렬화 
        this.BestScoreInfo = JsonConvert.DeserializeObject<BestScoreInfo>(json);
    }

    public void SaveBestScoreInfo()
    {
        var json = JsonConvert.SerializeObject(this.BestScoreInfo);
        File.WriteAllText(bestScorePath, json);
    }

    public bool IsNewbie(string path)
    {
        bool existFile = File.Exists(path);
        return !existFile;
    }
}
