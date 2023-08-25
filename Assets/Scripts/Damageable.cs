using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
	public UnityEvent<int, Vector2> damageableHit;
	public UnityEvent<int, int> healthChanged;


	[SerializeField] private int _maxHealth = 100;
	public int MaxHealth
	{
		get { return _maxHealth; }
		set { _maxHealth = value; }
	}

	[SerializeField] private int _health = 100;
	public int Health
	{
		get { return _health; }
		set
		{
			_health = value;
			healthChanged?.Invoke(_health, MaxHealth);
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
			CharacterEvents.characterDamaged.Invoke(gameObject, damage);

			return true;
		}

		// Unable to be hit
		return false;
	}

	public bool Heal(int healthRestore)
	{
		if (IsAlive && (Health < MaxHealth))
		{
			int maxHeal = Mathf.Max(MaxHealth - Health, 0);
			int actualHeal = Mathf.Min(maxHeal, healthRestore);
			Health += actualHeal;
			CharacterEvents.characterHealed.Invoke(gameObject, actualHeal);
			return true;
		}
		return false;
	}








}
