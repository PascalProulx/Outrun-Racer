using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that animate the car
/// </summary>
public class CarAnimation : MonoBehaviour
{
    /// <summary>
    /// Fields
    /// </summary>
    private Animator _anim;                             // The component Animator
    private Joystick _leftjoystick;                     // Joystick who move the car horizontally
    private Joystick _rightjoystick;                    // Joystick who turn the car 90 degree on the y axis


    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component and the joysticks
        _anim = GetComponent<Animator>();
        _leftjoystick = GameObject.FindWithTag("LeftJoystick").GetComponent<FixedJoystick>();
        _rightjoystick = GameObject.FindWithTag("RightJoystick").GetComponent<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make the wheels of the car spin
        _anim.SetBool("wheelsSpinning", true);

        // Play the animations of the car when it moves
        if (_leftjoystick.Horizontal == -1f || Input.GetKey(KeyCode.A))
        {
            // Play the animation when the car moves to the left
            _anim.SetBool("strafeLeft", true);
        }
        else
        {
            // If not, play the Idle animation
            _anim.SetBool("strafeLeft", false);
            _anim.SetBool("isIdle", true);
        }
        if (_leftjoystick.Horizontal == 1f || Input.GetKey(KeyCode.D))
        {
            // Play the animation when the car moves to the right
            _anim.SetBool("strafeRight", true);
        }
        else
        {
            // If not, play the Idle animation
            _anim.SetBool("strafeRight", false);
            _anim.SetBool("isIdle", true);
        }

        // Play the animations of the car when it can turn on a T-section 
        if ((_rightjoystick.Horizontal == -1f || Input.GetKeyDown(KeyCode.Q)) && Car._canTurn)
        {
            // Play the animation when the car turn to the left
            _anim.SetBool("driftLeft", true);
        }
        else
        {
            // If not, play the Idle animation
            _anim.SetBool("driftLeft", false);
            _anim.SetBool("isIdle", true);
        }
        if ((_rightjoystick.Horizontal == 1f || Input.GetKeyDown(KeyCode.E)) && Car._canTurn)
        {
            // Play the animation when the car turn to the right
            _anim.SetBool("driftRight", true);
        }
        else
        {
            // If not, play the Idle animation
            _anim.SetBool("driftRight", false);
            _anim.SetBool("isIdle", true);
        }
        /*else
        {
            // On bloque les mouvements du joueur si il y a contacte
            _anim.SetBool("wheelsSpinning", false);
        }*/
    }

}
