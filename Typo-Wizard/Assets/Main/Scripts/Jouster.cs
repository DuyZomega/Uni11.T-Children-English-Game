using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jouster : MonoBehaviour
{
    public bool goingRight = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > 11 || this.transform.position.x < -11)
        {
            Destroy(this.gameObject);
        }
        if (goingRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 4);
        } else
        {
            transform.Translate(Vector3.left * Time.deltaTime * 4);
        }        
    }
}
