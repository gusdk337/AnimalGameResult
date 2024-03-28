using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    public TMP_Text txtScore;
    public TMP_Text txtBestScore;
    public Button btnAd;
    public AdsManager adsManager;

    private void Awake()
    {
        this.btnAd.enabled = true;
    }

    private void Start()
    {
        this.adsManager = FindObjectOfType<AdsManager>();

        this.btnAd.onClick.AddListener(() =>
        {
            adsManager.LoadLoadInterstitialAd();    //±§∞Ì »£√‚
            this.btnAd.enabled = false;
        });
    }
}
