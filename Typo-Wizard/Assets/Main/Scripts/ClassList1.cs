using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using Newtonsoft.Json;

public class ClassList2 : MonoBehaviour
{
    public static ClassList2 Instance;

    [Header("UI Elements")]
    public GameObject classListPanel;
    public GameObject classItemPrefab;
    public Transform classListContent;
    public TMP_Text statusText;
    public TMP_Text classNameText;
    private string classSceneName = "Intro Video";
    private bool _classLocked;
    // Base URL for API calls - should match AccountManager
    private readonly string _baseUrl = "https://cegwebapi-bsamgfdjgqbyg2fr.eastus-01.azurewebsites.net";

    // Class data
    private List<ClassData> _enrolledClasses = new List<ClassData>();

    [Serializable]
    public class ClassData
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public int MinimumStudents { get; set; }
        public int MaximumStudents { get; set; }
        public int NumberOfStudents { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EnrollmentFee { get; set; }
        public string Status { get; set; }
        public List<EnrollData> Enrolls { get; set; }
    }
    public class EnrollData
    {
        public int EnrollId { get; set; }
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EnrolledDate { get; set; }
        public string Status { get; set; }
    }
    public class ApiResponse
    {
        public bool status;
        public string successMessage;
        public List<ClassData> data;
    }
    //[Serializable]
    //public class ApiListResponse<T>
    //{
    //    public bool status;
    //    public string successMessage;
    //    public List<T> data;
    //}
    [Serializable]
    public class ClassDataArrayWrapper
    {
        public bool status;
        public string successMessage;
        public ClassData[] data;
    }
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
        // Only fetch classes if the user is logged in
        if (PlayerPrefs.HasKey("IsLoggedIn") && PlayerPrefs.GetInt("IsLoggedIn") == 1)
        {
            _classLocked = false;
            var button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);
            StartCoroutine(FetchEnrolledClasses());
            if (_classLocked)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
    }
    // Fetches classes the student is already enrolled in
    public IEnumerator FetchEnrolledClasses()
    {
        if (statusText != null)
            statusText.text = "Loading classes...";

        // Get account ID from account manager
        string accountId = AccountManager.Instance._user.UserId;
        Debug.Log($"Fetching enrolled classes for Account ID: {accountId}");  // Log account ID

        string url = $"{_baseUrl}/api/Class/student/account/{accountId}";
        Debug.Log($"Request URL: {url}");  // Log full API URL

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "application/json");

        if (!string.IsNullOrEmpty(AccountManager.Instance.GetAccessToken()))
        {
            request.SetRequestHeader("Authorization", "Bearer " + AccountManager.Instance.GetAccessToken());
        }

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failed to fetch enrolled classes: " + request.error);
            if (statusText != null)
                statusText.text = "Failed to load classes. Please try again.";
        }
        else
        {
            Debug.Log("Class list fetch succeeded");
            var response = request.downloadHandler.text;
            Debug.Log($"API Response: {response}");
            try
            {
                //// Fix JSON format if needed for JsonUtility
                //if (!response.StartsWith("{"))
                //{
                //    response = "{\"status\":true,\"successMessage\":\"Success\",\"data\":" + response + "}";
                //    var apiResponse = JsonUtility.FromJson<ClassDataArrayWrapper>(response);
                //    _enrolledClasses = new List<ClassData>(apiResponse.data);
                //}
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);
                //ApiListResponse<ClassData> apiResponse = JsonUtility.FromJson<ApiListResponse<ClassData>>(response);
                //Debug.Log($"API Response: {apiResponse}");
                if (apiResponse.status)
                {
                    _enrolledClasses = new List<ClassData>(apiResponse.data);
                    foreach (var c in _enrolledClasses)
                    {
                        Debug.Log(JsonConvert.SerializeObject(c, Formatting.Indented));
                    }
                    Debug.Log($"Fetched {_enrolledClasses.Count} enrolled classes");
                    if (statusText != null)
                    {
                        statusText.text = $"Found {_enrolledClasses.Count} classes";
                    }
                    UpdateButtonInfo();
                }
                else
                {
                    if (statusText != null)
                        statusText.text = "No classes found";
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error parsing class data: {e.Message}");
                if (statusText != null)
                    statusText.text = "Error loading classes";
            }
        }
    }
    // Populate UI with class list
    //private void UpdateButtonInfo()
    //{

    //    //bool hasOngoingClass = false;
    //    //int ongoingCount = 0;

    //        // Only show classes that are Ongoing
    //        //if (classData.status.Trim().ToLower() == "ongoing")
    //        //{
    //        //    hasOngoingClass = true;
    //        //    ongoingCount++;
    //        for(int i = 0; i < _enrolledClasses.Count; i++) {
    //        var classData = _enrolledClasses[i];
    //        // Instantiate prefab under the content container
    //        GameObject newButton = Instantiate(classItemPrefab, classListContent);

    //            // Set text
    //            TMP_Text classText = newButton.GetComponentInChildren<TMP_Text>();
    //            if (classText != null)
    //            {
    //                classText.text = classData.ClassName;
    //            }
    //            else
    //            {
    //                Debug.LogWarning("TMP_Text not found in prefab!");
    //            }

    //            // Button behavior
    //            Button btn = newButton.GetComponent<Button>();
    //            if (btn != null)
    //            {
    //                int classId = classData.ClassId;
    //                btn.onClick.AddListener(() =>
    //                {
    //                    Debug.Log($"Clicked class: {classData.ClassName} (ID: {classId})");
    //                    SceneManager.LoadScene(classSceneName);
    //                });
    //            }
    //        }
    //}
    //// Lock class if none ongoing
    //_classLocked = !hasOngoingClass;

    //if (statusText != null)
    //    statusText.text = hasOngoingClass
    //        ? $"{ongoingCount} ongoing class(es) loaded."
    //        : "No ongoing classes found.";

    // Refresh class list manually
    private void UpdateButtonInfo()
    {
        // Optional: Clear previous buttons if any
        foreach (Transform child in classListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var classData in _enrolledClasses)
        {
            // Instantiate prefab under the content container
            GameObject classItem = Instantiate(classItemPrefab, classListContent);

            // Set class name text
            TMP_Text classText = classItem.GetComponentInChildren<TMP_Text>();
            if (classText != null)
            {
                classText.text = classData.ClassName;
            }
            else
            {
                Debug.LogWarning("TMP_Text not found in prefab!");
            }

            // Button behavior
            Button btn = classItem.GetComponent<Button>();
            if (btn != null)
            {
                int classId = classData.ClassId;
                string className = classData.ClassName; // Store locally to avoid closure issue

                btn.onClick.AddListener(() =>
                {
                    Debug.Log($"Clicked class: {className} (ID: {classId})");
                    SceneManager.LoadScene(classSceneName);
                });
            }
        }
    }
    public void RefreshClassList()
    {
        StartCoroutine(FetchEnrolledClasses());
    }
    private void OnButtonClick()
    {
        SceneManager.LoadScene(classSceneName);
    }
    // Return to the main menu or dashboard
    public void ReturnToMainMenu()
    {
        // Implementation for navigation back to main menu
        if (classListPanel != null)
        {
            classListPanel.SetActive(false);
        }
    }
}