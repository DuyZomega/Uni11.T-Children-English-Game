using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ClickStart : MonoBehaviour
{
    public GameObject Wizard = null;
    public GameObject Enemies = null;
    float time;
    public Sprite wizard_surprise;
    public float moveSpeed = 1f;
    string path;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/isFirstPlay.txt";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ChangeScene());
        }
    }
    IEnumerator ChangeScene()
    {
        if (Wizard != null)
        {
            Wizard.GetComponent<SpriteRenderer>().sprite = wizard_surprise;
            Wizard.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
            Enemies.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
            yield return new WaitForSeconds(1);
            Application.Quit();
            if (isFirstPlay(path))
            {
                SceneManager.LoadScene("ComicScene0");
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    bool isFirstPlay(string file_path)
    {
        if (!File.Exists(file_path))
        {
            StreamWriter sw = File.CreateText(file_path);
            sw.WriteLine("false");
            sw.Close();
            return true;
        }
        return false;
    }
}
