using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class SessionList : MonoBehaviour
{
    public static SessionList Instance;

    [Header("UI Elements")]
    public GameObject sessionListPanel;
    public GameObject sessionItemPrefab;
    public Transform sessionListContent;
    public TMP_Text statusText;

    // Base URL for API calls - should match AccountManager
    private readonly string _baseUrl = "https://cegwebapi-bsamgfdjgqbyg2fr.eastus-01.azurewebsites.net";

    // Session data
    private List<SessionData> _availableSessions = new List<SessionData>();

    [Serializable]
    public class SessionData
    {
        public int sessionId;
        public int courseId;
        public string title;
        public string description;
        public int? hours;
        public int? sessionNumber;
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
        // Only fetch sessions if the user is logged in
        if (PlayerPrefs.HasKey("IsLoggedIn") && PlayerPrefs.GetInt("IsLoggedIn") == 1)
        {
            StartCoroutine(FetchAvailableSessions());
        }
    }

    // Fetches all available sessions
    public IEnumerator FetchAvailableSessions()
    {
        if (statusText != null)
            statusText.text = "Loading sessions...";

        string url = $"{_baseUrl}/api/Session/Available";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "application/json");

        // Add authorization if needed
        if (!string.IsNullOrEmpty(AccountManager.Instance.GetAccessToken()))
        {
            request.SetRequestHeader("Authorization", "Bearer " + AccountManager.Instance.GetAccessToken());
        }

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failed to fetch available sessions: " + request.error);
            if (statusText != null)
                statusText.text = "Failed to load sessions. Please try again.";
        }
        else
        {
            string response = request.downloadHandler.text;
            try
            {
                // Fix JSON format if needed for JsonUtility
                if (!response.StartsWith("{"))
                {
                    response = "{\"status\":true,\"successMessage\":\"Success\",\"data\":" + response + "}";
                }

                ApiListResponse<SessionData> apiResponse = JsonUtility.FromJson<ApiListResponse<SessionData>>(response);

                if (apiResponse.status)
                {
                    _availableSessions = apiResponse.data;
                    Debug.Log($"Fetched {_availableSessions.Count} available sessions");
                    PopulateSessionList();

                    if (statusText != null)
                        statusText.text = $"Found {_availableSessions.Count} sessions";
                }
                else
                {
                    if (statusText != null)
                        statusText.text = "No sessions found";
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error parsing session data: {e.Message}");
                if (statusText != null)
                    statusText.text = "Error loading sessions";
            }
        }
    }

    // Populate UI with session list
    private void PopulateSessionList()
    {
        // Clear existing items
        foreach (Transform child in sessionListContent)
        {
            Destroy(child.gameObject);
        }

        // Add new items
        foreach (var sessionData in _availableSessions)
        {
            GameObject sessionItem = Instantiate(sessionItemPrefab, sessionListContent);

            // Set session title
            Transform titleTransform = sessionItem.transform.Find("SessionTitle");
            if (titleTransform != null)
            {
                TMP_Text titleText = titleTransform.GetComponent<TMP_Text>();
                if (titleText != null)
                {
                    titleText.text = sessionData.title;
                }
            }

            // Set course ID
            Transform courseIdTransform = sessionItem.transform.Find("CourseId");
            if (courseIdTransform != null)
            {
                TMP_Text courseIdText = courseIdTransform.GetComponent<TMP_Text>();
                if (courseIdText != null)
                {
                    courseIdText.text = $"Course ID: {sessionData.courseId}";
                }
            }

            // Set session number
            Transform sessionNumberTransform = sessionItem.transform.Find("SessionNumber");
            if (sessionNumberTransform != null)
            {
                TMP_Text sessionNumberText = sessionNumberTransform.GetComponent<TMP_Text>();
                if (sessionNumberText != null)
                {
                    sessionNumberText.text = $"Session: {sessionData.sessionNumber ?? 0}";
                }
            }

            // Set hours
            Transform hoursTransform = sessionItem.transform.Find("Hours");
            if (hoursTransform != null)
            {
                TMP_Text hoursText = hoursTransform.GetComponent<TMP_Text>();
                if (hoursText != null)
                {
                    hoursText.text = $"Hours: {sessionData.hours ?? 0}";
                }
            }

            // Set description
            Transform descTransform = sessionItem.transform.Find("Description");
            if (descTransform != null)
            {
                TMP_Text descText = descTransform.GetComponent<TMP_Text>();
                if (descText != null)
                {
                    // Trim the description if it's too long
                    string description = sessionData.description ?? "No description available";
                    if (description.Length > 100)
                    {
                        description = description.Substring(0, 97) + "...";
                    }
                    descText.text = description;
                }
            }
        }
    }

    // Search sessions by keyword
    public void SearchSessions(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            // If search is empty, show all sessions
            PopulateSessionList();
            return;
        }

        keyword = keyword.ToLower();
        List<SessionData> filteredSessions = new List<SessionData>();

        foreach (var session in _availableSessions)
        {
            if (session.title.ToLower().Contains(keyword) ||
                (session.description?.ToLower().Contains(keyword) ?? false))
            {
                filteredSessions.Add(session);
            }
        }

        // Clear existing items
        foreach (Transform child in sessionListContent)
        {
            Destroy(child.gameObject);
        }

        // Add filtered items
        foreach (var sessionData in filteredSessions)
        {
            GameObject sessionItem = Instantiate(sessionItemPrefab, sessionListContent);

            // Set session title
            Transform titleTransform = sessionItem.transform.Find("SessionTitle");
            if (titleTransform != null)
            {
                TMP_Text titleText = titleTransform.GetComponent<TMP_Text>();
                if (titleText != null)
                {
                    titleText.text = sessionData.title;
                }
            }

            // Set course ID
            Transform courseIdTransform = sessionItem.transform.Find("CourseId");
            if (courseIdTransform != null)
            {
                TMP_Text courseIdText = courseIdTransform.GetComponent<TMP_Text>();
                if (courseIdText != null)
                {
                    courseIdText.text = $"Course ID: {sessionData.courseId}";
                }
            }

            // Set session number
            Transform sessionNumberTransform = sessionItem.transform.Find("SessionNumber");
            if (sessionNumberTransform != null)
            {
                TMP_Text sessionNumberText = sessionNumberTransform.GetComponent<TMP_Text>();
                if (sessionNumberText != null)
                {
                    sessionNumberText.text = $"Session: {sessionData.sessionNumber ?? 0}";
                }
            }

            // Set hours
            Transform hoursTransform = sessionItem.transform.Find("Hours");
            if (hoursTransform != null)
            {
                TMP_Text hoursText = hoursTransform.GetComponent<TMP_Text>();
                if (hoursText != null)
                {
                    hoursText.text = $"Hours: {sessionData.hours ?? 0}";
                }
            }

            // Set description
            Transform descTransform = sessionItem.transform.Find("Description");
            if (descTransform != null)
            {
                TMP_Text descText = descTransform.GetComponent<TMP_Text>();
                if (descText != null)
                {
                    string description = sessionData.description ?? "No description available";
                    if (description.Length > 100)
                    {
                        description = description.Substring(0, 97) + "...";
                    }
                    descText.text = description;
                }
            }
        }

        if (statusText != null)
            statusText.text = $"Found {filteredSessions.Count} matching sessions";
    }

    // Handle session search input
    public void OnSearchInputChanged(TMP_InputField searchInput)
    {
        if (searchInput != null)
        {
            SearchSessions(searchInput.text);
        }
    }

    // Refresh session list manually
    public void RefreshSessionList()
    {
        StartCoroutine(FetchAvailableSessions());
    }

    // Filter sessions by course ID
    public void FilterByCourseId(int courseId)
    {
        List<SessionData> filteredSessions = new List<SessionData>();

        foreach (var session in _availableSessions)
        {
            if (session.courseId == courseId)
            {
                filteredSessions.Add(session);
            }
        }

        // Clear and repopulate the list with filtered sessions
        foreach (Transform child in sessionListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var sessionData in filteredSessions)
        {
            GameObject sessionItem = Instantiate(sessionItemPrefab, sessionListContent);

            // Same UI setup as in PopulateSessionList
            Transform titleTransform = sessionItem.transform.Find("SessionTitle");
            if (titleTransform != null)
            {
                TMP_Text titleText = titleTransform.GetComponent<TMP_Text>();
                if (titleText != null)
                {
                    titleText.text = sessionData.title;
                }
            }

            // Set course ID
            Transform courseIdTransform = sessionItem.transform.Find("CourseId");
            if (courseIdTransform != null)
            {
                TMP_Text courseIdText = courseIdTransform.GetComponent<TMP_Text>();
                if (courseIdText != null)
                {
                    courseIdText.text = $"Course ID: {sessionData.courseId}";
                }
            }

            // Set session number
            Transform sessionNumberTransform = sessionItem.transform.Find("SessionNumber");
            if (sessionNumberTransform != null)
            {
                TMP_Text sessionNumberText = sessionNumberTransform.GetComponent<TMP_Text>();
                if (sessionNumberText != null)
                {
                    sessionNumberText.text = $"Session: {sessionData.sessionNumber ?? 0}";
                }
            }

            // Set hours
            Transform hoursTransform = sessionItem.transform.Find("Hours");
            if (hoursTransform != null)
            {
                TMP_Text hoursText = hoursTransform.GetComponent<TMP_Text>();
                if (hoursText != null)
                {
                    hoursText.text = $"Hours: {sessionData.hours ?? 0}";
                }
            }

            // Set description
            Transform descTransform = sessionItem.transform.Find("Description");
            if (descTransform != null)
            {
                TMP_Text descText = descTransform.GetComponent<TMP_Text>();
                if (descText != null)
                {
                    string description = sessionData.description ?? "No description available";
                    if (description.Length > 100)
                    {
                        description = description.Substring(0, 97) + "...";
                    }
                    descText.text = description;
                }
            }
        }

        if (statusText != null)
            statusText.text = $"Found {filteredSessions.Count} sessions for Course ID {courseId}";
    }

    // Return to the main menu or dashboard
    public void ReturnToMainMenu()
    {
        // Implementation for navigation back to main menu
        if (sessionListPanel != null)
        {
            sessionListPanel.SetActive(false);
        }
    }
}