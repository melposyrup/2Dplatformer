using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Damageable))]
public class PlayerManager : MonoBehaviour
{
	[SerializeField] float walkSpeed = 4f;
	[SerializeField] float runSpeed = 7f;
	[SerializeField] float jumpImpulse = 10f;
	[SerializeField] int maxJumpCount = 2;
	[SerializeField] int jumpCount = 0;
	Vector2 moveInput;

	[SerializeField] PhysicsMaterial2D MaterialFriction0;
	[SerializeField] PhysicsMaterial2D MaterialFriction10;

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

	public bool CanMove
	{
		get { return animator.GetBool(AnimStrings.canMove); }
	}
	public bool IsAlive
	{
		get { return animator.GetBool(AnimStrings.isAlive); }
	}



	public Vector2 CurrentSpeed
	{
		get
		{
			if (!CanMove) { return new Vector2(0, rb.velocity.y); }
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
	PlayerInput playerInput;
	Damageable damageable;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		directions = GetComponent<Directions>();
		playerInput = GetComponent<PlayerInput>();
		damageable = GetComponent<Damageable>();

		playerInput.enabled = true;
	}
	private void FixedUpdate()
	{
		if (!IsAlive)
		{
			playerInput.enabled = false;
			//TODO GameOverAnim() with SceneChange
		}
		//else { playerInput.enabled = true; }

		// update speed
		if (!damageable.LockVelocity) { rb.velocity = CurrentSpeed; }
		animator.SetFloat(AnimStrings.yVelocity, rb.velocity.y);

		// Go smooth on stairs
		if (!directions.OnStair) { rb.sharedMaterial = MaterialFriction10; }
		else { rb.sharedMaterial = MaterialFriction0; }
	}


	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
		transform.localScale *= facing(moveInput, transform);

		IsWalking = (moveInput != Vector2.zero);

	}
	public void OnRun(InputAction.CallbackContext context)
	{
		if (context.started) { IsRunning = true; }
		else if (context.canceled) { IsRunning = false; }
	}
	public void OnJump(InputAction.CallbackContext context)
	{
		if (directions.IsGrounded) { jumpCount = 1; }
		//TODO check alive
		if (context.started && (jumpCount < maxJumpCount))
		{
			if (directions.IsGrounded || directions.IsOnWall)
			{
				animator.SetTrigger(AnimStrings.jumpTrigger);
				rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
				jumpCount++;
			}
		}
	}

	public void OnAttack(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			animator.SetTrigger(AnimStrings.attackTrigger);
		}
	}

	private Vector2 facing(Vector2 moveInput, Transform transform)
	{
		if (moveInput.x > 0 && transform.localScale.x < 0) { return new Vector2(-1, 1); }
		else if (moveInput.x < 0 && transform.localScale.x > 0) { return new Vector2(-1, 1); }
		return new Vector2(1, 1);
	}

	public void OnHit(int damage, Vector2 knockback)
	{
		rb.velocity = new Vector2(knockback.x, rb.velocity.y * knockback.y);
	}

}
