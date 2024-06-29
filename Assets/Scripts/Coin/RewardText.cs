using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardText : MonoBehaviour
{
    [SerializeField] private Text coinText;

    public void SetText(int coinAmount)
    {
        coinText.text = coinAmount.ToString();
    }
    public void DeleteObject()
    {
        Destroy(gameObject);
    }
}
