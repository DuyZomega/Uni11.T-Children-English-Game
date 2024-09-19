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

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;

    public String baseUrl = @"";

    public TMP_InputField loginNameInputField;
    public TMP_InputField loginPasswordInputField;
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

    private IEnumerator Login(string _email, string _password)
    {
        string url = "https://your-api-url.com/login"; // Your API endpoint for login

        // Create form data
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", _email),
            new MultipartFormDataSection("password", _password)
        };

        UnityWebRequest request = UnityWebRequest.Post(url, formData);

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

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1f;
            GameIsPause = false;
            Cursor.lockState = CursorLockMode.None;

            // Load game data and scoreboard
            StartCoroutine(LoadGameData());
            StartCoroutine(LoadScoreBoard());

            // After loading data, update UI
            UpdateUIAfterLogin();
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