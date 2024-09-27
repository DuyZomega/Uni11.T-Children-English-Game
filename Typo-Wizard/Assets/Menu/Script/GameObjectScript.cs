using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameObjectScript : MonoBehaviour
{
    public static bool GameIsPause = false;
    // Start is called before the first frame update
    public void Setup()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
