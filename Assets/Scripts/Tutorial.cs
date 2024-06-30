using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    [SerializeField] private GameObject[] tutorialSlides;
    public int index;

    private void Start()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("TutorialDone"))
        {
            StartTutorial();
            PlayerPrefs.Save();
        }
    }
    private void StartTutorial()
    {
        index = 0;
        tutorialSlides[index].SetActive(true);
    }
    public void NextTutorial()
    {
        index += 1;
        Debug.Log("Next tutorial" + index);
        tutorialSlides[index - 1].SetActive(false);

        if (index != tutorialSlides.Length)
            tutorialSlides[index].SetActive(true);

        if(index == 2)
        {
            StartCoroutine(TutorialCoroutine());
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
