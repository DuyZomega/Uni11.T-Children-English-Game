using i5.Toolkit.Core.OpenIDConnectClient;
using i5.Toolkit.Core.ServiceCore;
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Text = UnityEngine.UI.Text;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;

    [System.Serializable]
    public class LoginPayload
    {
        public string username;
        public string password;
    }

    [Header("Login")]
    public TMP_InputField nameLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public static bool GameIsPause = false;

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
            if (canvasToActivate != null)
            {
                // Log to verify the canvas reference
                Debug.Log("Activating canvasToActivate.");
                canvasToActivate.SetActive(true);
            }
            else
            {
                Debug.LogError("CanvasToActivate is not assigned in the Inspector");
            }

            if (LoginScreen != null)
            {
                // Log to verify the LoginScreen reference
                Debug.Log("Deactivating LoginScreen.");
                LoginScreen.SetActive(false);
            }
            else
            {
                Debug.LogError("LoginScreen is not assigned in the Inspector");
            }
        }

        count++;

        if (canvasToActivate != null)
        {
            // Check for UsernameText
            var textObj = canvasToActivate.transform.Find("UsernameText");
            if (textObj == null)
            {
                Debug.LogWarning("UsernameText not found directly. Searching in children...");

                var usernameText = canvasToActivate.GetComponentInChildren<TMPro.TMP_Text>();
                if (usernameText != null)
                {
                    Debug.Log("Found UsernameText using GetComponentInChildren.");
                    if (_user != null)
                    {
                        welcomeName = $"Welcome {_user.Username}";
                        usernameText.text = welcomeName;
                    }
                    else
                    {
                        usernameText.text = welcomeName;
                        Debug.LogWarning("_user is null, using default welcome message");
                    }
                }
                else
                {
                    Debug.LogError("UsernameText not found in canvasToActivate or its children.");
                }
            }
            else
            {
                var tmpUsernameText = textObj.GetComponent<TMPro.TMP_Text>();
                if (tmpUsernameText != null)
                {
                    if (_user != null)
                    {
                        welcomeName = $"Welcome {_user.Username}";
                        tmpUsernameText.text = welcomeName;
                    }
                    else
                    {
                        tmpUsernameText.text = welcomeName;
                        Debug.LogWarning("_user is null, using default welcome message");
                    }
                }
                else
                {
                    var uiUsernameText = textObj.GetComponent<UnityEngine.UI.Text>();
                    if (uiUsernameText != null)
                    {
                        if (_user != null)
                        {
                            welcomeName = $"Welcome {_user.Username}";
                            uiUsernameText.text = welcomeName;
                        }
                        else
                        {
                            uiUsernameText.text = welcomeName;
                            Debug.LogWarning("_user is null, using default welcome message");
                        }
                    }
                    else
                    {
                        Debug.LogError("Neither TMP_Text nor UI Text component found on UsernameText object.");
                    }
                }
            }
        }
        else
        {
            Debug.LogError("CanvasToActivate is null");
        }
    }
    public void LoginButton()
    {
        Debug.Log("LoginButton pressed");
        StartCoroutine(Login(nameLoginField.text, passwordLoginField.text));
    }
    private IEnumerator Login(string _name, string _password)
    {
        string url = "https://localhost:7143/api/Account/Login"; // Your API endpoint for login

        // Create JSON payload
        var formData = new LoginPayload
        {
            username = _name,
            password = _password
        };

        // Convert the form data to JSON format
        string jsonData = JsonUtility.ToJson(formData);

        // Create UnityWebRequest and set it up for JSON
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Log the JSON payload
        Debug.Log("Sending JSON payload: " + jsonData);

        // Send request as coroutine
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Login Failed: " + request.error);
            warningLoginText.text = "Login Failed!";
        }
        else
        {
            Debug.Log("User logged in successfully");
            warningLoginText.text = "Logged In";

            // Further actions like loading game data and scoreboard
            StartCoroutine(LoadGameData());
            StartCoroutine(LoadScoreBoard());
            canvasToActivate.SetActive(true);
            LoginScreen.SetActive(false);
        }
    }

    private UserObject LoadUserData(List<UserObject> scoreboard)
    {
        try
        {
            // Using LINQ FirstOrDefault
            var user = scoreboard.FirstOrDefault(u => u.UserId == _user.UserId);
            if (user == null)
            {
                StartCoroutine(InitialUserInfoToDatabase());
                return new UserObject
                {
                    CategoryScore = new Dictionary<string, UserScoreObject>
                    {
                        {
                            _gameData.Keys.First(), // Use LINQ to get the first key
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

    private IEnumerator InitialUserInfoToDatabase()
    {
        string url = $"https://your-api-url.com/users/{_user.UserId}/initialize"; // API endpoint for user initialization

        // Form data dictionary
        var formData = new Dictionary<string, object>
        {
            { "username", _user.Username },
            { "category", _gameData.Keys.First() },
            { "level", "1" },
            { "score", 0 }
        };

        // Serialize to JSON and convert to byte array
        string jsonData = JsonUtility.ToJson(formData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failed to initialize user info: " + request.error);
        }
    }

    public IEnumerator SendScore(string cate, string level, string score)
    {
        string url = $"https://your-api-url.com/users/{_user.UserId}/scores"; // API endpoint for sending score

        // Form data dictionary
        var formData = new Dictionary<string, string>
        {
            { "category", cate },
            { "level", level },
            { "score", score }
        };

        // Serialize to JSON and convert to byte array
        string jsonData = JsonUtility.ToJson(formData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failed to send score: " + request.error);
        }
        else
        {
            Debug.Log("Score sent successfully");
        }
    }

    public IEnumerator LoadScoreBoard()
    {
        string url = "https://your-api-url.com/scoreboard"; // API endpoint for scoreboard data

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failed to load scoreboard: " + request.error);
        }
        else
        {
            // Parse the response (assuming JSON)
            var jsonData = request.downloadHandler.text;
            _scoreboard = JsonUtility.FromJson<List<UserObject>>(jsonData);
        }
    }

    public IEnumerator LoadGameData()
    {
        string url = "https://your-api-url.com/game-data"; // API endpoint for game data

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failed to load game data: " + request.error);
        }
        else
        {
            // Parse the response (assuming JSON)
            var jsonData = request.downloadHandler.text;
            _gameData = JsonUtility.FromJson<Dictionary<string, List<LevelObject>>>(jsonData);
        }
    }

    public void Logout()
    {
        _user = null;
        _gameData = new Dictionary<string, List<LevelObject>>();
        Debug.Log("User logged out successfully");
    }
}