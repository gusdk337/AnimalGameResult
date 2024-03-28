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
