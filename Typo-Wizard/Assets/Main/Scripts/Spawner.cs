using System.Linq;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public string[] easyWords = new string[] { "at", "ant", "eat", "but", "bat", "bat", "dog", "apple", "fish", "bear", "can", "big", "small" }; // 2 - 4 characters
    public string[] normalWords = new string[] { "scissor", "apollo", "amazing", "mommy", "daddy", "catch", "match", "bitter", "butter", "banner", "attach", "attack" }; // 5 - 8 chars
    public string[] hardWords = new string[] { "agglutination", "aurantiaceous", "blandiloquence", "blandiloquence", "cephalonomancy", "cheiloproclitic", "dactylopterous", "gastrohysterotomy" }; // > 8 chars


    public GameObject easyPrefab;
    public GameObject normalPrefab;
    public GameObject hardPrefab;

    public float spawnInterval = 2;
    private float _counter = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _counter += Time.deltaTime;

        if (_counter >= spawnInterval)
        {
            _counter = 0 + Random.Range(0, Time.deltaTime * 5); // Randomness
            Spawn();
        }
    }

    private GameObject Spawn()
    {
        var random = Random.Range(0, 10); // 0 - 5 Easy; 6 - 8 Normal; 9 Hard

        GameObject prefab;
        string[] words;
        float boardScale = 1;

        if (random < 6)
        {
            prefab = easyPrefab;
            words = easyWords;
        } else if (random < 9)
        {
            prefab = normalPrefab;
            words = normalWords;
            boardScale = 2f;
        } else
        {
            prefab = hardPrefab;
            words = hardWords;
            boardScale = 3f;
        }

        var go = Instantiate(prefab);

        // Get a random spawn position
        var board = go.transform.GetChild(1).transform.GetChild(1);
        board.transform.localScale = new Vector3(board.transform.localScale.x * boardScale, board.transform.localScale.y, 1);
        var boardSize = board.GetComponent<SpriteRenderer>().bounds.size;
        var spawnY = Screen.height + boardSize.y;
        var spawnX = Random.Range(200, Screen.width - 200);
        go.transform.position = Camera.main!.ScreenToWorldPoint(new Vector3(spawnX, spawnY, -Camera.main.transform.position.z));

        // Assign random text to object
        var textMesh = go.GetComponentInChildren<TextMeshPro>();
        textMesh.text = words[Random.Range(0, words.Count())];

        // Associate this object with the text
        Controller.INSTANCE.Register(textMesh.text, go);

        return go;
    }
}
