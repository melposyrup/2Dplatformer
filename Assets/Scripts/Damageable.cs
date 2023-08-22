using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
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
			if (_health < 0) { IsAlive = false; }
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

	private bool isInvincible = false;
	private float timeSinceHit = 0f;
	public float invincibilityTimer = 0.25f;


	Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void Hit(int damage)
	{
		if (_isAlive && !isInvincible)
		{
			Health -= damage;
			isInvincible = true;
		}
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

		Hit(5);
	}








}
