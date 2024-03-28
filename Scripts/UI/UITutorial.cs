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
