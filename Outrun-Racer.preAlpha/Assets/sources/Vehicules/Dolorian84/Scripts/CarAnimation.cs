using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script that animate the car
/// </summary>
public class CarAnimation : MonoBehaviour
{
    private Animator _anim;                             // The component Animator
    private Joystick _leftjoystick;                     // Joystick who move the car horizontally
    private Joystick _rightjoystick;                    // Joystick who turn the car 90 degree on the y axis


    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _leftjoystick = GameObject.FindWithTag("LeftJoystick").GetComponent<FixedJoystick>();
        _rightjoystick = GameObject.FindWithTag("RightJoystick").GetComponent<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("wheelsSpinning", true);
        if (_leftjoystick.Horizontal == -1f || Input.GetKey(KeyCode.A))
        {
            _anim.SetBool("strafeLeft", true);
        }
        else
        {
            _anim.SetBool("strafeLeft", false);
            _anim.SetBool("isIdle", true);
        }
        if (_leftjoystick.Horizontal == 1f || Input.GetKey(KeyCode.D))
        {
            _anim.SetBool("strafeRight", true);
        }
        else
        {
            // Retour dans son état initial
            _anim.SetBool("strafeRight", false);
            _anim.SetBool("isIdle", true);
        }
        if ((_rightjoystick.Horizontal == -1f || Input.GetKeyDown(KeyCode.Q)) && CarController._canTurn)
        {
            _anim.SetBool("driftLeft", true);
        }
        else
        {
            _anim.SetBool("driftLeft", false);
            _anim.SetBool("isIdle", true);
        }
        if ((_rightjoystick.Horizontal == 1f || Input.GetKeyDown(KeyCode.E)) && CarController._canTurn)
        {
            _anim.SetBool("driftRight", true);
        }
        else
        {
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
