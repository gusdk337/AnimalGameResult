using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    //���� �߰�
    public Action<int> addScore;

    public Action changeScene;

    public Action ShowToturial;

    public EventManager()
    {

    }

}
