using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
	Animator animator;
	public GameObject ItemFeedbackEffect;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!animator.GetBool(AnimStrings.saved))
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				animator.SetBool(AnimStrings.saved, true);
				Instantiate(ItemFeedbackEffect, transform.position, transform.rotation);
			}
		}
	}

}
