using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    float speed = 0.5f;
    double time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < 1)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }
}
