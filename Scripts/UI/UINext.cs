using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINext : MonoBehaviour
{
    public GameObject[] animals;

    public GameObject nextAnimal;

    private void Awake()
    {
        GameObject parent = GameObject.Find("imgBallon");

        int randomIndex = Random.Range(0, animals.Length);
        this.nextAnimal = Instantiate(animals[randomIndex], parent.transform);  //풍선 이미지에 랜덤 동물 생성(다음에 나올 동물)
    }

    private void Update()
    {
        if(nextAnimal == null) //다음 동물이 빠지면 다시 랜덤 생성 
        {
            GameObject parent = GameObject.Find("imgBallon");

            int randomIndex = Random.Range(0, animals.Length);
            this.nextAnimal = Instantiate(animals[randomIndex], parent.transform);
        }
    }
}
