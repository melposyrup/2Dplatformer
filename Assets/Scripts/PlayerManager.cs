using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerManager : MonoBehaviour
{
	[SerializeField] float walkSpeed = 4f;
	[SerializeField] float runSpeed = 7f;
	[SerializeField] float jumpImpulse = 10f;
	Vector2 moveInput;

	private bool _isWalking = false;
	public bool IsWalking
	{
		get { return _isWalking; }
		private set
		{
			_isWalking = value;
			animator.SetBool(AnimStrings.isWalking, value);
		}
	}
	private bool _isRunning = false;
	public bool IsRunning
	{
		get { return _isRunning; }
		private set
		{
			_isRunning = value;
			animator.SetBool(AnimStrings.isRunning, value);
		}
	}

	public Vector2 CurrentSpeed
	{
		get
		{
			if (IsWalking)
			{
				if (IsRunning) { return new Vector2(moveInput.x * runSpeed, rb.velocity.y); }
				else { return new Vector2(moveInput.x * walkSpeed, rb.velocity.y); }
			}
			else { return new Vector2(0, rb.velocity.y); }
		}
	}

	Rigidbody2D rb;
	Animator animator;
	Directions directions;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		directions = GetComponent<Directions>();
	}
	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
		transform.localScale *= directions.facing(moveInput, transform);

		//if (!Input.GetKeyDown("space")) 
		IsWalking = (moveInput != Vector2.zero);

	}
	public void OnRun(InputAction.CallbackContext context)
	{
		if (context.started) { IsRunning = true; }
		else if (context.canceled) { IsRunning = false; }
	}
	public void OnJump(InputAction.CallbackContext context)
	{
		//TODO check alive
		if (context.started)
		{
			if (directions.IsGrounded || directions.IsOnWall)
			{
				animator.SetTrigger(AnimStrings.jump);
				rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
			}
		}
	}


	private void FixedUpdate()
	{
		rb.velocity = CurrentSpeed;
		animator.SetFloat(AnimStrings.yVelocity, rb.velocity.y);
	}
}
