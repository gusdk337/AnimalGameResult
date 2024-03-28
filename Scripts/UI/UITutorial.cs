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
        this.btnNext.onClick.AddListener(() =>  //���� ��ư ������
        {
            //0�� �̹����� Ȱ��ȭ�Ǿ� �ִٸ� 0���� ��Ȱ��ȭ ��Ű�� 1���� Ȱ��ȭ��Ų��.
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
                //������ ���������� �ƹ� ȿ�� ����
            }
        });
        this.btnPrev.onClick.AddListener(() =>  //���� ��ư�� ������
        {
            if (this.imgs[0].gameObject.activeSelf)
            {
                //ù ���������� �ƹ� ȿ�� ����
            }
            else if (this.imgs[1].gameObject.activeSelf)
            {
                //1�� �̹����� Ȱ��ȭ�Ǿ� �ִٸ� 1���� ��Ȱ��ȭ ��Ű�� 0���� Ȱ��ȭ��Ų��.
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
            //�ݱ� ��ư ������ ��Ȱ��ȭ
            this.gameObject.SetActive(false);
        });

    }
}
