using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    private Shop shop;

    public int MaxAnimalCapacity = 10;
    public int CurrentCapacity = 0;
    public Text capacityText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        shop = FindObjectOfType<Shop>();
    }
    private void Update()
    {
        capacityText.text = $"{CurrentCapacity}/{MaxAnimalCapacity}";
        MaxAnimalCapacity = 5 + shop.addedCapacity;
    }
}
