using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

[Serializable]
public class User
{
    public int id;
    public string token;
    public string name;
    public int coins;
    public int coins_per_hour;
    public int? clan_id;
}

[Serializable]
public class UsersResponse
{
    public User[] users;
}


[Serializable]
public class UpdateUserRequest
{
    public int id;
    public int coins;
    public int coins_per_hour;
}

public class UserService : MonoBehaviour
{
    private const string BaseUrl = "https://zoofarmbackend.onrender.com";
    private const string TokenKey = "UserToken";

    public static User CurrentUser { get; private set; }

    public static bool IsLoggedIn => !string.IsNullOrEmpty(GetSavedToken());

    private void Start()
    {
        string savedToken = GetSavedToken();
        if (!string.IsNullOrEmpty(savedToken))
        {
            StartCoroutine(Login(savedToken));
        }
    }

    public static void SaveToken(string token)
    {
        PlayerPrefs.SetString(TokenKey, token);
        PlayerPrefs.Save();
    }

    public static string GetSavedToken()
    {
        return PlayerPrefs.GetString(TokenKey, "");
    }

    public static void Logout()
    {
        PlayerPrefs.DeleteKey(TokenKey);
        CurrentUser = null;
    }

    public IEnumerator Register(string token, string name, Action<bool> callback)
    {
        string url = $"{BaseUrl}/register";
        var requestData = JsonUtility.ToJson(new { token, name });

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                CurrentUser = JsonUtility.FromJson<User>(request.downloadHandler.text);
                SaveToken(CurrentUser.token);
                callback(true);
            }
            else
            {
                Debug.LogError("Ошибка регистрации: " + request.error);
                callback(false);
            }
        }
    }

    public IEnumerator Login(string token, Action<bool> callback = null)
    {
        string url = $"{BaseUrl}/login/{token}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                CurrentUser = JsonUtility.FromJson<User>(request.downloadHandler.text);
                SaveToken(CurrentUser.token);
                callback?.Invoke(true);
            }
            else
            {
                Debug.LogError("Ошибка входа: " + request.error);
                callback?.Invoke(false);
            }
        }
    }

    public IEnumerator GetUserById(int id, Action<User> callback)
    {
        string url = $"{BaseUrl}/user/{id}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                User user = JsonUtility.FromJson<User>(request.downloadHandler.text);
                callback?.Invoke(user);
            }
            else
            {
                Debug.LogError("Ошибка получения пользователя: " + request.error);
                callback?.Invoke(null);
            }
        }
    }

    public IEnumerator UpdateUser(int coins, int coinsPerHour, Action<bool> callback)
    {
        string url = $"{BaseUrl}/user/update";
        var requestData = JsonUtility.ToJson(new UpdateUserRequest
        {
            id = CurrentUser.id,
            coins = coins,
            coins_per_hour = coinsPerHour
        });

        using (UnityWebRequest request = new UnityWebRequest(url, "PUT"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                callback(true);
            }
            else
            {
                Debug.LogError("Ошибка обновления данных: " + request.error);
                callback(false);
            }
        }
    }

    public IEnumerator GetAllUsers(Action<User[]> callback)
    {
        string url = $"{BaseUrl}/users";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = "{\"users\":" + request.downloadHandler.text + "}";
                UsersResponse response = JsonUtility.FromJson<UsersResponse>(json);
                callback?.Invoke(response.users);
            }
            else
            {
                Debug.LogError("Ошибка получения всех пользователей: " + request.error);
                callback?.Invoke(null);
            }
        }
    }

    public IEnumerator GetUsersByClanID(int clanId, Action<User[]> callback)
    {
        string url = $"{BaseUrl}/users/clan/{clanId}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                UsersResponse response = JsonUtility.FromJson<UsersResponse>("{\"users\":" + request.downloadHandler.text + "}");
                callback?.Invoke(response.users);
            }
            else
            {
                Debug.LogError("Ошибка получения пользователей клана: " + request.error);
                callback?.Invoke(null);
            }
        }
    }

    public IEnumerator GetLeaderboard(int topN, Action<User[]> callback)
    {
        yield return StartCoroutine(GetAllUsers(users =>
        {
            if (users != null)
            {
                Array.Sort(users, (a, b) => b.coins.CompareTo(a.coins));

                User[] leaderboard = users.Length > topN ? users[..topN] : users;

                callback?.Invoke(leaderboard);
            }
            else
            {
                callback?.Invoke(null);
            }
        }));
    }

}
