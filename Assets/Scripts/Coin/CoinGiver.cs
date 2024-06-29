using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGiver : MonoBehaviour
{
    [SerializeField] private int coinAmount;
    [SerializeField] private TextSpawner textSpawner;
    private RewardManager rewardManager;

    private void Awake()
    {
        rewardManager = FindObjectOfType<RewardManager>();
    }
    private void Start()
    {
        rewardManager.animals.Add(this);
    }

    public void GiveCoin()
    {
        CoinManager.instance.AddCoins(coinAmount);
        textSpawner.SpawnText(coinAmount);
    }
}
