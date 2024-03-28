using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameDirector : MonoBehaviour
{
    public BtnLeftArrow btnLeftArrow;
    public BtnRightArrow btnRightArrow;
    public Button btnPush;
    public UINext uiNext;
    public UIGameOver uiGameOver;
    public UIScore uiScore;
    public UITutorial uiTutorial;
    public Button btnExit;
    public UIExit uiExit;

    public GameOverLine gameOverLine;
    public Text txtTimerCount1;
    public Text txtTimerCount2;
    private float totalTime = 3f;
    private float currentTime;
    public bool isGameOver;

    private void Start()
    {
        this.currentTime = this.totalTime;

        this.btnExit.onClick.AddListener(() =>
        {
            this.uiExit.gameObject.SetActive(true);
        });
    }
    private void Update()
    {
        if (this.btnLeftArrow.isBtnLeftPressed || this.btnRightArrow.isBtnRightPressed) //�����̰� ���� �� Ǫ�� ��ư ��Ȱ��ȭ
        {
            this.btnPush.enabled = false;
        }
        else
        {
            this.btnPush.enabled = true;
        }

        if (this.gameOverLine.isCounting)   //ī��Ʈ �ٿ� ǥ��
        {
            StartCoroutine(this.Timer());
        }
        else
        {
            StopCoroutine(this.Timer());

            this.txtTimerCount1.gameObject.SetActive(false);
            this.txtTimerCount2.gameObject.SetActive(false);
            this.currentTime = this.totalTime;
        }

        if (this.currentTime <= 0)
        {
            StopCoroutine(this.Timer());
            this.txtTimerCount1.gameObject.SetActive(false);
            this.txtTimerCount2.gameObject.SetActive(false);
            this.isGameOver = true;
        }


    }

    private void UpdateTimerDisplay()
    {
        //�ð��� ������ �ݿø��Ͽ� �ؽ�Ʈ�� ǥ��
        txtTimerCount1.text = Mathf.Ceil(currentTime).ToString();
        txtTimerCount2.text = Mathf.Ceil(currentTime).ToString();
    }

    private IEnumerator Timer()
    {
        this.txtTimerCount1.gameObject.SetActive(true);
        this.txtTimerCount2.gameObject.SetActive(true);
        currentTime -= Time.deltaTime;
        UpdateTimerDisplay();
        this.btnPush.interactable = false;
        this.btnLeftArrow.enabled = false;
        this.btnRightArrow.enabled = false;

        yield return null;
    }

}
