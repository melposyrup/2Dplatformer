using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
	public UnityEvent ColliderRemain;
	public UnityEvent noColliderRemain;

	public List<Collider2D> detectedCol = new List<Collider2D>();
	Collider2D col;

	private void Awake()
	{
		col = GetComponent<Collider2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		detectedCol.Add(collision);
		if (detectedCol.Count > 0) { ColliderRemain.Invoke(); }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		detectedCol.Remove(collision);

		if (detectedCol.Count <= 0 ) { noColliderRemain.Invoke(); }
	}
}
