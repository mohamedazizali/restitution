using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private const string BASE_URL = "http://localhost:5000/auth";

    public IEnumerator Signup(string name, string password, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(BASE_URL + "/signup", form))
        {
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Signup successful: " + www.downloadHandler.text);
                callback?.Invoke("Signup successful");
            }
            else
            {
                Debug.LogError("Signup failed: " + www.error);
                Debug.LogError("Response: " + www.downloadHandler.text);
                callback?.Invoke("Signup failed: " + www.error);
            }
        }
    }

    public IEnumerator Login(string name, string password, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(BASE_URL + "/login", form))
        {
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Login successful: " + www.downloadHandler.text);
                callback?.Invoke("Login successful: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Login failed: " + www.error);
                Debug.LogError("Response: " + www.downloadHandler.text);
                callback?.Invoke("Login failed: " + www.error);
            }
        }
    }

    public IEnumerator TestConnection(System.Action<string> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(BASE_URL.Replace("/auth", "/test")))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Test successful: " + www.downloadHandler.text);
                callback?.Invoke("Test successful: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Test failed: " + www.error);
                Debug.LogError("Response: " + www.downloadHandler.text);
                callback?.Invoke("Test failed: " + www.error);
            }
        }
    }

    void Start()
    {
        StartCoroutine(TestConnection((result) => {
            Debug.Log(result);
        }));
    }
}
