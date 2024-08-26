using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlicker : MonoBehaviour
{
    public GameObject startObject;
    double time;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 0.5)
        {
            active = !active;
            startObject.SetActive(active);
            time = 0;
        }
    }
}
