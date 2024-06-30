using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGiver : MonoBehaviour
{
    public int coinAmount;
    [SerializeField] private TextSpawner textSpawner;
    private RewardManager rewardManager;

    private void Awake()
    {
        rewardManager = FindObjectOfType<RewardManager>();
    }
    private void Start()
    {
        PlayerStats.instance.CurrentCapacity += 1;
        rewardManager.animals.Add(this);
    }

    public void GiveCoin()
    {
        CoinManager.instance.AddCoins(coinAmount);
        textSpawner.SpawnText(coinAmount);
    }
    public void OnDestroy()
    {
        PlayerStats.instance.CurrentCapacity -= 1;
        rewardManager.animals.Remove(this);
    }
}
