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

    public static GameObject player;                                    // GameObject that represent the player
    public static GameObject currentPlatform;                           // GameObject that represent the current platform that the player in on

    /// <summary>
    /// Private variables
    /// </summary>
    private Joystick _leftjoystick;                                     // Joystick du jeu
    private Joystick _rightjoystick;                                     // Joystick du jeu
    private Animator _anim;                                             // Animator of the car
    private bool _canTurn = false;                                       // Bool that let the car do a 90 degree turn
    Vector3 startPosition;                                              // TEMPORARY

    [SerializeField] private float _moveForce = 5f;                     // Float qui représente la sensibilité du joystick
    [SerializeField] private float _carSpeed = 0.1f;                // Float qui représente la vitesse de la voiture
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
        _leftjoystick = GameObject.FindWithTag("LeftJoystick").GetComponent<FixedJoystick>();
        _rightjoystick = GameObject.FindWithTag("RightJoystick").GetComponent<FixedJoystick>();
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
            _canTurn = true;
    }

    /// <summary>
    /// Detect if the car has left the sphere collider of the T-section
    /// </summary>
    void OnTriggerExit(Collider other)
    {
        if (other is SphereCollider)
            _canTurn = false;
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
        // Effectuer le mouvement de la voiture
        this.transform.position += this.transform.forward * _carSpeed;
        if ((_rightjoystick.Horizontal == 1f || Input.GetKeyDown(KeyCode.E)) && _canTurn)
        {
            this.transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(this.transform.position.x,
                                            this.transform.position.y,
                                            this.transform.position.z);
            _canTurn = false;
        }
        else if ((_rightjoystick.Horizontal == -1f || Input.GetKeyDown(KeyCode.Q)) && _canTurn)
        {
            this.transform.Rotate(Vector3.up * -90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(this.transform.position.x,
                                this.transform.position.y,
                                this.transform.position.z);
            _canTurn = false;
        }
        else if (_leftjoystick.Horizontal == -1f || Input.GetKey(KeyCode.A))
        {
            // Move to the left
            this.transform.Translate(Vector3.right * -_moveForce * Time.deltaTime);
            //Instantiate(_skidPrefab, this.transform.position, this.transform.rotation);
        }
        else if (_leftjoystick.Horizontal == 1f || Input.GetKey(KeyCode.D))
        {
            // Move to the right
            this.transform.Translate(Vector3.right * _moveForce * Time.deltaTime);
            //Instantiate(_skidPrefab, this.transform.position, this.transform.rotation);
        }
    }
}
