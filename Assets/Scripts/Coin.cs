using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public GameObject ItemFeedbackEffect;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			SoundManager.Instance.PlaySE(SESoundData.SE.Kill);
			Instantiate(ItemFeedbackEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}


}
