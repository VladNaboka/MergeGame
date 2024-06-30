using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private Vector3 offset = new Vector3(0, 4, 0);

    public void SpawnText(int coinAmount)
    {
        GameObject text = Instantiate(textPrefab, gameObject.transform.position + offset, Quaternion.Euler(30, 180, 0));
        text.GetComponent<RewardText>().SetText(coinAmount);
    }
}
