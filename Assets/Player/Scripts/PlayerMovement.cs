﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	private CharacterController2D controller;
	private Animator animator;
	private SpriteRenderer spriteRenderer;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		controller = GetComponent<CharacterController2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis("Horizontal");

		horizontalMove = move.x * runSpeed;

		animator.SetFloat("velocityX", Mathf.Abs(horizontalMove));

		if (move.x > 0.01f)
		{
			if (spriteRenderer.flipX == true)
			{
				spriteRenderer.flipX = false;
			}
		}
		else if (move.x < -0.01f)
		{
			if (spriteRenderer.flipX == false)
			{
				spriteRenderer.flipX = true;
			}
		}

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching(bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
