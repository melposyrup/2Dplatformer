using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Directions))]
public class Hyena : MonoBehaviour
{
	[SerializeField] float walkSpeed = 4f;
	[SerializeField] float walkStopRate = 0.1f;
	public DetectionZone attackZone;

	private Vector2 walkDirectionVector = Vector2.right;

	public enum WALK_DIRECTION { RIGHT, LEFT };
	private WALK_DIRECTION _walkDirection;
	public WALK_DIRECTION WalkDirection
	{
		get { return _walkDirection; }
		set
		{
			if (_walkDirection != value)
			{
				transform.localScale = new Vector2(transform.localScale.x * -1 ,transform.localScale.y);
				if (value == WALK_DIRECTION.RIGHT) { walkDirectionVector = Vector2.right; }
				else if (value == WALK_DIRECTION.LEFT) { walkDirectionVector = Vector2.left; }
			}
			_walkDirection = value;
		}
	}

	[SerializeField] bool _hasTarget = false;
	public bool HasTarget
	{
		get { return _hasTarget; }
		private set
		{
			_hasTarget = value;
			animator.SetBool(AnimStrings.hasTarget, value);
		}
	}

	public bool CanMove
	{
		get { return animator.GetBool(AnimStrings.canMove); }
	}


	Rigidbody2D rb;
	Directions directions;
	Animator animator;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		directions = GetComponent<Directions>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		HasTarget = attackZone.detectedCol.Count > 0;
	}

	private void FixedUpdate()
	{

		if (directions.IsGrounded && directions.IsOnWall && CanMove) { FlipDirection(); }
		if (CanMove) { rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x , rb.velocity.y); }
		else { rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y); }
	}
	private void FlipDirection()
	{
		if (WalkDirection == WALK_DIRECTION.RIGHT) { WalkDirection = WALK_DIRECTION.LEFT; }
		else if (WalkDirection == WALK_DIRECTION.LEFT) { WalkDirection = WALK_DIRECTION.RIGHT; }
		else { Debug.LogError("Current WalkDirection is not set to legal value"); }
		//Debug.Log(walkDirectionVector);
	}

}
