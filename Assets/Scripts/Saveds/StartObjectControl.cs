using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartObjectControl : MonoBehaviour
{
    private SaveSystem saveSystem;

    private void Awake()
    {
        saveSystem = FindObjectOfType<SaveSystem>();
    }

    private void Start()
    {
        Debug.Log("PREFS " + PlayerPrefs.GetInt("PrefsStart"));
        if (PlayerPrefs.GetInt("PrefsStart") == 0)
        {
            PlayerPrefs.SetInt("PrefsStart", 1);
            PlayerPrefs.Save();
            Debug.Log("PREFS 2 " + PlayerPrefs.GetInt("PrefsStart"));
        }
        if (PlayerPrefs.GetInt("PrefsStart") == 1)
        {
            //if (saveSystem != null)
            //{
            //    saveSystem.UpdateDragObjectsArray();
            //}

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
