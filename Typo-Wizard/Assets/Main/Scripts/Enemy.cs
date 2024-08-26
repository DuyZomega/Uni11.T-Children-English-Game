using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Enemy : MonoBehaviour
{
    public GameObject ray;
    public GameObject splash;
    private bool _balloonsExploded = false;
    private Animator _lowerAnimator;
    private Animator _balloon1Animator;
    private Animator _balloon2Animator;

    

    public void Pop()
    {
        _balloonsExploded = true;
        _balloon1Animator.SetTrigger("Pop");
        _balloon2Animator.SetTrigger("Pop");
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 2, ForceMode2D.Impulse);
        //playsound when ballon pop
        FindObjectOfType<AudioManager>().PLay("BalloonPop");
        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(.25f);
        gameObject.GetComponent<Fall>().speed = 5;
        transform.GetChild(1).gameObject.SetActive(false);
        _lowerAnimator.SetTrigger("Fall");
    }

    IEnumerator Die()
    {
        Camera.main.GetComponent<Shake>().TriggerShake();
        _lowerAnimator.SetTrigger("Land");
        //playsound when enemy hit ground and screen shake
        FindObjectOfType<AudioManager>().PLay("VillianHitGround");
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
        // Increase difficulty
        var spawner = Controller.INSTANCE.gameObject.GetComponent<Spawner>();
        spawner.spawnInterval = Mathf.Max(1, spawner.spawnInterval - 0.05f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collider is not another enemy
        if (collision.gameObject.GetComponent<Enemy>() == null)
        {
            if (_balloonsExploded)
            {
                StartCoroutine(Die());
            } else
            {
                StartCoroutine(KillBarry());
            }
        }
    }

    IEnumerator KillBarry()
    {
        var audioM = FindObjectOfType<AudioManager>();
        _lowerAnimator.SetTrigger("Land");
        ray.SetActive(true);
        ray.GetComponent<Animator>().SetTrigger("Shoot");

        splash.SetActive(true);
        splash.GetComponent<Animator>().SetTrigger("Splash");
        yield return new WaitForSeconds(.3f);
        //playsound when lose game
        audioM.PLay("GameOver");
        yield return new WaitForSeconds(1.5f);
        audioM.Stop("BackGroundSound");
        Controller.INSTANCE.End();
    }


    void Start()
    {
        _lowerAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        _balloon1Animator = transform.GetChild(1).transform.GetChild(2).gameObject.GetComponent<Animator>();
        _balloon2Animator = transform.GetChild(1).transform.GetChild(3).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
