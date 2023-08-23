using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
	public UnityEvent<int, Vector2> damageableHit;



	[SerializeField] private float _maxHealth = 100f;
	public float MaxHealth
	{
		get { return _maxHealth; }
		set { _maxHealth = value; }
	}

	[SerializeField] private float _health = 100f;
	public float Health
	{
		get { return _health; }
		set
		{
			_health = value;
			if (_health <= 0) { IsAlive = false; }
		}
	}

	[SerializeField] private bool _isAlive = true;
	public bool IsAlive
	{
		get { return _isAlive; }
		set
		{
			_isAlive = value;
			animator.SetBool(AnimStrings.isAlive, value);
			Debug.Log("IsAlive set" + value);
		}
	}

	[SerializeField] private bool isInvincible = false;

	public bool LockVelocity
	{
		get { return animator.GetBool(AnimStrings.lockVelocity); }
		set { animator.SetBool(AnimStrings.lockVelocity, value); }
	}


	private float timeSinceHit = 0f;
	public float invincibilityTimer = 0.25f;


	Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	// Return whether the damageable took damage or not
	public bool Hit(int damage, Vector2 knockback)
	{
		if (_isAlive && !isInvincible)
		{
			Health -= damage;
			isInvincible = true;

			// Notify other subscribed components that the damageble was hit to handle the knockback
			animator.SetTrigger(AnimStrings.hitTrigger);
			LockVelocity = true;
			damageableHit?.Invoke(damage, knockback);

			return true;
		}

		// Unable to be hit
		return false;
	}

	private void Update()
	{
		if (isInvincible)
		{
			if (timeSinceHit > invincibilityTimer)
			{
				isInvincible = false;
				timeSinceHit = 0;
			}
			timeSinceHit += Time.deltaTime;
		}

		//Hit(5);
	}








}
