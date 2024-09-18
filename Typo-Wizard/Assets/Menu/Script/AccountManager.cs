using i5.Toolkit.Core.OpenIDConnectClient;
using i5.Toolkit.Core.ServiceCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class AccountManager : MonoBehaviour
{
    public static AccountManager instance;

    public String baseUrl = @"";

    public TMP_InputField loginNameInputField;

    public TMP_InputField loginPasswordInputField;

    [Header("UserData")]
    public UserObject _user;

    [Header("Scoreboard")]
    public static List<UserObject> _scoreboard;

    [Header("GameData")]
    public static Dictionary<string, List<LevelObject>> _gameData = new();

    public GameObject canvasToActivate;
    public GameObject LoginScreen;
    public static int count = 0;
    public static string welcomeName = "";

    //private AccountAuth auth;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        checkActive();
    }

    private void checkActive()
    {
        if (count > 0)
        {
            canvasToActivate.SetActive(true);
            LoginScreen.SetActive(false);
        }
        count++;
        if (canvasToActivate != null && _user != null)
        {
            Text usernameText = canvasToActivate.transform.Find("UsernameText").GetComponent<Text>();
            if (usernameText != null)
            {
                welcomeName = $"Welcome {_user.Username}";
                usernameText.text = welcomeName;
            }
        }
        if (canvasToActivate != null && _user == null)
        {
            Text usernameText = canvasToActivate.transform.Find("UsernameText").GetComponent<Text>();
            usernameText.text = welcomeName;
        }
    }

    private async void Login(string _email, string _password)
    {
        string url = "https://your-api-url.com/login"; // Your API endpoint for login
        var formData = new Dictionary<string, string>
        {
            { "email", _email },
            { "password", _password }
        };

        UnityWebRequest request = UnityWebRequest.Post(url, formData);
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Login Failed: " + request.error);
            warningLoginText.text = "Login Failed!";
        }
        else
        {
            Debug.Log("User logged in successfully");
            warningLoginText.text = "Logged In";

            _gameData = await LoadGameData();
            _scoreboard = await LoadScoreBoard();
            _user = LoadUserData(_scoreboard);

            UpdateUIAfterLogin(); // Update UI with the username
        }
    }

    private UserObject LoadUserData(List<UserObject> scoreboard)
    {
        try
        {
            var user = scoreboard.FirstOrDefault(user => user.UserId == _user.UserId);
            if (user == null)
            {
                InitialUserInfoToDatabase();
                return new UserObject
                {
                    CategoryScore = new Dictionary<string, UserScoreObject>
                    {
                        {
                            _gameData.Keys.First(),
                            new UserScoreObject { LevelScore = new Dictionary<string, double> { { "1", 0 } } }
                        }
                    },
                    UserId = _user.UserId,
                    Username = _user.Username
                };
            }
            return user;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async void InitialUserInfoToDatabase()
    {
        string url = $"https://your-api-url.com/users/{_user.UserId}/initialize"; // API endpoint for user initialization
        var formData = new Dictionary<string, object>
        {
            { "username", _user.Username },
            { "category", _gameData.Keys.First() },
            { "level", "1" },
            { "score", 0 }
        };

        UnityWebRequest request = UnityWebRequest.Put(url, formData);
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failed to initialize user info: " + request.error);
        }
    }

    public async void SendScore(string cate, string level, string score)
    {
        string url = $"https://your-api-url.com/users/{_user.UserId}/scores"; // API endpoint for sending score
        var formData = new Dictionary<string, string>
        {
            { "category", cate },
            { "level", level },
            { "score", score }
        };

        UnityWebRequest request = UnityWebRequest.Put(url, formData);
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failed to send score: " + request.error);
        }
        else
        {
            Debug.Log("Score sent successfully");
        }
    }

    public async Task<List<UserObject>> LoadScoreBoard()
    {
        string url = "https://your-api-url.com/scoreboard"; // API endpoint for scoreboard data

        UnityWebRequest request = UnityWebRequest.Get(url);
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failed to load scoreboard: " + request.error);
            return null;
        }

        // Parse the response (assuming JSON)
        var jsonData = request.downloadHandler.text;
        return JsonUtility.FromJson<List<UserObject>>(jsonData);
    }

    public async Task<Dictionary<string, List<LevelObject>>> LoadGameData()
    {
        string url = "https://your-api-url.com/game-data"; // API endpoint for game data

        UnityWebRequest request = UnityWebRequest.Get(url);
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failed to load game data: " + request.error);
            return null;
        }

        // Parse the response (assuming JSON)
        var jsonData = request.downloadHandler.text;
        return JsonUtility.FromJson<Dictionary<string, List<LevelObject>>>(jsonData);
    }

    public void Logout()
    {
        // Clear any local user session or token
        _user = null;
        _gameData = new Dictionary<string, List<LevelObject>>();
        Debug.Log("User logged out successfully");
    }

    private void UpdateUIAfterLogin()
    {
        if (canvasToActivate != null)
        {
            Text usernameText = canvasToActivate.transform.Find("UsernameText").GetComponent<Text>();
            if (usernameText != null)
            {
                welcomeName = $"Welcome {_user.Username}";
                usernameText.text = welcomeName;
            }
        }
        canvasToActivate.SetActive(true);
    }
}
}
