# <p align="center">동물 게임 : 머지 애니멀</p>

<p align="center">
<img src="https://github.com/gusdk337/AnimalGameResult/assets/51481890/e3d18a6d-008b-4106-90f3-b923aa9a54fc" width="200">
</p>

## 게임 소개
같은 동물을 합쳐 다음 단계의 동물을 만들고 점수를 쌓는 게임
 
## 개발 기간 & 개발 인원
- 개발 기간: 2023.10.10~2023.10.21(11일)
- 개발 인원: 1인

## 구글 플레이스토어 링크
https://play.google.com/store/apps/details?id=com.crunkymacaron.animalgame

## 핵심 기능
1. 튜토리얼
   &nbsp;
   > - InfoManager를 통해 신규유저 판독
   > - EventManager를 활용하여 신규유저일 때 이벤트 발동
   > - 이전 버튼과 다음 버튼을 이용하여 이미지를 앞뒤로 넘겨 튜토리얼 가능

<details>
 <summary>코드 보기</summary>
 
```
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

```
▲ InfoManager 스크립트 

```
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

```
▲ App 스크립트 중 일부

```
        EventManager.instance.ShowToturial = () =>
        {
            this.director.uiTutorial.gameObject.SetActive(true);
        };

        if (InfoManager.instance.BestScoreInfo.bestScore == 0)  //베스트 스코어가 0이면 신규유저
        {
            //신규유저일 때 튜토리얼 보여주기;
            EventManager.instance.ShowToturial();
        }

```
▲ GameMain 스크립트 중 일부

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    public Button btnNext;
    public Button btnPrev;
    public Button btnClose;

    public Image[] imgs;

    public void Init()
    {
        this.gameObject.SetActive(true);
    }

    void Start()
    {
        this.btnNext.onClick.AddListener(() =>  //다음 버튼 누르면
        {
            //0번 이미지가 활성화되어 있다면 0번을 비활성화 시키고 1번을 활성화시킨다.
            if (this.imgs[0].gameObject.activeSelf)
            {
                this.imgs[0].gameObject.SetActive(false);
                this.imgs[1].gameObject.SetActive(true);
            }
            else if (this.imgs[1].gameObject.activeSelf)
            {
                this.imgs[1].gameObject.SetActive(false);
                this.imgs[2].gameObject.SetActive(true);
            }
            else if (this.imgs[2].gameObject.activeSelf)
            {
                this.imgs[2].gameObject.SetActive(false);
                this.imgs[3].gameObject.SetActive(true);
            }
            else if (this.imgs[3].gameObject.activeSelf)
            {
                this.imgs[3].gameObject.SetActive(false);
                this.imgs[4].gameObject.SetActive(true);
                this.btnClose.gameObject.SetActive(true);
            }
            else if (this.imgs[4].gameObject.activeSelf)
            {
                //마지막 페이지에선 아무 효과 없음
            }
        });
        this.btnPrev.onClick.AddListener(() =>  //이전 버튼을 누르면
        {
            if (this.imgs[0].gameObject.activeSelf)
            {
                //첫 페이지에선 아무 효과 없음
            }
            else if (this.imgs[1].gameObject.activeSelf)
            {
                //1번 이미지가 활성화되어 있다면 1번을 비활성화 시키고 0번을 활성화시킨다.
                this.imgs[1].gameObject.SetActive(false);
                this.imgs[0].gameObject.SetActive(true);
            }
            else if (this.imgs[2].gameObject.activeSelf)
            {
                this.imgs[2].gameObject.SetActive(false);
                this.imgs[1].gameObject.SetActive(true);
            }
            else if (this.imgs[3].gameObject.activeSelf)
            {
                this.imgs[3].gameObject.SetActive(false);
                this.imgs[2].gameObject.SetActive(true);
            }
            else if (this.imgs[4].gameObject.activeSelf)
            {
                this.imgs[4].gameObject.SetActive(false);
                this.imgs[3].gameObject.SetActive(true);
            }
        });

        this.btnClose.onClick.AddListener(() =>
        {
            //닫기 버튼 누르면 비활성화
            this.gameObject.SetActive(false);
        });

    }
}

```
▲ UITutorial 스크립트

&nbsp;
</details>
&nbsp;

2. 동물 합치기
   
<details>
 <summary>코드 보기</summary>

</details>
