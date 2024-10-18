using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public BestScore BestScore;
    public TextMeshProUGUI scoreText;

    public static bool GameIsPause = false;

    public void Setup(int _score)
    {
        Time.timeScale = 1f;
        gameObject.SetActive(true);
        
        var score = "<cspace=0.1em>";
        foreach (char digit in _score.ToString())
        {
            score = score + $"<sprite=\"font_score_large_{digit}\" index=0>";
        }
        pointsText.text = score.ToString();
        scoreText.text = Controller.INSTANCE.GetComponent<BestScore>().GetBestScore().ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
        GameIsPause = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        GameIsPause = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
