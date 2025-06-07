using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Text = UnityEngine.UI.Text;

public class Controller : MonoBehaviour
{
    public static Controller INSTANCE;

    public TextMeshProUGUI scoreText;
    private readonly IDictionary<string, List<GameObject>> textToEnemyMappings = new Dictionary<string, List<GameObject>>();
    private int _score = 0;
    [SerializeField]
    public GameOverScript gameOverMenuUI;
    public PauseMenuScript pauseMenuUI;
    public GameObject barry;
    public AudioClip[] audioClips;
    private readonly string _baseUrl = "https://cegwebapi-bsamgfdjgqbyg2fr.eastus-01.azurewebsites.net";

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
        public int HomeworkResultId;
        public int TotalPoint;
        public int TotalCorrectAnswers;
        public string Playtime;// Format: hh:mm:ss
    }
    [Serializable]
    public class StudentProgressRequest
    {
        public int StudentId;
        public int ClassId;
        public int TotalPoint;
        public string Playtime;
    }
    void Start()
    {
        INSTANCE = this;
        int studentId = ClassList.Instance.StudentId;
        int classId = ClassList.Instance.ClassId;
        Debug.Log($"Loaded scene with ClassId: {classId}, StudentId: {studentId}");
        
    }

    // Update is called once per frame
    void Update()
    {
        var score = "<cspace=0.1em>";
        foreach (char digit in _score.ToString())
        {
            score = score + $"<sprite=\"big_{digit}\" index=0>";
        }
        scoreText.text = score;
    }

    public void Register(string text, GameObject enemy)
    {
        if (!textToEnemyMappings.ContainsKey(text))
        {
            textToEnemyMappings[text] = new List<GameObject>();
        }
        textToEnemyMappings[text].Add(enemy);
    }
    [Serializable]
    public class StudentProgressResponse
    {
        public int StudentProgressId;
        public int StudentId;
        public int ClassId;
        public int Point;
        public TimeSpan PlayTime;
        // Add more fields if needed based on the API response
    }
    [Serializable]
    public class StudentProgressResponseListWrapper
    {
        public bool status;
        public List<StudentProgressResponse> data;
    }
    [Serializable]
    public class HomeWorkResultResponse
    {
        public int HomeWorkResultId;
        public int StudentId;
        public int HomeWorkId;
        public int Point;
        public TimeSpan PlayTime;
        // Add more fields if needed based on the API response
    }
    [Serializable]
    public class HomeWorkResultListWrapper
    {
        public bool status;
        public List<HomeWorkResultResponse> data;
    }
    public bool CastSpell(string text)
    {
        text = text.Trim().ToLower(); //  add this line

        if (textToEnemyMappings.ContainsKey(text) && textToEnemyMappings[text].Any())
        {
            foreach (var enemy in textToEnemyMappings[text])
            {
                enemy.GetComponent<Enemy>().Pop();
                _score++;
            }
            textToEnemyMappings.Remove(text);
            barry.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Snap");
            return true;
        }

        return false;
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

        string url = $"{_baseUrl}/api/StudentHomework";
        Debug.Log("JSON Body: " + jsonRequestBody);
        bool success = await SendPostRequest(url, jsonRequestBody);
        Debug.Log(success ? "StudentHomework sent successfully!" : "Failed to send StudentHomework");
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
    //public async void SendHomeworkResult(int totalPoint, int totalCorrectAnswers, TimeSpan playtime)
    //{
    //    var homeworkResultRequest = new HomeworkResultRequest()
    //    {
    //        //HomeworkResultId = homeworkResultId,
    //        TotalPoint = totalPoint,
    //        TotalCorrectAnswers = totalCorrectAnswers,
    //        Playtime = playtime.ToString(@"hh\:mm\:ss")
    //    };

    //    string jsonRequestBody = JsonUtility.ToJson(homeworkResultRequest);

    //    string url = $"{_baseUrl}/api/HomeworkResult";
    //    bool success = await SendPostRequest(url, jsonRequestBody);
    //    Debug.Log(success ? "HomeworkResult sent successfully!" : "Failed to send HomeworkResult");
    //}
    [Serializable]
    public class HomeworkResultGetResponse
    {
        public bool status;
        public HomeworkResultData data;
    }
    [Serializable]
    public class HomeworkResultData
    {
        public int id;
        public int totalPoint;
        public int totalCorrectAnswers;
        public string playtime;
        public List<object> studentHomeworks;  // Or a proper class if you have it
    }
    private async Task<int> GetHomeworkResultId(int studentId, int homeworkId, int point, TimeSpan playtime)
    {
        string url = $"{_baseUrl}/api/HomeworkResult/student/{studentId}/homework/{homeworkId}";
        string response = await SendGetRequest(url);
        if (!string.IsNullOrEmpty(response))
        {
            var wrapper = JsonUtility.FromJson<HomeWorkResultListWrapper>(response);
            if (wrapper != null && wrapper.status && wrapper.data.Count > 0)
            {
                // Match based on known values
                var match = wrapper.data
                    .Where(r => r.StudentId == studentId &&
                                r.HomeWorkId == homeworkId &&
                                r.Point == point &&
                                r.PlayTime == playtime)
                    .LastOrDefault(); // Assumes latest is last in list

                if (match != null)
                {
                    return match.HomeWorkResultId;
                }
            }
        }

        Debug.LogWarning("Failed to retrieve HomeworkResult ID");
        return 0;
    }
    private async Task<int> GetStudentProgressId(int studentId, int classId, int point, TimeSpan playtime)
    {
        string url = $"{_baseUrl}/api/StudentProgress/student/{studentId}";
        string response = await SendGetRequest(url);

        if (!string.IsNullOrEmpty(response))
        {
            var wrapper = JsonUtility.FromJson<StudentProgressResponseListWrapper>(response);

            if (wrapper != null && wrapper.status && wrapper.data.Count > 0)
            {
                // Find the last progress matching all the criteria
                var match = wrapper.data
                    .Where(p => p.StudentId == studentId &&
                                p.ClassId == classId &&
                                p.Point == point &&
                                p.PlayTime == playtime)
                    .LastOrDefault();

                if (match != null)
                {
                    return match.StudentProgressId;
                }
            }
        }

        Debug.LogWarning("Failed to retrieve matching StudentProgress ID");
        return 0;
    }
    private async Task<string> SendGetRequest(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        var operation = request.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"GET request failed: {request.error}");
            return null;
        }
        else
        {
            Debug.Log($"GET request succeeded: {request.downloadHandler.text}");
            return request.downloadHandler.text;
        }
    }
    //public async void SendStudentProgress(int studentId, int classId, int totalPoint, TimeSpan playtime)
    //{
    //    var studentProgressRequest = new StudentProgressRequest()
    //    {
    //        StudentId = studentId,
    //        ClassId = classId,
    //        TotalPoint = totalPoint,
    //        Playtime = playtime.ToString(@"hh\:mm\:ss")
    //    };

    //    string jsonRequestBody = JsonUtility.ToJson(studentProgressRequest);
    //    string url = $"{_baseUrl}/api/StudentProgress";

    //    bool success = await SendPostRequest(url, jsonRequestBody);
    //    Debug.Log(success ? "StudentProgress sent successfully!" : "Failed to send StudentProgress");     
    //}
    public async Task<int> SendStudentProgress(int studentId, int classId, int totalPoint, TimeSpan playtime)
    {
        var studentProgressRequest = new StudentProgressRequest()
        {
            StudentId = ClassList.Instance.StudentId,
            ClassId = ClassList.Instance.ClassId,
            TotalPoint = totalPoint,
            Playtime = playtime.ToString(@"hh\:mm\:ss")
        };

        string jsonRequestBody = JsonUtility.ToJson(studentProgressRequest);
        string url = $"{_baseUrl}/api/StudentProgress";
        // Step 1: POST request
        bool postSuccess = await SendPostRequest(url, jsonRequestBody);
        if (!postSuccess)
        {
            Debug.LogError("Failed to create StudentProgress.");
            return 0;
        }
        //string getUrl = $"{_baseUrl}/api/StudentProgress/student/{studentId}";
        //string getResponse = await SendGetRequest(getUrl);

        //if (!string.IsNullOrEmpty(getResponse))
        //{
        //    var getResult = JsonUtility.FromJson<StudentProgressResponseListWrapper>(getResponse);
        //    if (getResult != null && getResult.status && getResult.data.Count > 0)
        //    {
        //        int latestId = getResult.data.Last().StudentProgressId;
        //        Debug.Log($"Received StudentProgress ID: {latestId}");
        //        return latestId;
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Student progress get call succeeded but status is false or no data returned.");
        //    }
        //}
        //else
        //{
        //    Debug.LogError("StudentProgress GET response was empty.");
        //}

        return 0; // failed to get ID
    }
    public async Task<int> SendHomeworkResult(int totalPoint, int totalCorrectAnswers, TimeSpan playtime)
    {
        var homeworkResultRequest = new HomeworkResultRequest()
        {
            TotalPoint = totalPoint,
            TotalCorrectAnswers = totalCorrectAnswers,
            Playtime = playtime.ToString(@"hh\:mm\:ss")
        };

        string jsonRequestBody = JsonUtility.ToJson(homeworkResultRequest);
        string url = $"{_baseUrl}/api/HomeworkResult";

        string response = await SendPostRequestWithResponse(url, jsonRequestBody);

        if (!string.IsNullOrEmpty(response))
        {
            ResponseWithId result = JsonUtility.FromJson<ResponseWithId>(response);
            if (result != null && result.status)
            {
                Debug.Log("HomeworkResultId received: " + result.data);
                return result.data;
            }
        }

        return 0; // failed to get ID
    }
    [Serializable]
    private class ResponseWithId
    {
        public bool status;
        public int data;
    }
    //public void End()
    //{
    //    // Save best score
    //    if (gameOverMenuUI != null)
    //    {
    //        gameOverMenuUI.Setup(_score);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("gameOverMenuUI is null when trying to call Setup in End()");
    //    }

    //    // Dummy values  replace with real ones dynamically if needed
    //    int homeworkId = 1;
    //    int studentProgressId = 1;
    //    int homeworkResultId = 1;
    //    int studentId = ClassList.Instance.StudentId;
    //    int classId = ClassList.Instance.ClassId;

    //    int point = _score;
    //    int totalCorrectAnswers = _score;
    //    TimeSpan playtime = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
    //    string status = "Submitted";

    //    // Log all values for debugging
    //    Debug.Log("<color=yellow>=== END GAME DATA DEBUG ===</color>");
    //    Debug.Log($"Score / Point: {point}");
    //    Debug.Log($"Playtime: {playtime.ToString(@"hh\:mm\:ss")}");
    //    Debug.Log($"Status: {status}");
    //    Debug.Log($"Correct Answers: {totalCorrectAnswers}");

    //    Debug.Log("<color=cyan>--- StudentHomework Data ---</color>");
    //    Debug.Log($"HomeworkId: {homeworkId}");
    //    Debug.Log($"StudentId: {studentId}");
    //    Debug.Log($"ClassId: {classId}");
    //    Debug.Log("<color=cyan>--- StudentProgress Data ---</color>");


    //    // Call API methods with debug info already inside each
    //    SendStudentHomework(homeworkId, studentProgressId, homeworkResultId, point, playtime, status, totalCorrectAnswers);
    //    SendHomeworkResult(point, totalCorrectAnswers, playtime);
    //    SendStudentProgress(studentId, classId, point, playtime);


    //    // End game visuals
    //    StartCoroutine(GameOver());
    //    gameOverMenuUI.Setup(_score);
    //}
    public void End()
    {
        if (gameOverMenuUI != null)
        {
            gameOverMenuUI.Setup(_score);
        }
        else
        {
            Debug.LogWarning("gameOverMenuUI is null when trying to call Setup in End()");
        }
        gameOverMenuUI?.Setup(_score);
        _ = HandleEndAsync(); // Fire and forget async method
    }

    private async Task HandleEndAsync()
    {
        int studentId = ClassList.Instance.StudentId;
        int classId = ClassList.Instance.ClassId;
        int homeworkId = 1;
        int studentProgressId = 1;
        int homeworkResultId = 1;
        int point = _score;
        int totalCorrectAnswers = _score;
        TimeSpan playtime = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        string status = "Submitted";

        await SendHomeworkResult(point, totalCorrectAnswers, playtime);
        await SendStudentProgress(studentId, classId, point, playtime);

        //// Now get the actual IDs via GET
        //int homeworkResultId = await GetHomeworkResultId(studentId, homeworkId, point, playtime);
        //Debug.LogWarning($"{homeworkResultId}");
        //int studentProgressId = await GetStudentProgressId(studentId, classId, point, playtime);
        //Debug.LogWarning($"{studentId}");
            await SendStudentHomework(homeworkId, studentProgressId, homeworkResultId, point, playtime, status, totalCorrectAnswers);

        StartCoroutine(GameOver());
        gameOverMenuUI.Setup(_score);
    }

    IEnumerator GameOver()
    {
        var dieGo = barry.transform.GetChild(2).gameObject;
        dieGo.SetActive(true);
        barry.transform.GetChild(0).gameObject.SetActive(false);
        barry.transform.GetChild(1).gameObject.SetActive(false);
        dieGo.GetComponent<Animator>().SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }

    //public void Restart()
    //{
    //    SceneManager.LoadScene("SampleScene");
    //}
}