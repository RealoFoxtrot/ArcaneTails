using UnityEngine;
using System.Collections;

public class PinballMovement : MonoBehaviour
{

    public float speed = 2f;
    public bool beenhit = false;
    public float boomForce = 10f;
    public float boomRadius = 1f;

    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    private Vector3 movement;
    private Vector3 crossHair;
    private Vector3 pointAtCrossHair;
    private Vector3 boomPosition;
    private float boomMultiplier;



    Vector3 explosionPos;
    Collider[] colliders;
    GameObject[] Enemies;


    private float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();


         explosionPos = transform.position;




         colliders = Physics.OverlapSphere(explosionPos, 6);
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        //boomPosition = GameObject.Find("AttackerEnemy").transform.position;
        boomMultiplier = boomForce * 1000;



        /* if (GameObject.Find("Attacker").GetComponent<AttackingPoint>().inRange == true && GameObject.Find("Attacker").GetComponent<AttackingPoint>().blastSpellFired == true)
         {
             beenhit = true;
         }

       */



        // swap between world control and physics pinball
        if (beenhit == true)
        {
            timer += 1.0f * Time.deltaTime;
            pinballhit();
        }
        else
        {
            //pinballcontrol(horizontal, vertical);
            timer = 0;
            physicsPinballControl();
        }

        if (timer >= 2)
        {

            beenhit = false;
        }

    }

    //Disable the controls and start spinning the player
    void pinballhit()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.AddExplosionForce(boomMultiplier, boomPosition, boomRadius, 0.1f);
    }

    //World Co-Ordinate Controlled Movement
    void pinballcontrol(float horizontal, float vertical)
    {
        // Set the movement vector based on the axis input.
        movement.Set(horizontal, 0f, vertical);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        rb.MovePosition(transform.position + movement);

        //turn the player towards the crosshair
        turning();
    }

    //Tony's physics based movement converted to Elina's game
    void physicsPinballControl()
    {
        if (horizontal > 0 || horizontal < 0)
        {

            rb.AddForce(horizontal * speed * rb.mass * 500 * Time.deltaTime, 0, 0);

        }

        if (vertical > 0 || vertical < 0)
        {


            rb.AddForce(0, 0, vertical * speed * rb.mass * 500 * Time.deltaTime);

        }

        turning();
    }


    void turning()
    {
        //get the location of the crosshair from the script in the camera
        crossHair = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraAim>().hitTransform;

        //create a vector between the crosshair and the player
        pointAtCrossHair = crossHair - transform.position;
        //pointAtCrossHair.y = 0.3f;

        Quaternion turnPlayer = Quaternion.LookRotation(pointAtCrossHair);

        rb.MoveRotation(turnPlayer);
    }

}