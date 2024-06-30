using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider loadingBar;

    private void Start()
    {
        loadingBar.value = 0;
        loadingBar.maxValue = 50;
        StartCoroutine(LoadingCoroutine());
    }
    private IEnumerator LoadingCoroutine()
    {
        while(loadingBar.value < loadingBar.maxValue)
        {
            yield return new WaitForSeconds(0.1f);
            loadingBar.value += 0.1f;
        }
    }
}
