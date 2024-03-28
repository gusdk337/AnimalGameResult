# <p align="center">동물 게임 : 머지 애니멀</p>

<p align="center">
<img src="https://github.com/gusdk337/AnimalGameResult/assets/51481890/e3d18a6d-008b-4106-90f3-b923aa9a54fc" width="200">
</p>

## 게임 소개
같은 동물을 합쳐 다음 단계의 동물을 만들고 점수를 쌓는 게임

&nbsp;

## 개발 기간 & 개발 인원
- 개발 기간: 2023.10.10~2023.10.21(11일)
- 개발 인원: 1인
  
&nbsp;

## 구글 플레이스토어 링크
https://play.google.com/store/apps/details?id=com.crunkymacaron.animalgame

&nbsp;

## 핵심 기능
1. 튜토리얼
   > - InfoManager를 통해 신규유저 판독
   > - EventManager를 활용하여 신규유저일 때 이벤트 발동
   > - 이전 버튼과 다음 버튼을 이용하여 이미지를 앞뒤로 넘겨 튜토리얼 가능

<details>
 <summary>코드 보기</summary>
 
```ts
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

```ts
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

```ts
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

```ts
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
</details>
&nbsp;

2. 동물 합치기
   > - 각각의 동물들이 한 스크립트를 사용중이기 때문에 static을 사용하여 동물이 합쳐질 때 하나의 동물이 생성되게 함
   > - EventManager를 활용하여 점수 추가

<details>
 <summary>코드 보기</summary>
 
```ts
    private static bool hasExecuted = false;    //한 번만 실행되도록

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.tag == collision.gameObject.tag && !hasExecuted)    //닿았을 때 2마리가 아닌 1마리가 나오도록(각각의 동물들이 한 스크립트를 쓰기 때문)
        {
            int animalIndex = -1;

            switch (this.gameObject.tag)
            {
                case "Egg": //Egg끼리 닿으면 animalIndex를 1로 설정하고 플러스 점수를 1점으로 설정
                    animalIndex = 1;
                    this.plusScore = 1;
                    break;

                case "Chicken":
                    animalIndex = 2;
                    this.plusScore = 3;
                    break;

                case "Frog":
                    animalIndex = 3;
                    this.plusScore = 6;
                    break;

                case "Rabbit":
                    animalIndex = 4;
                    this.plusScore = 10;
                    break;

                case "Cat":
                    animalIndex = 5;
                    this.plusScore = 15;
                    break;

                case "Dog":
                    animalIndex = 6;
                    this.plusScore = 21;
                    break;

                case "Pig":
                    animalIndex = 7;
                    this.plusScore = 28;
                    break;

                case "Penguin":
                    animalIndex = 8;
                    this.plusScore = 36;
                    break;

                case "Bear":
                    animalIndex = 9;
                    this.plusScore = 45;
                    break;

                case "Reindeer":
                    animalIndex = 10;
                    this.plusScore = 55;
                    break;

                case "Unicorn":
                    animalIndex = 100;
                    break;
            }

            if (animalIndex != -1 && animalIndex != 100)    //유니콘 제외(유니콘이 가장 높은 동물이기 때문)
            {
                GameObject parent = GameObject.Find("Animals");
                GameObject newAnimal = Instantiate(animals[animalIndex], this.transform.position, Quaternion.identity);
                newAnimal.transform.parent = parent.transform;
                Rigidbody2D newAnimalRb = newAnimal.GetComponent<Rigidbody2D>();

                if (newAnimal.CompareTag("Dog") || newAnimal.CompareTag("Pig") || newAnimal.CompareTag("Penguin") || newAnimal.CompareTag("Bear") || newAnimal.CompareTag("Reindeer"))
                {
                    newAnimalRb.gravityScale = 1;   //윗 단계 동물들의 중력을 낮게 설정
                }
                else if(newAnimal.CompareTag("Egg") || newAnimal.CompareTag("Chicken") || newAnimal.CompareTag("Frog") || newAnimal.CompareTag("Rabbit") || newAnimal.CompareTag("Cat"))
                {
                    newAnimalRb.gravityScale = 10;  //아래 단계 동물들의 중력을 높게 설정
                }
                this.gameObject.SetActive(false);
                collision.gameObject.SetActive(false);
                SoundManager.PlaySFX("Pop");
                EventManager.instance.addScore(this.plusScore); //해당 점수 추가
                hasExecuted = true;
            }
            else if(animalIndex == 100)
            {
                Debug.Log("유니콘끼리 부딪힘");
            }
        }
        else
        {
            hasExecuted = false;
        }
    }

```
▲ AnimalController 스크립트 중 일부
</details>
&nbsp;

3. 게임 오버
   > - 게임오버라인에 특정 시간동안 동물이 머물러 있으면 게임 오버
   > - InfoManager에 베스트스코어 저장

<details>
 <summary>코드 보기</summary>

```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLine : MonoBehaviour
{
    public bool isGameOver;
    private bool isInsideCollider = false;

    public bool isCounting;
    public float timer = 0.0f;

    private void OnTriggerEnter2D(Collider2D collision) //게임오버라인에 들어오면
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Animal"))   
        {
            this.isInsideCollider = true;
            StartCoroutine(this.CheckColliderStay());   //시간 측정 시작
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  //게임오버라인에서 나가면(동물이 잘 내려가면)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {
            this.isCounting = false;
            this.isInsideCollider = false;
            StopCoroutine(this.CheckColliderStay());
        }

    }


    private IEnumerator CheckColliderStay()
    {
        float timer = 0.0f;

        while (this.isInsideCollider)
        {
            timer += Time.deltaTime;

            if (timer >= 1.2f)  //1.2초 뒤에 게임오버
            {
                this.isGameOver = true;
                break;
            }
            yield return null;
        }
    }
}

```
▲ GameOverLine 스크립트

```ts
        if (this.gameOverLine.isGameOver)
        {
            Destroy(this.objects);  //3d오브젝트들 정리

            this.director.uiGameOver.txtScore.text = this.director.uiScore.txtCurrentScore.text;
            this.director.uiGameOver.txtBestScore.text = this.director.uiScore.txtBestScore.text;

            InfoManager.instance.BestScoreInfo.bestScore = this.director.uiScore.bestScore;
            InfoManager.instance.SaveBestScoreInfo();   //베스트스코어 저장
            this.director.uiGameOver.gameObject.SetActive(true);

            this.newAnimal.SetActive(false);
        }
```
▲ GameMain 스크립트 중 일부
</details>
&nbsp;

## 플레이 영상

## BGM
