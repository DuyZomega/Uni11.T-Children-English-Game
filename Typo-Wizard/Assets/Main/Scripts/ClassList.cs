using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClassList : MonoBehaviour
{
    public static ClassList Instance;

    [Header("UI Elements")]
    public GameObject classListPanel;
    public GameObject classItemPrefab;
    public Transform classListContent;
    public TMP_Text statusText;

    // Base URL for API calls - should match AccountManager
    private readonly string _baseUrl = "https://cegwebapi-bsamgfdjgqbyg2fr.eastus-01.azurewebsites.net";

    // Class data
    private List<ClassData> _enrolledClasses = new List<ClassData>();

    [Serializable]
    public class ClassData
    {
        public int class_id;
        public string class_name;
        public string description;
        public string teacher_name;
        public string status; // Active, Archived, etc.
        public string start_date;
        public string end_date;
    }

    [Serializable]
    public class ApiListResponse<T>
    {
        public bool status;
        public string successMessage;
        public List<T> data;
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
            StartCoroutine(FetchEnrolledClasses());
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

        string url = $"{_baseUrl}/api/Class/Enrolled/{accountId}";
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
            string response = request.downloadHandler.text;
            try
            {
                // Fix JSON format if needed for JsonUtility
                if (!response.StartsWith("{"))
                {
                    response = "{\"status\":true,\"successMessage\":\"Success\",\"data\":" + response + "}";
                }

                ApiListResponse<ClassData> apiResponse = JsonUtility.FromJson<ApiListResponse<ClassData>>(response);

                if (apiResponse.status)
                {
                    _enrolledClasses = apiResponse.data;
                    Debug.Log($"Fetched {_enrolledClasses.Count} enrolled classes");
                    PopulateClassList();

                    if (statusText != null)
                        statusText.text = $"Found {_enrolledClasses.Count} classes";
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
    private void PopulateClassList()
    {
        // Clear existing items
        foreach (Transform child in classListContent)
        {
            Destroy(child.gameObject);
        }

        // Add new items
        foreach (var classData in _enrolledClasses)
        {
            GameObject classItem = Instantiate(classItemPrefab, classListContent);

            // Set class name
            Transform nameTransform = classItem.transform.Find("ClassName");
            if (nameTransform != null)
            {
                TMP_Text nameText = nameTransform.GetComponent<TMP_Text>();
                if (nameText != null)
                {
                    nameText.text = classData.class_name;
                }
            }

            // Set teacher name
            Transform teacherTransform = classItem.transform.Find("TeacherName");
            if (teacherTransform != null)
            {
                TMP_Text teacherText = teacherTransform.GetComponent<TMP_Text>();
                if (teacherText != null)
                {
                    teacherText.text = "Teacher: " + classData.teacher_name;
                }
            }

            // Set status
            Transform statusTransform = classItem.transform.Find("Status");
            if (statusTransform != null)
            {
                TMP_Text statusText = statusTransform.GetComponent<TMP_Text>();
                if (statusText != null)
                {
                    statusText.text = classData.status;
                }
            }

            // Set dates
            Transform dateTransform = classItem.transform.Find("Dates");
            if (dateTransform != null)
            {
                TMP_Text dateText = dateTransform.GetComponent<TMP_Text>();
                if (dateText != null)
                {
                    dateText.text = $"{classData.start_date} - {classData.end_date}";
                }
            }
        }
    }

    // Refresh class list manually
    public void RefreshClassList()
    {
        StartCoroutine(FetchEnrolledClasses());
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