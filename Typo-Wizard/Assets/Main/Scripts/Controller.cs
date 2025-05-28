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
    void Start()
    {
        INSTANCE = this;
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
        public int student_progress_id;
        // Add more fields if needed based on the API response
    }
    [Serializable]
    public class StudentProgressResponseListWrapper
    {
        public bool status;
        public List<StudentProgressResponse> data;
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

    public async void SendStudentProgress(int studentProgressId, int totalPoint, TimeSpan playtime)
    {
        var studentProgressRequest = new StudentProgressRequest()
        {
            student_progress_id = studentProgressId,
            student_id = AccountManager.Instance.StudentId,  // Use global studentId here
            class_id = AccountManager.Instance.ClassId,      // Use global classId here
            total_point = totalPoint,
            playtime = playtime.ToString(@"hh\:mm\:ss")
        };

        string jsonRequestBody = JsonUtility.ToJson(studentProgressRequest);
        Debug.Log("Send StudentProgress: " + jsonRequestBody);

        string url = $"{_baseUrl}/api/StudentProgress/Create";
        await SendPostRequest(url, jsonRequestBody);
    }


    [Serializable]
    private class ResponseWithId
    {
        public bool status;
        public int data;
    }
    public void End()
    {
        // Save best score
        if (gameOverMenuUI != null)
        {
            gameOverMenuUI.Setup(_score);
        }
        else
        {
            Debug.LogWarning("gameOverMenuUI is null when trying to call Setup in End()");
        }

        // Dummy values – replace with real ones dynamically if needed
        int homeworkId = 1;
        int studentProgressId = 1;
        int homeworkResultId = 1;
        int studentId = 1;
        int classId = 10;

        int point = _score;
        int totalCorrectAnswers = _score;
        TimeSpan playtime = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        string status = "Submitted";

        // Log all values for debugging
        Debug.Log("<color=yellow>=== END GAME DATA DEBUG ===</color>");
        Debug.Log($"Score / Point: {point}");
        Debug.Log($"Playtime: {playtime.ToString(@"hh\:mm\:ss")}");
        Debug.Log($"Status: {status}");
        Debug.Log($"Correct Answers: {totalCorrectAnswers}");

        Debug.Log("<color=cyan>--- StudentHomework Data ---</color>");
        Debug.Log($"HomeworkId: {homeworkId}");
        Debug.Log($"StudentProgressId: {studentProgressId}");
        Debug.Log($"HomeworkResultId: {homeworkResultId}");

        Debug.Log("<color=cyan>--- HomeworkResult Data ---</color>");
        Debug.Log($"HomeworkResultId: {homeworkResultId}");

        Debug.Log("<color=cyan>--- StudentProgress Data ---</color>");
        Debug.Log($"StudentId: {studentId}");
        Debug.Log($"ClassId: {classId}");

        // Call API methods with debug info already inside each
        SendStudentHomework(homeworkId, studentProgressId, homeworkResultId, point, playtime, status, totalCorrectAnswers);
        SendHomeworkResult(homeworkResultId, point, totalCorrectAnswers, playtime);
        SendStudentProgress(studentId, classId, point, playtime);

        // End game visuals
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
