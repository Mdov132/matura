using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //knjiznica za input system, ki ga uporabljamo za vse vhodne podatke

public class InputManager : MonoBehaviour
{
    private PlayerInput PlayerInput; //referenca za skripto, ki ga je generiral input system
    private PlayerInput.OnFootActions onFoot; //referenca za onFoot mapo

    private PlayerMotor motor; //referenca za PlayerMotor skripto

    private PlayerLook look; 

    void Awake()
    {
        PlayerInput = new PlayerInput(); //nov instance playerInput razreda
        onFoot = PlayerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump(); //pripisemo jump funkcijo k jump action-u. vsakic ko je onFoot.jump izveden uporabimo callback context (ctx) da poklicemo motor.jump funkcijo
        onFoot.Sprint.performed += ctx => motor.Sprint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //povemo playerMotor-ju da se premakne z vrednostjo iz movement action-a
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable() //omogoci action map
    {
        onFoot.Enable();
    }

    private void OnDisable() //onemogoci action map
    {
        onFoot.Disable();
    }
}
