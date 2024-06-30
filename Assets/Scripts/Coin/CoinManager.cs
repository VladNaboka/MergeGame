using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Text coinText;
    public int coins;

    public static CoinManager instance;
   
    private void Awake()
    {
        LoadData();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        coinText.text = coins.ToString();
    }

    public void AddCoins(int amount)
    {
        SoundManager.instance.Play("Coin");
        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
    }
    public void RemoveCoins(int amount)
    {
        coins -= amount;
        PlayerPrefs.SetInt("Coins", coins);
    }
    private void LoadData()
    {
        coins = PlayerPrefs.GetInt("Coins", 5);
    }
}
