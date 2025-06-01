using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectClassButton : MonoBehaviour
{
    public ClassData classData;
    public static ClassData classDataClone;
    public ClassLevelData classLevelData;
    public static ClassLevelData classLevelDataClone;
    public TMPro.TMP_Text classText; // Using TextMeshPro for modern UI
    public Image progressBarFill;

    private string classSceneName = "ClassScene";
    private bool _classLocked;
    private static int flag = 0;

    private void Awake()
    {
        // Initialization if needed
    }

    void Start()
    {
        _classLocked = false;
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        UpdateButtonInfo();
        if (_classLocked)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    private void UpdateButtonInfo()
    {
        var currentProgress = -1;
        var totalTasks = 0;
        string status = "";
        string className = "";
        if (flag == 0)
        {
            classDataClone = classData;
            classLevelDataClone = classLevelData;
            flag++;
        }

        foreach (var data in classLevelData.data)
        {
            if (data.className == gameObject.name)
            {
                className = data.className;
                status = data.status;
                currentProgress = DataSaver.ReadClassProgress(gameObject.name);
                totalTasks = data.studentProgresses?.Count ?? 0;
                if (classLevelData.data[0].className == gameObject.name && currentProgress < 0)
                {
                    DataSaver.SaveClassProgress(classLevelData.data[0].className, 0);
                    currentProgress = DataSaver.ReadClassProgress(gameObject.name);
                    totalTasks = data.studentProgresses?.Count ?? 0;
                }
            }
        }

        if (currentProgress == -1 || status != "Active")
        {
            _classLocked = true;
        }

        // Display class name, student count, and progress
        classText.text = _classLocked ? string.Empty : $"{className} ({currentProgress}/{totalTasks}) - {status}";
        progressBarFill.fillAmount = (currentProgress > 0 && totalTasks > 0) ? ((float)currentProgress / (float)totalTasks) : 0f;
    }

    private void OnButtonClick()
    {
        classData.selectedClassName = gameObject.name;
        SceneManager.LoadScene(classSceneName);
    }
}