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
        transform.rotation = Quaternion.LookRotation(movement);
    }
}
