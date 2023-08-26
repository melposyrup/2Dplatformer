using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapSpikes : MonoBehaviour
{
	public int attackDamage = 5;
	public Vector2 knockback = Vector2.zero;

	Collider2D attackCol;
	private void Awake()
	{
		attackCol = GetComponent<Collider2D>();
		attackCol.enabled = true;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		// See if it can be hit
		Damageable damageable = collision.GetComponent<Damageable>();
		if (damageable != null)
		{
			if (collision.GetComponent<Transform>().position.x < 0) { knockback *= new Vector2(-1, 1); }
			bool gotHit = damageable.Hit(attackDamage, knockback);
			//if (gotHit) { Debug.Log(collision.name + "hit for " + attackDamage); }
		}


	}
}
