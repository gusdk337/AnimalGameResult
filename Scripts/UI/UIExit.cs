using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExit : MonoBehaviour
{
    public Button btnYes;
    public Button btnNo;

    void Start()
    {
        this.btnYes.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        this.btnNo.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
