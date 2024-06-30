using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class BackgroundIncome : MonoBehaviour
{
    [SerializeField] private GameObject incomeScreen;
    [SerializeField] private Text incomeText;
    void Start()
    {
        if (PlayerPrefs.HasKey("LastSession"))
        {
            CountIncome();
        }
        StartCoroutine(timerSaved());
    }
    private void CountIncome()
    {
        incomeScreen.SetActive(true);
        TimeSpan ts;
        int income = PlayerPrefs.GetInt("Income");
        ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));

        int finalIncome = income * Mathf.RoundToInt((float)ts.TotalSeconds);

        CoinManager.instance.AddCoins(finalIncome);
        incomeText.text = finalIncome.ToString();

        PlayerPrefs.DeleteKey("Income");
        PlayerPrefs.DeleteKey("LastSession");
    }
    private void SaveSession()
    {
        int income = 0;
        CoinGiver[] animals = FindObjectsOfType<CoinGiver>();

        foreach (CoinGiver animal in animals)
        {
            income += animal.coinAmount;
        }

        PlayerPrefs.SetInt("Income", income);
        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
    }
    public void Close()
    {
        incomeScreen.SetActive(false);
    }
    private IEnumerator timerSaved()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            SaveSession();
        }
    }
    //private void OnApplicationQuit()
    //{
    //    SaveSession();
    //}
}
