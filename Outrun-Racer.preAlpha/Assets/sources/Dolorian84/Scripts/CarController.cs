using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script that control the car 
/// </summary>
public class CarController : MonoBehaviour
{
    /// <summary>
    /// Public static variables
    /// </summary>
    
    public static GameObject player;            // GameObject that represent the player
    public static GameObject currentPlatform;   // GameObject that represent the current platform that the player in on

    /// <summary>
    /// Private variables
    /// </summary>
    private Animator _anim;                     // Animator of the car
    private bool canTurn = false;               // Bool that let the car do a 90 degree turn
    Vector3 startPosition;                      // TEMPORARY


    /// <summary>
    /// Detect if there is a collision between the car and the current platform
    /// </summary>
    void OnCollisionEnter(Collision other)
    {
        currentPlatform = other.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        _anim = this.GetComponent<Animator>();
        player = this.gameObject;
        startPosition = player.transform.position;
        GenerateWorld.RunDummy();
    }

    /// <summary>
    /// Trigger that detect if the next platform is a T-section so that it can let the car do a turn
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider && GenerateWorld.lastPlatform.tag != "platformTSection")
            GenerateWorld.RunDummy();

        if (other is SphereCollider)
            canTurn = true;
    }

    /// <summary>
    /// Detect if the car has left the sphere collider of the T-section
    /// </summary>
    void OnTriggerExit(Collider other)
    {
        if (other is SphereCollider)
            canTurn = false;
    }
    /// <summary>
    /// 
    /// </summary>

    // Update is called once per frame
    void Update()
    {
        CarMouvement();
    }

    /// <summary>
    /// Function that move the car
    /// </summary>
    private void CarMouvement()
    {
        if (Input.GetKeyDown(KeyCode.E) && canTurn)
        {
            this.transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x,
                                            this.transform.position.y,
                                            startPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && canTurn)
        {
            this.transform.Rotate(Vector3.up * -90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x,
                                this.transform.position.y,
                                startPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            //this.transform.Translate(Vector3.right * _speed * Time.deltaTime);
            this.transform.Translate(-0.5f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Translate(0.5f, 0, 0);
        }
    }
}
