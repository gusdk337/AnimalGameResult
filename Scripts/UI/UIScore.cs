using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScore : MonoBehaviour
{
    public TMP_Text txtCurrentScore;
    public TMP_Text txtBestScore;
    public int plusScore;
    public int currentScore;
    public int bestScore;

    void Start()
    {
        this.bestScore = InfoManager.instance.BestScoreInfo.bestScore;
        this.txtBestScore.text = this.bestScore.ToString();

        //스코어
        EventManager.instance.addScore = (score) =>
        {
            this.plusScore = score;
            this.UpdateScore(); //점수 업데이트
        };

    }

    private void Update()
    {
    }

    public void UpdateScore()
    {
        this.currentScore += this.plusScore;

        if (this.currentScore > this.bestScore)
        {
            this.bestScore = this.currentScore;
        }

        this.txtCurrentScore.text = currentScore.ToString();
        this.txtBestScore.text = this.bestScore.ToString();

        InfoManager.instance.SaveBestScoreInfo();   //점수 저장
    }
}
