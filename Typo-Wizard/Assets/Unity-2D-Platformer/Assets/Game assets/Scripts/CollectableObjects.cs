using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObjects : MonoBehaviour
{
    private LevelManager GameLevelManager;
    public int coinValue;

    [Header("Optional GameObject to activate")]
    public GameObject prefabToActivate;  // Assign this in Inspector

    [System.Obsolete]
    void Start()
    {
        GameLevelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameLevelManager.AddCoins(coinValue);

            if (prefabToActivate != null)
            {
                prefabToActivate.SetActive(true);
            }

            Destroy(gameObject); // Destroy this collectible
        }
    }
}