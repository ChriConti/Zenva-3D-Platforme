using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;




public class PlayerController : MonoBehaviour
{
    public HudManager hud;
    public AudioSource coinAudioSource;
    public float walkSpeed = 8f;
    public float jumpSpeed = 7f;

    //to keep our rigid body
    Rigidbody rb;

    //to keeo the collider object
    Collider coll;

    //flag to keep track of whether a jump started
    bool pressedJump = false;

    //use this for initialization
    void Start()
    {
        //get the rigid body component for later use 
        rb = GetComponent<Rigidbody>();

        //get player collider
        coll = GetComponent<Collider>();

        //it is not be in the tutorial i had to add by myself
        coinAudioSource = GetComponent<AudioSource>();

        hud.Refresh();
    }

    void Update()
    {
        //handle player walking
        WalkHandler();

        //handle player jumping
        JumpHandler();

        hud.Refresh();

    }
    //check whether the player can jump and make it jump
    void JumpHandler()
    {
        //jump axis
        float jAxis = Input.GetAxis("Jump");

        //is grounded
        bool isGrounded = CheckGrounded();

        if (jAxis > 0f)
        {

            //make sure we've not already jumped on this key press
            if (!pressedJump && isGrounded)
            {
                //we are jumping on the current key press
                pressedJump = true;

                //jumping vector 
                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);

                //make the player jump by adding velocity
                rb.linearVelocity = jumpVector + rb.linearVelocity;

            }
        }
        else
        {
            //update flag so it can jump again if we press the jump key
            pressedJump = false;
        }
    }

    //check if the object is grounded
    bool CheckGrounded()
    {

        //object size in x
        float sizeX = coll.bounds.size.x;
        float sizeZ = coll.bounds.size.z;
        float sizeY = coll.bounds.size.y;

        //posing of the 4 bottom corners of the game object
        //we add 0.01 in Y so that there is some distance between the point and the floor
        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
        Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);
        Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);

        //send a short ray down the cube on all 4 corners to detected ground
        bool grounded1 = Physics.Raycast(corner1, new Vector3(0, -1, 0), 0.01f);
        bool grounded2 = Physics.Raycast(corner2, new Vector3(0, -1, 0), 0.01f);
        bool grounded3 = Physics.Raycast(corner3, new Vector3(0, -1, 0), 0.01f);
        bool grounded4 = Physics.Raycast(corner4, new Vector3(0, -1, 0), 0.01f);

        //if any corner is grounded, the object is grounded
        return (grounded1 || grounded2 || grounded3 || grounded4);
    }
    //make the player walk according to user input
    void WalkHandler()
    {
        //set x and z velocity to zero
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);

        //distance (speed = distance / time --> distance = speed * time)
        float distance = walkSpeed * Time.deltaTime;

        //input on x ("Horizontal")
        float hAxis = Input.GetAxis("Horizontal");

        print("Horizontal axis");
        print(hAxis);

        //input on z ("Vertical")
        float vAxis = Input.GetAxis("Vertical");

        print("Vertical axis");
        print(vAxis);

        //movement vector
        Vector3 movement = new Vector3(hAxis * distance, 0f, vAxis * distance);

        //current position
        Vector3 currPosition = transform.position;

        //new position
        Vector3 newPosition = currPosition + movement;

        //move the rigid body 
        rb.MovePosition(newPosition);
    }

    

    void OnTriggerEnter(Collider collider) {
        
        //check if we ran into a coin
        if(collider.gameObject.tag == "Coin")
        {
            print("Grabbing coin..");

            //Increase score
            GameManager.instance.IncreaseScore();


            hud.Refresh();

            //play coin collection sound
            coinAudioSource.Play();

            //destory coin
            Destroy(collider.gameObject);


        }
        else if (collider.gameObject.tag == "Enemy")
        {
            //game over
            print("game over");
        }
        else if (collider.gameObject.tag == "Goal")
        {
            print("next level");

        }

    }
}