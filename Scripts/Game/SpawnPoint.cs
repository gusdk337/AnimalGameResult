using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] animals;
    public Transform spawnPoint;

    //public GameObject currentAnimal;
    public GameObject newAnimal;
    public GameObject nextAnimal;

    private bool isOut;
    //public bool isGameOver;

    private void Update()
    {
        //this.currentAnimal = this.newAnimal;
    }
    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == this.newAnimal)
        {
            //this.isOut = false;
            //StopCoroutine(this.CheckTime());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.gameObject == this.newAnimal)
        {
            //this.isOut = true;
            //StartCoroutine(this.CheckTime());
            
            //Invoke("SpawnNewAnimal", 0.1f);
        }

    }

    private IEnumerator CheckTime()
    {
        float timer = 0f;

        while (this.isOut)
        {
            timer += Time.deltaTime;

            if(timer >= 0.3f)   //바로바로 생기면 오류 발생
            {
                this.isOut = false;

                GameObject parent = GameObject.Find("Animals");

                switch (nextAnimal.tag)
                {
                    case "Egg":
                        this.newAnimal = Instantiate(animals[0], new Vector3(this.spawnPoint.position.x, this.spawnPoint.position.y, this.spawnPoint.position.z), Quaternion.identity);
                        break;
                    case "Chicken":
                        this.newAnimal = Instantiate(animals[1], new Vector3(this.spawnPoint.position.x, this.spawnPoint.position.y, this.spawnPoint.position.z), Quaternion.identity);
                        break;
                    case "Frog":
                        this.newAnimal = Instantiate(animals[2], new Vector3(this.spawnPoint.position.x, this.spawnPoint.position.y, this.spawnPoint.position.z), Quaternion.identity);
                        break;
                    case "Rabbit":
                        this.newAnimal = Instantiate(animals[3], new Vector3(this.spawnPoint.position.x, this.spawnPoint.position.y, this.spawnPoint.position.z), Quaternion.identity);
                        break;
                    case "Cat":
                        this.newAnimal = Instantiate(animals[4], new Vector3(this.spawnPoint.position.x, this.spawnPoint.position.y, this.spawnPoint.position.z), Quaternion.identity);
                        break;
                }
                this.newAnimal.SetActive(true);
                newAnimal.transform.parent = parent.transform;

                break;
            }

            yield return null;
        }
    }

    public void SpawnNewAnimal()
    {
        GameObject parent = GameObject.Find("Animals");

        switch (nextAnimal.tag)
        {
            case "Egg":
                this.newAnimal = Instantiate(animals[0], new Vector3(this.spawnPoint.position.x, this.spawnPoint.position.y, this.spawnPoint.position.z), Quaternion.identity);
                break;
            case "Chicken":
                this.newAnimal = Instantiate(animals[1], new Vector3(this.spawnPoint.position.x, this.spawnPoint.position.y, this.spawnPoint.position.z), Quaternion.identity);
                break;
            case "Frog":
                this.newAnimal = Instantiate(animals[2], new Vector3(this.spawnPoint.position.x, this.spawnPoint.position.y, this.spawnPoint.position.z), Quaternion.identity);
                break;
            case "Rabbit":
                this.newAnimal = Instantiate(animals[3], new Vector3(this.spawnPoint.position.x, this.spawnPoint.position.y, this.spawnPoint.position.z), Quaternion.identity);
                break;
            case "Cat":
                this.newAnimal = Instantiate(animals[4], new Vector3(this.spawnPoint.position.x, this.spawnPoint.position.y, this.spawnPoint.position.z), Quaternion.identity);
                break;
        }
        this.newAnimal.SetActive(true);
        newAnimal.transform.parent = parent.transform;

    }


    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0);
    //    if (colliders.Length >= 2)
    //    {
    //        Debug.Log("겹침");
    //        Time.timeScale = 0;
    //    }

    //}
}
