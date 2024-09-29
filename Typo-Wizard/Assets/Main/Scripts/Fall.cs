using System;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public float speed;

    void Start()
    {
        if (speed <= 0)
        {
            throw new Exception("Go up?");
        }

        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.down * 5); // initial speed;
    }

    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.down * speed);
    }
}
