using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject animal;
    [Header("Positions")]
    [SerializeField] private Vector3 leftUpperCorner;
    [SerializeField] private Vector3 rightDownCorner;
    [Header("Price")]
    [SerializeField] private Text priceText;
    [SerializeField] private int startPrice = 5;
    [SerializeField] private float increment = 1.5f;
    private int currentPrice;

    private bool tutorialDone = false;

    private void Start()
    {
        currentPrice = PlayerPrefs.GetInt("SpawnPrice", startPrice);
    }
    private void Update()
    {
        priceText.text = currentPrice.ToString();
    }

    public void BuyAnimal()
    {
        if(PlayerStats.instance.CurrentCapacity < PlayerStats.instance.MaxAnimalCapacity)
        {
            if(CoinManager.instance.coins >= currentPrice)
            {
                SoundManager.instance.Play("Buy");

                if (!PlayerPrefs.HasKey("TutorialDone") && !tutorialDone)
                {
                    tutorialDone = true;
                    Tutorial.instance.NextTutorial();
                }

                CoinManager.instance.RemoveCoins(currentPrice);
                currentPrice *= Mathf.RoundToInt(Mathf.Pow(currentPrice, increment));
                PlayerPrefs.SetInt("SpawnPrice", currentPrice);
                PlayerPrefs.Save();

                SpawnAnimal();

            }
        }
    }
    private void SpawnAnimal()
    {
        float randX = Random.Range(leftUpperCorner.x, rightDownCorner.x);
        float randZ = Random.Range(rightDownCorner.z, leftUpperCorner.z);

        Vector3 position = new Vector3(randX, leftUpperCorner.y, randZ);
        Instantiate(animal, position, Quaternion.identity);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("SpawnPrice", currentPrice);
        PlayerPrefs.Save();
    }
}
