using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	[Header("Map Settings")]
	[SerializeField] GameObject Player;
	[SerializeField] float mapWidth;
	[SerializeField] int mapNums = 3;
	[SerializeField] float totalWidth;
	[SerializeField] float fraction = 1f;
	private Vector3 lastPos;
	private Vector3 lastPosPlayer;

	private void Awake()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		mapWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
	}

	private void Start()
	{
		totalWidth = mapWidth * mapNums;
		lastPos = Player.transform.position;
		lastPosPlayer = Player.transform.position;
	}

	private void FixedUpdate()
	{
		Vector2 MoveAmount = new Vector2(
			Player.transform.position.x - lastPosPlayer.x,
			Player.transform.position.y - lastPosPlayer.y);

		transform.position += new Vector3(MoveAmount.x * fraction, MoveAmount.y * fraction, 0f);

		Vector3 tempPosition = transform.position;
		if (Player.transform.position.x > transform.position.x + totalWidth / 2)
		{
			tempPosition.x += totalWidth;
			transform.position = tempPosition;
		}
		else if (Player.transform.position.x < transform.position.x - totalWidth / 2)
		{
			tempPosition.x -= totalWidth;
			transform.position = tempPosition;
		}

		lastPos = transform.position;
		lastPosPlayer = Player.transform.position;
	}

}
