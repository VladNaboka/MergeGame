using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomFacts : MonoBehaviour
{
    [SerializeField] private string[] facts;
    [SerializeField] private Text factText;

    private void Start()
    {
        int index = Random.Range(0, facts.Length);

        factText.text = "Совет: " + facts[index];
    }
}
