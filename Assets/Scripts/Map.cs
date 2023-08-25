using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Map : MonoBehaviour
{
	[Header("Map Settings")]
	[SerializeField] GameObject Camera;
	[SerializeField] float mapWidth;
	[SerializeField] int mapNums = 3;
	[SerializeField] float totalWidth;
	[SerializeField] float fraction = 1f;
	private Vector3 lastPos;
	private Vector3 lastPosCamera;

	private void Awake()
	{
		Camera = GameObject.FindGameObjectWithTag("MainCamera");
		mapWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
	}

	private void Start()
	{
		totalWidth = mapWidth * mapNums;
		lastPos = Camera.transform.position;
		lastPosCamera = Camera.transform.position;
	}

	private void FixedUpdate()
	{
		Vector2 MoveAmount = new Vector2(
			Camera.transform.position.x - lastPosCamera.x,
			Camera.transform.position.y - lastPosCamera.y);

		transform.position += new Vector3(MoveAmount.x * fraction, MoveAmount.y * fraction, 0f);

		Vector3 tempPosition = transform.position;
		if (Camera.transform.position.x > transform.position.x + totalWidth / 2)
		{
			tempPosition.x += totalWidth;
			transform.position = tempPosition;
		}
		else if (Camera.transform.position.x < transform.position.x - totalWidth / 2)
		{
			tempPosition.x -= totalWidth;
			transform.position = tempPosition;
		}

		lastPos = transform.position;
		lastPosCamera = Camera.transform.position;
	}

}
