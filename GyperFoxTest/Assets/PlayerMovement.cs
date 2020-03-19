using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
	public Collider2D coll;

	public float runSpeed = 40f;
	float horizontalMove = 0f;
	public int cherries = 0;

	bool jump = false;
	bool crouch = false;
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("Jumping", true);
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

	}

	public void OnLanding ()
	{
		animator.SetBool("Jumping", false);
	}

	public void OnCrouching(bool IsCrouching)
	{
		animator.SetBool("Crouching", IsCrouching);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Cherry")
		{
			Destroy(collision.gameObject);
			cherries += 1;
		}
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
