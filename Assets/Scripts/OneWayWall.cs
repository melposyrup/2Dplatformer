using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayWall : MonoBehaviour
{
	public bool isTrigger = true;

	BoxCollider2D col;
	private void Awake()
	{
		col = GetComponent<BoxCollider2D>();
		col.isTrigger = isTrigger;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			col.isTrigger = !(isTrigger);
		}
	}
}
