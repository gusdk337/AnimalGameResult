using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public UIGameDirector director;
    public AnimalsController controller;
    public SpawnPoint spawnPoint;
    public GameOverLine gameOverLine;

    public GameObject objects;

    private bool isClicked;

    public GameObject[] animals;
    public Transform spawnTrans;

    public GameObject newAnimal;
    public GameObject nextAnimal;

    private void Start()
    {
        EventManager.instance.ShowToturial = () =>
        {
            this.director.uiTutorial.gameObject.SetActive(true);
        };

        if (InfoManager.instance.BestScoreInfo.bestScore == 0)  //����Ʈ ���ھ 0�̸� �ű�����
        {
            //�ű������� �� Ʃ�丮�� �����ֱ�;
            EventManager.instance.ShowToturial();
        }


        AudioListener.volume = 5;
        this.director.btnPush.onClick.AddListener(() =>
        {
            this.isClicked = true;
            this.controller.rb.gravityScale = 2;    //�����ִ� ���� Ǫ��

            Invoke("SpawnNewAnimal", 0.5f);

            Invoke("DestroyNextAnimal", 0.8f);  //�ٷ� �����ϸ� ������ ã�� ���ϴ� ���� �߻�

            Invoke("SetController", 0.8f);    //���� ������ �������� ��Ʈ�ѷ��� �ο��ϴ� �ð��� �����. �ȱ׷��� �����տ� �ִ� ������Ʈ�� �ν���.

            Invoke("IsClicked", 1f);
        });

    }

    private void Update()
    {
        if(this.controller == null || this.controller.rb.gravityScale >= 1)
        {
            this.controller = this.newAnimal.GetComponent<AnimalsController>();
        }

        if (this.isClicked == false)
        {
            this.director.btnPush.interactable = true;
            this.director.btnLeftArrow.enabled = true;
            this.director.btnRightArrow.enabled = true;
        }
        else
        {
            this.director.btnPush.interactable = false;
            this.director.btnLeftArrow.enabled = false;
            this.director.btnRightArrow.enabled = false;
        }

        if (this.director.btnLeftArrow.isBtnLeftPressed)
        {
            this.controller.StartLeftMoving();
        }
        else
        {
            this.controller.StopLeftMoving();
        }

        if (this.director.btnRightArrow.isBtnRightPressed)
        {
            this.controller.StartRightMoving();
        }
        else
        {
            this.controller.StopRightMoving();
        }

        if(this.nextAnimal == null)
        {
            Debug.Log("��");
            Invoke("SetNextAnimal", 0.2f);
        }

        if (this.gameOverLine.isGameOver)
        {
            Destroy(this.objects);  //3d������Ʈ�� ����

            this.director.uiGameOver.txtScore.text = this.director.uiScore.txtCurrentScore.text;
            this.director.uiGameOver.txtBestScore.text = this.director.uiScore.txtBestScore.text;

            InfoManager.instance.BestScoreInfo.bestScore = this.director.uiScore.bestScore;
            InfoManager.instance.SaveBestScoreInfo();   //����Ʈ���ھ� ����
            this.director.uiGameOver.gameObject.SetActive(true);

            this.newAnimal.SetActive(false);
        }

        if (this.director.uiExit.gameObject.activeSelf)
        {
            this.objects.SetActive(false);
            Invoke("StopTime", 0.2f);
        }
        else
        {
            Time.timeScale = 1;
            this.objects.SetActive(true);
        }
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void IsClicked()
    {
        this.isClicked = false;
    }

    public void SetController()
    {
        this.controller = this.newAnimal.GetComponent<AnimalsController>();
    }

    public void DestroyNextAnimal()
    {
        Destroy(this.director.uiNext.nextAnimal.gameObject);
    }

    public void SetNextAnimal()
    {
        this.nextAnimal = this.director.uiNext.nextAnimal;
    }

    public void SpawnNewAnimal()
    {
        GameObject parent = GameObject.Find("Animals");

        switch (nextAnimal.tag) //������ ���� ������ ����
        {
            case "Egg":
                this.newAnimal = Instantiate(animals[0], new Vector3(this.spawnTrans.position.x, this.spawnTrans.position.y, this.spawnTrans.position.z), Quaternion.identity);
                break;
            case "Chicken":
                this.newAnimal = Instantiate(animals[1], new Vector3(this.spawnTrans.position.x, this.spawnTrans.position.y, this.spawnTrans.position.z), Quaternion.identity);
                break;
            case "Frog":
                this.newAnimal = Instantiate(animals[2], new Vector3(this.spawnTrans.position.x, this.spawnTrans.position.y, this.spawnTrans.position.z), Quaternion.identity);
                break;
            case "Rabbit":
                this.newAnimal = Instantiate(animals[3], new Vector3(this.spawnTrans.position.x, this.spawnTrans.position.y, this.spawnTrans.position.z), Quaternion.identity);
                break;
            case "Cat":
                this.newAnimal = Instantiate(animals[4], new Vector3(this.spawnTrans.position.x, this.spawnTrans.position.y, this.spawnTrans.position.z), Quaternion.identity);
                break;
        }
        this.newAnimal.SetActive(true);
        newAnimal.transform.parent = parent.transform;

    }

}
