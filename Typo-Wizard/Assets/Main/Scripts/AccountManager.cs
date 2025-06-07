using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Text = UnityEngine.UI.Text;
using Application = UnityEngine.Application;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;
    public int StudentId { get; set; }
    public int ClassId { get; set; }
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
    [Serializable]
    public class StudentAnswerRequest
    {
        public int student_answer_id { get; set; }
        public int game_id { get; set; }
        public int student_homework_id { get; set; }
        public string answer { get; set; } // Nullable
        public string type { get; set; } // Not Nullable

    }
    [Serializable]
    public class StudentHomeworkRequest
    {
        public int student_homework_id { get; set; }
        public int homework_id { get; set; }
        public int student_progress_id { get; set; }
        public int homework_result_id { get; set; }
        public int point { get; set; }
        public string playtime { get; set; } // In "hh:mm:ss" format
        public string status { get; set; } // E.g., "Completed", "Pending"
        public int correct_answers { get; set; }
    }
    [Serializable]
    public class HomeworkResultRequest
    {
        public int homework_result_id { get; set; }
        public int total_point { get; set; }
        public int total_correct_answers { get; set; }
        public string playtime { get; set; } // Format: hh:mm:ss
    }
    [Serializable]
    public class StudentProgressRequest
    {
        public int student_progress_id { get; set; }
        public int student_id { get; set; }
        public int class_id { get; set; }
        public int total_point { get; set; }
        public string playtime { get; set; } // Format: hh:mm:ss
    }
    [Serializable]
    public class HomeworkQuestion
    {
        public int homework_question_id;
        public string question;
        public List<HomeworkAnswer> answers;
    }

    [Serializable]
    public class HomeworkAnswer
    {
        public int homework_answer_id;
        public string answer;
        public string type;
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
    [Serializable]
    public class UserLoginRS
    {
        public int userId;
        public string username;
        public string email;
    }

    private UserLoginRS _userLoginRS;
    private string accessToken;
    [HideInInspector]
    public int currentIndex;

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
    private readonly string _baseUrl = "https://cegwebapi-bsamgfdjgqbyg2fr.eastus-01.azurewebsites.net";
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
        string url = $"{_baseUrl}/api/Account/Login"; // Your API endpoint for login

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
                // Check role
                string roleName = apiResponse.data.roleName.Trim().ToLower();
                if (roleName != "student")
                {
                    warningLoginText.text = "Access Denied: Only students can log in.";
                    yield break;
                }
                SetAccessToken(apiResponse.data.accessToken);
                // Save user info
                _user = new UserObject
                {
                    UserId = apiResponse.data.accountId, // account ID as string
                    Username = apiResponse.data.userName,
                    RoleName = apiResponse.data.roleName
                };
                PlayerPrefs.SetString("AccessToken", apiResponse.data.accessToken);
                PlayerPrefs.SetString("AccountID", _user.UserId);
                PlayerPrefs.SetString("Username", _user.Username);
                PlayerPrefs.SetString("RoleName", _user.RoleName);
                Debug.Log($"Logged in user Account ID: {_user.UserId}");
                if (int.TryParse(_user.UserId, out int parsedAccountId))
                {
                    StartCoroutine(FetchStudentId(parsedAccountId));
                }
                else
                {
                    Debug.LogError("Invalid Account ID format. Cannot parse to int.");
                }
                PlayerPrefs.SetInt("IsLoggedIn", 1);
                PlayerPrefs.SetString(usernameKey, _user.Username);
                PlayerPrefs.Save();

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
    private IEnumerator FetchStudentId(int accountId)
    {
        var task = GetStudentIdByAccountId(accountId);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result != -1)
        {
            StudentId = task.Result;
            Debug.Log("Successfully fetched StudentId: " + StudentId);
        }
        else
        {
            Debug.LogError("Failed to fetch StudentId.");
        }
    }

    [Serializable]
    private class ResponseWithId
    {
        public bool status;
        public int data;
    }

    public async Task<int> GetStudentIdByAccountId(int accountId)
    {
        string url = $"{_baseUrl}/api/Student/id/account/{accountId}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "application/json");

        var operation = request.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to get studentId: " + request.error);
            return -1;
        }

        var json = request.downloadHandler.text;
        Debug.Log("Response: " + json);

        ResponseWithId response = JsonUtility.FromJson<ResponseWithId>(json);
        return response.data;
    }

    // Utility class for parsing JSON lists
    public static class JsonUtilityHelper
{
    public static List<T> FromJsonList<T>(string json)
    {
        string wrappedJson = $"{{\"list\":{json}}}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(wrappedJson);
        return wrapper.list;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public List<T> list;
    }
}
    public string GetAccessToken()
    {
        return accessToken;
    }

    public void SetAccessToken(string token)
    {
        accessToken = token;
    }
    public void Logout()
    {
        _user = null;
        //_gameData = new Dictionary<string, List<LevelObject>>();

        PlayerPrefs.DeleteAll(); // Clears all saved PlayerPrefs data
        PlayerPrefs.Save();      // Save the changes
       Debug.Log("PlayerPrefs cleared and user logged out.");
        Application.Quit();
    }
}