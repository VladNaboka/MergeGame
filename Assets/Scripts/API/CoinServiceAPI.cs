using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;

public class CoinServiceAPI : MonoBehaviour
{
    private string baseUrl = "https://zoofarmbackend.onrender.com";

    [Serializable]
    private class ClaimCoinsResponse
    {
        public int claimed_coins;
    }

    public IEnumerator UpdateUserCoins(int userId, int coins, Action<bool> callback)
    {
        string url = $"{baseUrl}/updateUserCoins";
        string jsonData = $"{{\"user_id\":{userId},\"coins\":{coins}}}";
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Монеты успешно обновлены.");
            callback?.Invoke(true);
        }
        else
        {
            Debug.LogError("Ошибка обновления монет: " + request.error);
            callback?.Invoke(false);
        }
    }


    public IEnumerator ClaimOwnerCoins(int clanId, Action<int> callback)
    {
        string url = $"{baseUrl}/claimOwnerCoins";
        string jsonData = $"{{\"clan_id\":{clanId}}}";
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            ClaimCoinsResponse response = JsonUtility.FromJson<ClaimCoinsResponse>(request.downloadHandler.text);
            Debug.Log($"Получено {response.claimed_coins} монет.");
            callback?.Invoke(response.claimed_coins);
        }
        else
        {
            Debug.LogError("Ошибка при получении монет владельца клана: " + request.error);
            callback?.Invoke(0);
        }
    }


}
