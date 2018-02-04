using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerMovementController : MonoBehaviour
{
    //Components
    private PlayerAnimationController playerAnimController;

    private float acclerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    [SerializeField]
    private float moveSpeed = 3;
    private float gravity = -20f;
    private Controller2D controller;
    private Vector3 velocity;
    private float velocityXSmoothing;

    private float fallingDistance;

    private MovementState movementState;
    private enum MovementState
    {
        grounded,
        falling,
    }

	// Use this for initialization
	void Start ()
    {
        controller = this.GetComponent<Controller2D>();
        playerAnimController = this.GetComponentInChildren<PlayerAnimationController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (controller.collisions.below)
        {
            velocity.y = 0;

            //If Player Has Just Landed
            if (movementState == MovementState.falling)
            {
                velocity.x = 0;

                float distanceFallen = fallingDistance - transform.position.y;

                bool hardLanding = false;

                if(distanceFallen > 5)
                {
                    hardLanding = true;
                }
             
                playerAnimController.PlayerLanding(hardLanding);
                StartCoroutine(LandingCooldown(hardLanding));
            }
            else if(movementState == MovementState.grounded)
            {
                Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                float targetVelocityX = input.x * moveSpeed;
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : acclerationTimeAirborne);

                if (velocity.x != 0)
                    playerAnimController.UpdateMeshRotation(new Vector3(velocity.x, 0, 0));

                playerAnimController.UpdateAnimatorMovement((int)Mathf.Abs(input.x));
            }
        }

            //Check if falling
        if (!controller.collisions.below)
        {
            if (movementState == MovementState.grounded)
            {
                fallingDistance = transform.position.y;
                playerAnimController.PlayerIsFalling();
                movementState = MovementState.falling;
            }
        }
      
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        
    }

    IEnumerator LandingCooldown(bool hardLanding)
    {
        float waitTime = .5f;
        if (hardLanding)
            waitTime = 1f;

        yield return new WaitForSeconds(waitTime);
        Debug.Log("Hit");
        playerAnimController.PlayerHasLanded();
        movementState = MovementState.grounded;

    }
}
