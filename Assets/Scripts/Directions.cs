using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directions : MonoBehaviour
{
	public ContactFilter2D contactFilter;
	public float groundDistance = 0.05f;
	public float wallDistance = 0.05f;
	public float ceilingDistance = 0.05f;
	RaycastHit2D[] groundHits = new RaycastHit2D[5];
	RaycastHit2D[] wallHits = new RaycastHit2D[5];
	RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

	private Vector2 wallDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

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



	CapsuleCollider2D col;
	Animator animator;
	private void Awake()
	{
		col = GetComponent<CapsuleCollider2D>();
		animator = GetComponent<Animator>();
	}
	public Vector2 facing(Vector2 moveInput, Transform transform)
	{
		if (moveInput.x > 0 && transform.localScale.x < 0) { return new Vector2(-1, 1); }
		else if (moveInput.x < 0 && transform.localScale.x > 0) { return new Vector2(-1, 1); }
		return new Vector2(1, 1);
	}

	private void FixedUpdate()
	{
		IsGrounded = col.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
		IsOnWall = col.Cast(wallDirection, contactFilter, wallHits, wallDistance) > 0;
		IsOnCeiling = col.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;

	}

}
