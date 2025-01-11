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
    [System.Serializable]
    public class ApiResponse
    {
        public bool status;
        public string successMessage;
        public UserData data;
    }

    [System.Serializable]
    public class UserData
    {
        public string accountId;
        public string userName;
        public string roleName;
        public string accessToken;
        public string imagePath;
        public string status;
    }
    [Header("Login")]
    public TMP_InputField nameLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public static bool GameIsPause = false;

    [Header("UserData")]
    public UserObject _user;
    public UserObject userRole;

    [Header("Scoreboard")]
    public static List<UserObject> _scoreboard;

    [Header("GameData")]
    public static Dictionary<string, List<LevelObject>> _gameData = new();


    public GameObject canvasToActivate;
    public GameObject LoginScreen;
    public static int count = 0;
    public static string welcomeName = "";
    private const string loginKey = "IsLoggedIn";
    private const string usernameKey = "Username";

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
        // Check if a user is already logged in and load the appropriate UI
        if (PlayerPrefs.HasKey("IsLoggedIn") && PlayerPrefs.GetInt("IsLoggedIn") == 1)
        {
            SetUIForRole();
        }
        else
        {
            checkActive();
        }
    }
private void SetUIForRole()
    {
        LoginScreen.SetActive(false);
        canvasToActivate.SetActive(true);
    }
    private void checkActive()
    {
        // Check login status from PlayerPrefs
        if (PlayerPrefs.GetInt(loginKey, 0) == 1)
        {
            // User is already logged in, show game UI
            if (canvasToActivate != null)
            {
                canvasToActivate.SetActive(true);
            }
            if (LoginScreen != null)
            {
                LoginScreen.SetActive(false);
            }

            var username = PlayerPrefs.GetString(usernameKey, "Player");
            welcomeName = $"Welcome {username}";

            var usernameTextObj = canvasToActivate.transform.Find("UsernameText");
            if (usernameTextObj != null)
            {
                var usernameText = usernameTextObj.GetComponent<TMP_Text>();
                if (usernameText != null)
                {
                    usernameText.text = welcomeName;
                }
            }
        }
        else
        {
            // User is not logged in, show login UI
            if (LoginScreen != null)
            {
                LoginScreen.SetActive(true);
            }
            if (canvasToActivate != null)
            {
                canvasToActivate.SetActive(false);
            }
        }

        count++;
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

            var response = request.downloadHandler.text;
            Debug.Log($"API Response: {response}");
            // Deserialize the response to retrieve user data including the role
            var apiResponse = JsonUtility.FromJson<ApiResponse>(response);

            if (apiResponse.status)
            {
                // Extract and validate the user role
                string roleName = apiResponse.data.roleName.Trim().ToLower();
                if (roleName != "student")
                {
                    Debug.LogWarning("Access denied. Only students can log in.");
                    warningLoginText.text = "Access Denied: Only students can log in.";
                    yield break;
                }

                // Store user details
                _user = new UserObject
                {
                    UserId = apiResponse.data.accountId,
                    Username = apiResponse.data.userName,
                    RoleName = apiResponse.data.roleName
                };

                // Save login state
                PlayerPrefs.SetInt("IsLoggedIn", 1);
                PlayerPrefs.SetString(usernameKey, _user.Username);
                PlayerPrefs.Save();

                // Set up the UI for the logged-in user
                SetUIForRole();
                var usernameTextObj = canvasToActivate.transform.Find("UsernameText");
                if (usernameTextObj != null)
                {
                    Debug.Log("UsernameText found.");

                    // Get the text component and set the welcome message
                    var usernameText = usernameTextObj.GetComponent<TMP_Text>();
                    if (usernameText != null)
                    {
                        welcomeName = $"Welcome {_name}";
                        usernameText.text = welcomeName;
                        Debug.Log("UsernameText updated: " + welcomeName);
                    }
                    else
                    {
                        Debug.LogError("UsernameText component not found on UsernameText object.");
                    }
                }
                else
                {
                    Debug.LogError("UsernameText not found in canvasToActivate.");
                }

                // Further actions like loading game data and scoreboard
                //StartCoroutine(LoadGameData());
                //StartCoroutine(LoadScoreBoard());

            }
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
        string url = $"https://localhost:7143/api/users/{_user.UserId}/initialize"; // API endpoint for user initialization

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
        string url = $"https://localhost:7143/api/{_user.UserId}/scores"; // API endpoint for sending score

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
        string url = "https://localhost:7143/api/scoreboard"; // API endpoint for scoreboard data

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
        string url = "https://localhost:7143/api/game-data"; // API endpoint for game data

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
        PlayerPrefs.SetInt(loginKey, 0);
        PlayerPrefs.DeleteKey(usernameKey);
        PlayerPrefs.Save();
        Debug.Log("User logged out successfully");
    }

}