using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 0f;

    public GameObjectScript game;

    public static bool GameIsPause = false;

    public void PlayGame(int sceneID)
    {
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

    IEnumerator LoadLevel(int levelIndex)
    {
        // Trigger the start of the transition
        transition.SetTrigger("Start");

        // Wait for the duration of the transition animation
        yield return new WaitForSeconds(transitionTime);

        // Load the new scene
        SceneManager.LoadScene(levelIndex);
        
    }
}
