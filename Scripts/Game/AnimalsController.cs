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

    private static bool hasExecuted = false;    //�� ���� ����ǵ���

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
        if (this.gameObject.tag == collision.gameObject.tag && !hasExecuted)    //����� �� 2������ �ƴ� 1������ ��������(������ �������� �� ��ũ��Ʈ�� ���� ����)
        {
            int animalIndex = -1;

            switch (this.gameObject.tag)
            {
                case "Egg": //Egg���� ������ animalIndex�� 1�� �����ϰ� �÷��� ������ 1������ ����
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

            if (animalIndex != -1 && animalIndex != 100)    //������ ����(�������� ���� ���� �����̱� ����)
            {
                GameObject parent = GameObject.Find("Animals");
                GameObject newAnimal = Instantiate(animals[animalIndex], this.transform.position, Quaternion.identity);
                newAnimal.transform.parent = parent.transform;
                Rigidbody2D newAnimalRb = newAnimal.GetComponent<Rigidbody2D>();

                if (newAnimal.CompareTag("Dog") || newAnimal.CompareTag("Pig") || newAnimal.CompareTag("Penguin") || newAnimal.CompareTag("Bear") || newAnimal.CompareTag("Reindeer"))
                {
                    newAnimalRb.gravityScale = 1;   //�� �ܰ� �������� �߷��� ���� ����
                }
                else if(newAnimal.CompareTag("Egg") || newAnimal.CompareTag("Chicken") || newAnimal.CompareTag("Frog") || newAnimal.CompareTag("Rabbit") || newAnimal.CompareTag("Cat"))
                {
                    newAnimalRb.gravityScale = 10;  //�Ʒ� �ܰ� �������� �߷��� ���� ����
                }
                this.gameObject.SetActive(false);
                collision.gameObject.SetActive(false);
                SoundManager.PlaySFX("Pop");
                EventManager.instance.addScore(this.plusScore); //�ش� ���� �߰�
                hasExecuted = true;
            }
            else if(animalIndex == 100)
            {
                Debug.Log("�����ܳ��� �ε���");
            }
        }
        else
        {
            hasExecuted = false;
        }
    }
}
