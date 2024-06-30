using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private int basePrice = 100;
    [SerializeField] private Text costText;
    private int currentPrice;
    public int addedCapacity = 0;

    private void Start()
    {
        addedCapacity = PlayerPrefs.GetInt("AddedCapacity", 0);
        currentPrice = PlayerPrefs.GetInt("CapacityPrice", basePrice);
        StartCoroutine(timerSaved());
    }
    private void Update()
    {
        costText.text = currentPrice.ToString();
    }
    public void BuyCapacity()
    {
        if(CoinManager.instance.coins >= currentPrice)
        {
            SoundManager.instance.Play("Buy");
            CoinManager.instance.RemoveCoins(currentPrice);
            addedCapacity += 1;
            currentPrice *= 3;

            PlayerPrefs.SetInt("AddedCapacity", addedCapacity);
            PlayerPrefs.SetInt("CapacityPrice", currentPrice);
        }
    }
    private IEnumerator timerSaved()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            PlayerPrefs.SetInt("AddedCapacity", addedCapacity);
            PlayerPrefs.SetInt("CapacityPrice", currentPrice);
            PlayerPrefs.Save();
        }
    }
    //private void OnApplicationQuit()
    //{
    //    PlayerPrefs.SetInt("AddedCapacity", addedCapacity);
    //    PlayerPrefs.SetInt("CapacityPrice", currentPrice);
    //    PlayerPrefs.Save();
    //}
}
