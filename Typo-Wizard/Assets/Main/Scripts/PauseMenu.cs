using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPause = false;

    [SerializeField]
    private GameObject

            pauseMenuUI,
            optionMenu;

    [SerializeField]




    private GameObject mainMenu;


    // Update is called once per frame

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;

    }

    public void Quit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Return()
    {
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    public void Options()
    {
        if (GameIsPause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            GameIsPause = true;

        }
        else
        {
            Resume();
        }
    }
}
