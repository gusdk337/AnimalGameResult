using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    public enum eSceneType
    {
        App,
        Title,
        Game
    }

    private eSceneType state;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        this.state = eSceneType.Title;

        GPGSManager.GetInstance().Authenticate();
        EventManager.instance = new EventManager();

        string bestScorePath = InfoManager.instance.bestScorePath;
        Debug.LogFormat("<color=cyan>{0}</color>", bestScorePath);
        if (!InfoManager.instance.IsNewbie(bestScorePath))
        {
            //기존 유저
            //베스트 점수 불러오기
            InfoManager.instance.LoadBestScoreInfo();
        }
        else
        {
            //신규 유저
            InfoManager.instance.BestScoreInfo = new BestScoreInfo(0);
            InfoManager.instance.SaveBestScoreInfo();
        }

        this.ChangeScene(this.state);

    }

    public void ChangeScene(eSceneType sceneType)
    {
        switch (sceneType)
        {
            case eSceneType.Title:
                var titleOper = SceneManager.LoadSceneAsync("Title");
                titleOper.completed += (obj) =>
                {
                    EventManager.instance.changeScene = () =>
                    {
                        Debug.Log("버튼 누름");
                        this.state = eSceneType.Game;
                        this.ChangeScene(this.state);
                    };
                };
                break;

            case eSceneType.Game:
                var gameOper = SceneManager.LoadSceneAsync("Game");
                gameOper.completed += (obj) =>
                {

                };
                break;
        }
    }
}
