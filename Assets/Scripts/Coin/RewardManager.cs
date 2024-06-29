using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public List<CoinGiver> animals;
    [SerializeField] private int timeForReward;

    private void Start()
    {
        StartCoroutine(RewardCoroutine());
    }
    private IEnumerator RewardCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeForReward);

            if(animals != null && animals.Count >= 1)
            {
                foreach(CoinGiver animal in animals)
                {
                    if(animal != null)
                    {
                        animal.GiveCoin();
                    }
                }
            }
        }
    }
}
