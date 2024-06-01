using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Create a movement vector based on input
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement = transform.TransformDirection(movement);

        // Move the character controller
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }
}