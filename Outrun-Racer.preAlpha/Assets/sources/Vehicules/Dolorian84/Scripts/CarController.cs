using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script that control the car 
/// </summary>
public class CarController : MonoBehaviour
{
    
    /// <summary>
    /// Fields
    /// </summary>
    public static GameObject player;                                    // GameObject that represent the player
    public static GameObject currentPlatform;                           // GameObject that represent the current platform that the player in on
    public static bool _canTurn = false;                                // Bool that let the car do a 90 degree turn

    private Joystick _leftjoystick;                                     // Joystick who move the car horizontally
    private Joystick _rightjoystick;                                    // Joystick who turn the car 90 degree on the y axis

    private AudioSource _engineLowSource;                               // AudioSource that represent the low accelleration of the car
    private AudioSource _engineHighSource;                              // AudioSource that represent the low accelleration of the car
    private AudioSource _skidSource;                                    // AudioSource that represent the low accelleration of the car

    Vector3 startPosition;                                              // TEMPORARY
    private bool _carIsMoving = false;
    private bool _sfxIsPlaying = false;

    [SerializeField] private float _moveForce = 5f;                     // Float that represent the speed of the joysticks
    [SerializeField] private float _carSpeed = 0.1f;                    // Float that represent the car speed's

    [Header("Car SFX")]
    [SerializeField] private AudioClip _engineLowSFX;                   // Audioclip that represent the low accelleration of the car
    [SerializeField] private AudioClip _engineHighSFX;                  // Audioclip that represent the high accelleration of the car
    [SerializeField] private AudioClip _skidScreechSFX;                 // Audioclip that represent the skid screeching of the car
    [SerializeField] private AudioClip _skidScreechSFX2;                 // Audioclip that represent the skid screeching of the car


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
        // Get the joysticks gameObjects
        _leftjoystick = GameObject.FindWithTag("LeftJoystick").GetComponent<FixedJoystick>();
        _rightjoystick = GameObject.FindWithTag("RightJoystick").GetComponent<FixedJoystick>();

        // Make this object static
        player = this.gameObject;

        // Generate the platforms for the player
        GenerateWorld.RunDummy();

        // Create the audioSources, and save them as references
        _engineLowSource = this.gameObject.AddComponent<AudioSource>();
        _engineHighSource = this.gameObject.AddComponent<AudioSource>();
        _skidSource = this.gameObject.AddComponent<AudioSource>();

        PlayEngineLowSFX(_engineLowSFX);
        PlayEngineHighSFX(_engineHighSFX);
        AudioManager.Instance.PlaySFX(_skidScreechSFX2);
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

    // Update is called once per frame
    void Update()
    {
        // Move the car
        _carIsMoving = true;
        if (_carIsMoving == true)
        {
            CarMouvement();
        }
        else
        {
            _carIsMoving = false;
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyUp(KeyCode.A) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.A) && Input.GetKeyUp(KeyCode.D)))
        {
            PlaySkidSFX();
        }else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _skidSource.Stop();
        }

    }

    #region
    /// <summary>
    /// Function that move the car
    /// </summary>
    private void CarMouvement()
    {
        if (_carIsMoving == true)
        {
            // Move the car forward
            this.transform.position += this.transform.forward * _carSpeed;

            // Set the inputs of the car and ajust the car mouvement according to the platform that the car is actually on
            if ((_rightjoystick.Horizontal == 1f || Input.GetKeyDown(KeyCode.E)) && _canTurn)
            {
                // Rotate the car to the left if it's on a T-section platform
                this.transform.Rotate(Vector3.up * 90);

                // Change the platforms spawning location so that it can face the car
                GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
                GenerateWorld.RunDummy();

                // Continue to generate the platforms if the last platform was not a T-section platform
                if (GenerateWorld.lastPlatform.tag != "platformTSection")
                    GenerateWorld.RunDummy();

                // Place the platforms on the car position's
                this.transform.position = new Vector3(this.transform.position.x,
                                                this.transform.position.y,
                                                this.transform.position.z);

                // Make sure that the car can't no longer turn
                _canTurn = false;
            }
            else if ((_rightjoystick.Horizontal == -1f || Input.GetKeyDown(KeyCode.Q)) && _canTurn)
            {
                // Rotate the car to the right if it's on a T-section platform
                this.transform.Rotate(Vector3.up * -90);

                // Change the platforms spawning location so that it can face the car
                GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
                GenerateWorld.RunDummy();

                // Continue to generate the platforms if the last platform was not a T-section platform
                if (GenerateWorld.lastPlatform.tag != "platformTSection")
                    GenerateWorld.RunDummy();

                // Place the platforms on the car position's
                this.transform.position = new Vector3(this.transform.position.x,
                                    this.transform.position.y,
                                    this.transform.position.z);

                // Make sure that the car can't no longer turn
                _canTurn = false;
            }
            else if (_leftjoystick.Horizontal == -1f || Input.GetKey(KeyCode.A))
            {
                // Move the car to the left
                this.transform.Translate(Vector3.right * -_moveForce * Time.deltaTime);
                //Instantiate(_skidPrefab, this.transform.position, this.transform.rotation);
            }
            else if (_leftjoystick.Horizontal == 1f || Input.GetKey(KeyCode.D))
            {
                // Move the car to the right
                this.transform.Translate(Vector3.right * _moveForce * Time.deltaTime);
                _sfxIsPlaying = true;
                //Instantiate(_skidPrefab, this.transform.position, this.transform.rotation);
            }else if (Input.GetKeyDown(KeyCode.A))
            {
                // Move the car to the right
                this.transform.Translate(Vector3.right * _moveForce * Time.deltaTime);
                
            }
        }
        
    }
    #endregion

    #region Car Sound Manager
    /// <summary>
    /// Method that play a looped sound for the car engine
    /// </summary>
    /// <param name="clip"></param>
    private void PlayEngineLowSFX(AudioClip clip)
    {
        _engineLowSource.loop = true;
        _engineLowSource.clip = clip;
        _engineLowSource.volume = 1f;
        _engineLowSource.Play();
    }

    /// <summary>
    /// Method that play another looped sound for the car
    /// </summary>
    /// <param name="clip"></param>
    private void PlayEngineHighSFX(AudioClip clip)
    {
        _engineHighSource.loop = true;
        _engineHighSource.clip = clip;
        _engineHighSource.volume = 1f;
        _engineHighSource.Play();
    }

    /// <summary>
    /// Method that play a sound when the car is moving left and right
    /// </summary>
    /// <param name="clip"></param>
    private void PlaySkidSFX()
    {
        _skidSource.loop = true;
        _skidSource.clip = _skidScreechSFX;
        _skidSource.volume = 1f;
        _skidSource.Play();
    }
    #endregion
}