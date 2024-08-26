using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    public const string BEST_SCORE = "best_score";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetBestScore() => PlayerPrefs.GetInt(BEST_SCORE, 0);

    public void SetBestScore(int score) => PlayerPrefs.SetInt(BEST_SCORE, Mathf.Max(PlayerPrefs.GetInt(BEST_SCORE, 0), score));
}
