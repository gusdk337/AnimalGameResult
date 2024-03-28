using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsController : MonoBehaviour
{
    private float moveSpeed = 3f;
    private bool isLeftMoving = false;
    private bool isRightMoving = false;

    public GameObject[] animals;

    public Rigidbody2D rb;

    private static bool hasExecuted = false;    //한 번만 실행되도록

    public int bestScore;
    public int currentScore;
    public int plusScore;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isLeftMoving)
        {
            this.LeftMove();
        }
        if (isRightMoving)
        {
            this.RightMove();
        }

        if(this.gameObject.transform.position.y <= 3.8f)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Animal");
        }

    }

    public void LeftMove()
    {
        this.transform.Translate(Vector3.left * this.moveSpeed * Time.deltaTime, Space.World);
    }

    public void StartLeftMoving()
    {
        this.isLeftMoving = true;
    }

    public void StopLeftMoving()
    {
        this.isLeftMoving = false;
    }

    public void RightMove()
    {
        this.transform.Translate(Vector3.right * this.moveSpeed * Time.deltaTime, Space.World);
    }

    public void StartRightMoving()
    {
        this.isRightMoving = true;
    }

    public void StopRightMoving()
    {
        this.isRightMoving = false;
    }


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
}
