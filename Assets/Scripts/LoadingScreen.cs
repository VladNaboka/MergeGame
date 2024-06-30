using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider loadingBar;

    private void Start()
    {
        loadingBar.value = 0;
        loadingBar.maxValue = 25;
        StartCoroutine(LoadingCoroutine());
    }
    private IEnumerator LoadingCoroutine()
    {
        while(loadingBar.value < loadingBar.maxValue)
        {
            yield return new WaitForSeconds(0.01f);
            loadingBar.value += 0.1f;
        }
        SceneManager.LoadScene("GameScene");
    }
}
