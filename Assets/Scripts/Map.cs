using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Map : MonoBehaviour
{
	[Header("Map Settings")]
	[SerializeField] GameObject player;
	[SerializeField] float mapWidth;
	[SerializeField] int mapNums = 3;
	[SerializeField] float totalWidth;
	[SerializeField] float fraction = 1f;
	private Vector3 lastPos;
	private Vector3 lastPosPlayer;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		mapWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
	}

	private void Start()
	{
		totalWidth = mapWidth * mapNums;
		lastPos = player.transform.position;
		lastPosPlayer = player.transform.position;
	}

	private void FixedUpdate()
	{
		Vector2 MoveAmount = new Vector2(
			player.transform.position.x - lastPosPlayer.x,
			player.transform.position.y - lastPosPlayer.y);

		transform.position += new Vector3(MoveAmount.x * fraction, MoveAmount.y * fraction, 0f);

		Vector3 tempPosition = transform.position;
		if (player.transform.position.x > transform.position.x + totalWidth / 2)
		{
			tempPosition.x += totalWidth;
			transform.position = tempPosition;
		}
		else if (player.transform.position.x < transform.position.x - totalWidth / 2)
		{
			tempPosition.x -= totalWidth;
			transform.position = tempPosition;
		}

		lastPos = transform.position;
		lastPosPlayer = player.transform.position;
	}

}
