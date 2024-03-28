using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLine : MonoBehaviour
{
    public bool isGameOver;
    private bool isInsideCollider = false;

    public bool isCounting;
    public float timer = 0.0f;

    private void OnTriggerEnter2D(Collider2D collision) //���ӿ������ο� ������
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Animal"))   
        {
            this.isInsideCollider = true;
            StartCoroutine(this.CheckColliderStay());   //�ð� ���� ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  //���ӿ������ο��� ������(������ �� ��������)
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

            if (timer >= 1.2f)  //1.2�� �ڿ� ���ӿ���
            {
                this.isGameOver = true;
                break;
            }
            yield return null;
        }
    }
}
