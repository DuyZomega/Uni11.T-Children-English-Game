using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public static Controller INSTANCE;

    public TextMeshProUGUI scoreText;
    private readonly IDictionary<string, List<GameObject>> textToEnemyMappings = new Dictionary<string, List<GameObject>>();
    private int _score = 0;
    [SerializeField]
    public GameOverScript gameOverMenuUI;
    public GameObject barry;
    public AudioClip[] audioClips;

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

    public bool CastSpell(string text)
    {
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

    public void End()
    {
        GetComponent<BestScore>().SetBestScore(_score);
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

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
