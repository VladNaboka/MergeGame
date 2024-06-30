using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private string nameScene;

    public void SceneChange()
    {
        SceneManager.LoadScene(nameScene);
    }
}
