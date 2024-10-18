using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI feedbackText;

    private const string signupUrl = "http://localhost:5000/auth/signup";
    private const string loginUrl = "http://localhost:5000/auth/login";

    public void OnSignupButtonClicked()
    {
        StartCoroutine(Signup(nameInput.text, passwordInput.text));
    }

    public void OnLoginButtonClicked()
    {
        StartCoroutine(Login(nameInput.text, passwordInput.text));
    }

    private IEnumerator Signup(string name, string password)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Name and password cannot be empty";
            yield break;
        }

        var signupData = new SignupData { name = name, password = password };
        string json = JsonUtility.ToJson(signupData);

        using (UnityWebRequest www = new UnityWebRequest(signupUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                feedbackText.text = $"Signup failed: {www.error}";
            }
            else
            {
                feedbackText.text = "Signup successful!";
            }
        }
    }

    private IEnumerator Login(string name, string password)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Name and password cannot be empty";
            yield break;
        }

        var loginData = new LoginData { name = name, password = password };
        string json = JsonUtility.ToJson(loginData);

        using (UnityWebRequest www = new UnityWebRequest(loginUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                feedbackText.text = $"Login failed: {www.error}";
            }
            else
            {
                feedbackText.text = "Login successful!";
                PersistentDataManager.Instance.UserName = name; // Save the user's name
                SceneManager.LoadScene("SampleScene"); // Load your game scene
            }
        }
    }

    [System.Serializable]
    public class SignupData
    {
        public string name;
        public string password;
    }

    [System.Serializable]
    public class LoginData
    {
        public string name;
        public string password;
    }
}
