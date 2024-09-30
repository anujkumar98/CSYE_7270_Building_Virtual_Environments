using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float speed = 1.0f; 
    public float range = 2.0f;

    private float startX;
    private bool movingRight = true;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        Vector3 position = transform.position;

        if (movingRight)
        {
            position.x += speed * Time.deltaTime;
            if (position.x >= startX + range)
            {
                movingRight = false;
            }
        }
        else
        {
            position.x -= speed * Time.deltaTime;
            if (position.x <= startX - range)
            {
                movingRight = true;
            }
        }

        transform.position = position;
    }
}