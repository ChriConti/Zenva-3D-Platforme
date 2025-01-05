using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;


public class EnemyController : MonoBehaviour 
{

    //range of movement
    public float rangeY = 2f;

    //speed
    public float speed = 3f;

    //initial direction
    public float direction = 1f;

    //to keep the initial position
    Vector3 initialPosition;

    //use this for initialization
    void Start()
    {
        //initial location in Y
        initialPosition = transform.position;

    }

    //update is called once per frame
    void Update ()
    {
        float movementY = direction * speed * Time.deltaTime;

        float newY = transform.position.y + movementY;

        if (Mathf.Abs(newY - initialPosition.y) > rangeY)
        {
            direction *= -1;

        }
        else
        {
            transform.Translate(new Vector3(0, movementY, 0));
        }
    }    
}
