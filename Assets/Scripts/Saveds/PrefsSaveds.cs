using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsSaveds : MonoBehaviour
{
    private const string KeyListKey = "KeyList";

    public void SaveAnimalData(string key, string value)
    {
        PlayerPrefs.SetString(key, value);

        List<string> keys = GetKeyList();
        if (!keys.Contains(key))
        {
            keys.Add(key);
            SaveKeyList(keys);
        }

        PlayerPrefs.Save();
    }

    public List<string> GetKeyList()
    {
        string keyListJson = PlayerPrefs.GetString(KeyListKey, "{}");
        KeyList keyList = JsonUtility.FromJson<KeyList>(keyListJson);
        return keyList?.keys ?? new List<string>();
    }

    private void SaveKeyList(List<string> keys)
    {
        KeyList keyList = new KeyList { keys = keys };
        string keyListJson = JsonUtility.ToJson(keyList);
        PlayerPrefs.SetString(KeyListKey, keyListJson);
    }

    [System.Serializable]
    private class KeyList
    {
        public List<string> keys = new List<string>();
    }
}
