using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerMovementController : MonoBehaviour
{
    //Components
    private PlayerAnimationController playerAnimController;

    private float moveSpeed = 3;
    private float gravity = -20f;
    private Controller2D controller;
    private Vector3 velocity;

	// Use this for initialization
	void Start ()
    {
        controller = this.GetComponent<Controller2D>();
        playerAnimController = this.GetComponentInChildren<PlayerAnimationController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        velocity.x = input.x * moveSpeed;
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(velocity.x != 0)
            playerAnimController.UpdateMeshRotation(new Vector3(velocity.x,0,0));
        playerAnimController.UpdateAnimatorMovement((int)Mathf.Abs(input.x));


    }
}
