using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private SavedData[] savedObjects;
    [SerializeField] private DragAndDropObjects[] dragObjects;

    [SerializeField] private List<int> dragObjectsList;

    private PrefsSaveds prefsManager;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        var all = Resources.LoadAll<AnimalScript>("Animals");
        Debug.Log(all.ToString());
        prefsManager = FindObjectOfType<PrefsSaveds>();

        if (prefsManager == null)
        {
            Debug.LogError("PrefsSaveds not found!");
            return;
        }

        List<string> animalKeys = GetKeysStartingWith("animal");
        Debug.Log("Animal Keys Count: " + animalKeys.Count);

        foreach (string key in animalKeys)
        {
            string value = PlayerPrefs.GetString(key);
            dragObjectsList.Add(int.Parse(value));
        }

        if (dragObjectsList != null)
        {
            for (int i = 0; i < dragObjectsList.Count; i++)
            {
                Instantiate(all[dragObjectsList[i]-1].prefab, 
                    new Vector3(
                    UnityEngine.Random.Range(-3, 3), 
                    1, 
                    UnityEngine.Random.Range(3, 9)), 
                    Quaternion.identity);
                Debug.Log("ID "+all[dragObjectsList[i]-1].id);
            }
        }
        dragObjects = FindObjectsOfType<DragAndDropObjects>();

        savedObjects = new SavedData[dragObjects.Length];
        SaveObjects();
        //StartCoroutine(ControllAnimals());
    }

    private IEnumerator ControllAnimals()
    {
        yield return new WaitForSeconds(1f);
        SaveObjects();
        yield break;
    }

    private void SaveObjects()
    {
        for (int i = 0; i < dragObjects.Length; i++)
        {
            SaveObject(dragObjects[i], i);
            Debug.Log(dragObjects[i].id);
            prefsManager.SaveAnimalData("animal" + i, dragObjects[i].id.ToString());
        }
    }

    private void SaveObject(DragAndDropObjects obj, int index)
    {
        if (obj != null && index >= 0 && index < savedObjects.Length)
        {
            savedObjects[index] = new SavedData(obj, obj.transform.position);
        }
    }

    private List<string> GetKeysStartingWith(string prefix)
    {
        List<string> matchingKeys = new List<string>();
        List<string> keys = prefsManager.GetKeyList();

        foreach (string key in keys)
        {
            if (key.StartsWith(prefix))
            {
                matchingKeys.Add(key);
            }
        }

        return matchingKeys;
    }
    public void UpdateDragObjectsArray()
    {
        StartCoroutine(timerAddedDrag());
    }

    IEnumerator timerAddedDrag()
    {
        dragObjectsList.Clear();
        PlayerPrefs.DeleteAll();
        List<DragAndDropObjects> updatedList = new List<DragAndDropObjects>();
        yield return new WaitForSeconds(0.1f);
        foreach (var obj in FindObjectsOfType<DragAndDropObjects>())
        {
            updatedList.Add(obj);
            dragObjectsList.Add(obj.id);
            Debug.Log("awdawdawdawd" + obj.id);
        }
        dragObjects = updatedList.ToArray();
        savedObjects = new SavedData[dragObjects.Length];

        for (int i = 0; i < dragObjects.Length; i++)
        {
            SaveObject(dragObjects[i], i);
            prefsManager.SaveAnimalData("animal" + i, dragObjects[i].id.ToString());
        }
        SaveObjects();
        yield break;
    }
}
