using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directions : MonoBehaviour
{
	public ContactFilter2D contactFilter;
	float groundDistance = 0.05f;
	float wallDistance = 0.1f;
	float ceilingDistance = 0.05f;
	RaycastHit2D[] groundHits = new RaycastHit2D[5];
	RaycastHit2D[] wallHits = new RaycastHit2D[5];
	RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

	float onStairDownDistance = 1f;
	RaycastHit2D[] onStairHits = new RaycastHit2D[5];


	public Vector2 wallDirection => rb.velocity.x > 0 ? Vector2.right : Vector2.left;

	[SerializeField]
	private bool _isGrounded;
	public bool IsGrounded
	{
		get { return _isGrounded; }
		private set
		{
			_isGrounded = value;
			animator.SetBool(AnimStrings.isGrounded, value);
		}
	}

	[SerializeField]
	private bool _isOnWall;
	public bool IsOnWall
	{
		get { return _isOnWall; }
		private set
		{
			_isOnWall = value;
			animator.SetBool(AnimStrings.isOnWall, value);
		}
	}

	[SerializeField]
	private bool _isOnCeiling;
	public bool IsOnCeiling
	{
		get { return _isOnCeiling; }
		private set { _isOnCeiling = value; }
	}

	[SerializeField]
	private bool _onStair;
	public bool OnStair
	{
		get { return _onStair; }
		private set { _onStair = value; }
	}

	CapsuleCollider2D col;
	Animator animator;
	Rigidbody2D rb;
	private void Awake()
	{
		col = GetComponent<CapsuleCollider2D>();
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		IsGrounded = col.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
		IsOnWall = col.Cast(wallDirection, contactFilter, wallHits, wallDistance) > 0;
		IsOnCeiling = col.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;

		OnStair = col.Cast(Vector2.down, contactFilter, onStairHits, onStairDownDistance) > 0 && IsOnWall;

	}

}
