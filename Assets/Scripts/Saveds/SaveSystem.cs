using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private SavedData[] savedObjects;
    [SerializeField] private DragAndDropObjects[] dragObjects;

    private void Awake()
    {
        dragObjects = FindObjectsOfType<DragAndDropObjects>();
    }
    private void Start()
    {
        var all = Resources.LoadAll<AnimalScript>("Animals");
        foreach (var obj in all)
        {
            Debug.Log(obj.id);
            Debug.Log(obj.prefab);
            Instantiate(obj.prefab);
        }
        savedObjects = new SavedData[dragObjects.Length];

        StartCoroutine(SaveDataCoroutine());
    }

    private IEnumerator SaveDataCoroutine()
    {
        while (true)
        {
            SaveObjects();
            yield return new WaitForSeconds(3f);
        }
    }

    private void SaveObjects()
    {
        for (int i = 0; i < dragObjects.Length; i++)
        {
            SaveObject(dragObjects[i], i);
            Debug.Log(savedObjects[i]);
        }
    }

    private void SaveObject(DragAndDropObjects obj, int index)
    {
        if (obj != null && index >= 0 && index < savedObjects.Length)
        {
            savedObjects[index] = new SavedData(obj, obj.transform.position);
        }
    }
}
