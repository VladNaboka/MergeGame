using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;

public class ClanService : MonoBehaviour
{

    #region Классы для десериализации JSON

    [Serializable]
    public class Clan
    {
        public int id;
        public string name;
        public int owner_id;
        public int members_count;
        public float income_bonus;
        public int owner_coins;
    }

    [Serializable]
    public class ClanList
    {
        public Clan[] clans;
    }

    [Serializable]
    private class CreateClanResponse
    {
        public int clan_id;
    }

    #endregion

    private string baseUrl = "https://zoofarmbackend.onrender.com";

    public void CreateClan(string name, int ownerId, Action<int> onSuccess, Action<string> onError)
    {
        string jsonData = $"{{\"name\":\"{name}\",\"owner_id\":{ownerId}}}";
        StartCoroutine(SendPostRequest($"{baseUrl}/create", jsonData, (response) =>
        {
            var json = JsonUtility.FromJson<CreateClanResponse>(response);
            onSuccess?.Invoke(json.clan_id);
        }, onError));
    }

    public void GetClanByID(int clanId, Action<Clan> onSuccess, Action<string> onError)
    {
        StartCoroutine(SendGetRequest($"{baseUrl}/{clanId}", (response) =>
        {
            var clan = JsonUtility.FromJson<Clan>(response);
            onSuccess?.Invoke(clan);
        }, onError));
    }

    public void GetAllClans(Action<Clan[]> onSuccess, Action<string> onError)
    {
        StartCoroutine(SendGetRequest($"{baseUrl}s", (response) =>
        {
            var clans = JsonUtility.FromJson<ClanList>(response);
            onSuccess?.Invoke(clans.clans);
        }, onError));
    }

    public void DeleteClan(int clanId, Action onSuccess, Action<string> onError)
    {
        StartCoroutine(SendDeleteRequest($"{baseUrl}/delete/{clanId}", onSuccess, onError));
    }

    public void JoinClan(int userId, int clanId, Action onSuccess, Action<string> onError)
    {
        string jsonData = $"{{\"user_id\":{userId},\"clan_id\":{clanId}}}";
        StartCoroutine(SendPostRequest($"{baseUrl}/join", jsonData, (response) => onSuccess?.Invoke(), onError));
    }

    public void LeaveClan(int userId, Action onSuccess, Action<string> onError)
    {
        string jsonData = $"{{\"user_id\":{userId}}}";
        StartCoroutine(SendPostRequest($"{baseUrl}/leave", jsonData, (response) => onSuccess?.Invoke(), onError));
    }

    #region Вспомогательные методы для запросов

    private IEnumerator SendPostRequest(string url, string jsonData, Action<string> onSuccess, Action<string> onError)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke(request.downloadHandler.text);
            else
                onError?.Invoke($"Ошибка: {request.error}");
        }
    }

    private IEnumerator SendGetRequest(string url, Action<string> onSuccess, Action<string> onError)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke(request.downloadHandler.text);
            else
                onError?.Invoke($"Ошибка: {request.error}");
        }
    }

    private IEnumerator SendDeleteRequest(string url, Action onSuccess, Action<string> onError)
    {
        using (UnityWebRequest request = UnityWebRequest.Delete(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke();
            else
                onError?.Invoke($"Ошибка: {request.error}");
        }
    }

    #endregion


}
