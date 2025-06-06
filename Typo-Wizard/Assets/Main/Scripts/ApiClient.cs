using i5.Toolkit.Core.OpenIDConnectClient;
using i5.Toolkit.Core.ServiceCore;
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

public class ApiClient : MonoBehaviour
{
    public static ApiClient Instance;

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
        public int StudentAnswerId { get; set; }
        public int GameId { get; set; }
        public int StudentHomeworkId { get; set; }
        public string Answer { get; set; } // Nullable
        public string Type { get; set; } // Not Nullable

    }
    [System.Serializable]
    public class StudentHomeworkRequest
    {
        public int homeworkId;
        public int studentProgressId;
        public int homeworkResultId;
        public int point;
        public string playtime;
        public string status;
        public int correctAnswers;
    }

    [Serializable]
    public class HomeworkResultRequest
    {
        public int HomeworkResultId { get; set; }
        public int TotalPoint { get; set; }
        public int TotalCorrectAnswers { get; set; }
        public string Playtime { get; set; } // Format: hh:mm:ss
    }
    [Serializable]
    public class StudentProgressRequest
    {
        public int StudentId;
        public int ClassId;
        public int TotalPoint;
        public string Playtime;
    }
    [Serializable]
    public class StudentProgressResponse
    {
        public int student_progress_id;
        // Add more fields if needed based on the API response
    }
    [Serializable]
    public class StudentProgressResponseListWrapper
    {
        public bool status;
        public List<StudentProgressResponse> data;
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
    public async void SendStudentAnswer(int gameId, int studentHomeworkId, string answer, string type)
    {
        // Create a new StudentAnswer object
        var studentAnswerRequest = new StudentAnswerRequest()
        {
            GameId = 1,
            StudentHomeworkId = studentHomeworkId,
            Answer = answer,
            Type = type
        };

        // Convert the object to JSON
        string jsonRequestBody = JsonUtility.ToJson(studentAnswerRequest);
        Debug.Log("Send StudentAnswer: " + jsonRequestBody);

        // Define the URL for StudentAnswer API
        string url = $"{_baseUrl}/api/StudentAnswer/Create";

        await SendPostRequest(url, jsonRequestBody);
    }
    public async Task SendStudentHomework(int homeworkId, int studentProgressId, int homeworkResultId, int point, TimeSpan playtime, string status, int correctAnswers)
    {
        StudentHomeworkRequest requestData = new StudentHomeworkRequest
        {
            homeworkId = homeworkId,
            studentProgressId = studentProgressId,
            homeworkResultId = homeworkResultId,
            point = point,
            playtime = playtime.ToString(@"hh\:mm\:ss"),
            status = status,
            correctAnswers = correctAnswers
        };

        string jsonRequestBody = JsonUtility.ToJson(requestData);
        Debug.Log("JSON Body: " + jsonRequestBody);

        string url = $"{_baseUrl}/api/StudentHomework/Create";
        Debug.Log("JSON Body: " + jsonRequestBody);
        bool success = await SendPostRequest(url, jsonRequestBody);
        Debug.Log(success ? "HomeworkResult sent successfully!" : "Failed to send HomeworkResult");
    }
    private async Task<bool> SendPostRequest(string url, string jsonRequestBody)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        request.SetRequestHeader("Content-Type", "application/json");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonRequestBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        var operation = request.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error POST request to {url} failed: {request.error}");
            return false;
        }
        else
        {
            Debug.Log($"Success POST request to {url} successful! Response: {request.downloadHandler.text}");
            return true;
        }
    }
    private async Task<string> SendPostRequestWithResponse(string url, string jsonRequestBody)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        request.SetRequestHeader("Content-Type", "application/json");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonRequestBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        Debug.Log($" Sending POST request to: {url}");
        Debug.Log($" Request Body:\n{jsonRequestBody}");

        var operation = request.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($" POST to {url} failed: {request.responseCode} {request.error}");
            Debug.LogError($" Server response: {request.downloadHandler.text}");
            return null;
        }
        else
        {
            Debug.Log($" POST to {url} succeeded!");
            Debug.Log($" Response:\n{request.downloadHandler.text}");
            return request.downloadHandler.text;
        }
    }
    public async void SendHomeworkResult(int homeworkResultId, int totalPoint, int totalCorrectAnswers, TimeSpan playtime)
    {
        var homeworkResultRequest = new HomeworkResultRequest()
        {
            HomeworkResultId = homeworkResultId,
            TotalPoint = totalPoint,
            TotalCorrectAnswers = totalCorrectAnswers,
            Playtime = playtime.ToString(@"hh\:mm\:ss")
        };

        string jsonRequestBody = JsonUtility.ToJson(homeworkResultRequest);

        string url = $"{_baseUrl}/api/HomeworkResult/Create";
        bool success = await SendPostRequest(url, jsonRequestBody);
        Debug.Log(success ? "HomeworkResult sent successfully!" : "Failed to send HomeworkResult");
    }

    public async void SendStudentProgress(int studentId, int classId, int totalPoint, TimeSpan playtime)
    {
        var studentProgressRequest = new StudentProgressRequest()
        {
            StudentId = studentId,
            ClassId = classId,
            TotalPoint = totalPoint,
            Playtime = playtime.ToString(@"hh\:mm\:ss")
        };

        string jsonRequestBody = JsonUtility.ToJson(studentProgressRequest);
        string url = $"{_baseUrl}/api/StudentProgress/Create";

        bool success = await SendPostRequest(url, jsonRequestBody);
        Debug.Log(success ? "StudentProgress sent successfully!" : "Failed to send StudentProgress");

        //// Fetch the list of progress entries
        //UnityWebRequest www = UnityWebRequest.Get(getUrl);
        //www.SetRequestHeader("Content-Type", "application/json");
        //await www.SendWebRequest();

        //if (www.result == UnityWebRequest.Result.Success)
        //{
        //    string responseText = www.downloadHandler.text;
        //    var wrapper = JsonUtility.FromJson<StudentProgressResponseListWrapper>(responseText);

        //    if (wrapper != null && wrapper.data != null && wrapper.data.Count > 0)
        //    {
        //        var latestProgress = wrapper.data.OrderByDescending(p => p.student_progress_id).First();
        //        Debug.Log("Created student_progress_id = " + latestProgress.student_progress_id);
        //        return latestProgress.student_progress_id;
        //    }
        //    else
        //    {
        //        Debug.LogError("Student progress list is empty or null");
        //        return 0;
        //    }
        //}
        //else
        //{
        //    Debug.LogError($"Failed to get student_progress_id: {www.error}");
        //    return 0;
        //}
    }


    [Serializable]
    private class ResponseWithId
    {
        public bool status;
        public int data;
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
        _gameData = new Dictionary<string, List<LevelObject>>();

        PlayerPrefs.DeleteAll(); // Clears all saved PlayerPrefs data
        PlayerPrefs.Save();      // Save the changes

        Debug.Log("PlayerPrefs cleared and user logged out.");
        Application.Quit();
    }
}