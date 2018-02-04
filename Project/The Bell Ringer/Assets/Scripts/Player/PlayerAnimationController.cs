using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator playerAnimController;

	// Use this for initialization
	void Start ()
    {
        playerAnimController = this.GetComponent<Animator>();
	}	

    public void UpdateAnimatorMovement(int movement)
    {
        playerAnimController.SetInteger("walking", movement);
    }

    public void UpdateMeshRotation(Vector3 movement)
    {
        if(movement != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(movement);
    }

    public void PlayerIsFalling()
    {
        playerAnimController.SetBool("isFalling", true);
    }

    public void PlayerLanding(bool isHardLanding)
    {
        playerAnimController.SetBool("isFalling", false);

        if (isHardLanding)
        {
            playerAnimController.SetBool("hardLanding", true);
        }
        else
        {
            playerAnimController.SetBool("softLanding", true);
        }
    }

    public void PlayerHasLanded()
    {
        playerAnimController.SetBool("hardLanding", false);
        playerAnimController.SetBool("softLanding", false);
    }
}
