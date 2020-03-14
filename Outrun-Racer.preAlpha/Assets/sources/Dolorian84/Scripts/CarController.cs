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
    private Animator _anim;                              // Animator of the car
    private bool canTurn = false;                       // Bool that let the car do a 90 degree turn
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
