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
        this.nextAnimal = Instantiate(animals[randomIndex], parent.transform);  //ǳ�� �̹����� ���� ���� ����(������ ���� ����)
    }

    private void Update()
    {
        if(nextAnimal == null) //���� ������ ������ �ٽ� ���� ���� 
        {
            GameObject parent = GameObject.Find("imgBallon");

            int randomIndex = Random.Range(0, animals.Length);
            this.nextAnimal = Instantiate(animals[randomIndex], parent.transform);
        }
    }
}
