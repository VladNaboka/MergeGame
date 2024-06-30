using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartObjectControl : MonoBehaviour
{
    private void Awake()
    {

        if (!PlayerPrefs.HasKey("PrefsStart"))
        {
            PlayerPrefs.SetInt("PrefsStart", 1);
            PlayerPrefs.Save();
        }
        else
        {
            if (PlayerPrefs.GetInt("PrefsStart") == 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
