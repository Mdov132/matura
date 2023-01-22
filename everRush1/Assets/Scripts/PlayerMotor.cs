using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded; //preverja ce si na tlah ali ne
    public float speed = 5f; //hitrost 
    public float gravity = -9.8f; //gravitacijski pospesek
    public float jumpHeight = 3f; //visina skoka
    public bool sprinting;

    // Start is called before the first frame update
    void Start() 
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    //pridobi vnose za InputManager.cs ki jih uporablja character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x; //vzamemo x komponento vector2 in jo enacimo z x-osjo player-ja
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);


        //gravitacija
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f; //konstanten gravitacijski pospesek ko je player na tleh
        playerVelocity.y += gravity * Time.deltaTime; //doda gravitacijski pospesek player-ju
        controller.Move(playerVelocity * Time.deltaTime); //z casom pospesi hitrost padca
        
    }
    public void Jump()
    {
        if (isGrounded) //skocis lahko samo ce si na tleh
        {
            playerVelocity.y = MathF.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8;
        else
            speed = 5;
    }
}


