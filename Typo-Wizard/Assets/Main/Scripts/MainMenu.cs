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
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Application = UnityEngine.Application;

public class MainMenu : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 0f;

    public float transitionTime2 = 1.5f;

    public GameObject canvasToActivate;

    public static bool GameIsPause = false;

    private void Start()
    {

        // Hide the canvas initially
        StartCoroutine(ActivateCanvasAfterDelay(transitionTime2));
    }

    public void PlayGame(int sceneID)
    {
        // Start the transition and load the new scene
        StartCoroutine(LoadLevel(sceneID));
    }

    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
        GameIsPause = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    // Coroutine to handle loading the level with a transition
    IEnumerator LoadLevel(int levelIndex)
    {
        // Trigger the start of the transition
        transition.SetTrigger("Start");


        // Wait for the duration of the transition (1.5 seconds)
        yield return new WaitForSeconds(transitionTime);

        // Load the new scene
        SceneManager.LoadScene(levelIndex);
    }

    // Coroutine to activate the canvas after a delay
    IEnumerator ActivateCanvasAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Activate the canvas
        canvasToActivate.SetActive(true);
    }
}
