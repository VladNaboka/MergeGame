using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    [SerializeField] private GameObject[] tutorialSlides;
    public int index;

    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("TutorialDone"))
        {
            StartTutorial();
        }
        else
        {
            // Туториал уже пройден, ничего не делаем
            Debug.Log("Tutorial already completed.");
        }
    }

    private void StartTutorial()
    {
        index = 0;
        tutorialSlides[index].SetActive(true);
    }

    public void NextTutorial()
    {
        tutorialSlides[index].SetActive(false);
        index += 1;
        Debug.Log("Next tutorial: " + index);

        if (index < tutorialSlides.Length)
        {
            tutorialSlides[index].SetActive(true);
            PlayerPrefs.SetInt("TutorialIndex", index);
            PlayerPrefs.Save();
        }

        if (index == 2)
        {
            StartCoroutine(TutorialCoroutine());
        }
        else if (index >= tutorialSlides.Length)
        {
            PlayerPrefs.SetInt("TutorialDone", 1);
            PlayerPrefs.Save();
        }
    }

    public IEnumerator TutorialCoroutine()
    {
        tutorialSlides[1].SetActive(false);
        tutorialSlides[2].SetActive(true);
        yield return new WaitForSeconds(4f);

        tutorialSlides[2].SetActive(false);

        PlayerPrefs.SetInt("TutorialDone", 1);
        PlayerPrefs.Save();
    }
}
