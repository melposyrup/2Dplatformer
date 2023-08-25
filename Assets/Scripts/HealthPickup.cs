using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
	public int healthRestore = 50;
	public GameObject ItemFeedbackEffect;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Damageable damageable = collision.GetComponent<Damageable>();

		if (damageable)
		{
			bool wasHealed = damageable.Heal(healthRestore);
			if (wasHealed)
			{
				SoundManager.Instance.PlaySE(SESoundData.SE.Heal);
				Instantiate(ItemFeedbackEffect, transform.position, transform.rotation);
				Destroy(gameObject);
			}
		}
	}

}
