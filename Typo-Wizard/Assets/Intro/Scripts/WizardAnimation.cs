using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnimation : MonoBehaviour
{
    float speed = 1;
    double time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time < 1)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }
}
