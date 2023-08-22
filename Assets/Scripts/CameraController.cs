using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform player;
	public Transform BackgroundFar, BackgroundMiddle,BackgroundNearby;
	private Vector2 lastPos;
	private void Start()
	{
		lastPos = transform.position;
	}

	private void FixedUpdate()
	{
		transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

		Vector2 MoveAmount = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

		BackgroundFar.position += new Vector3(MoveAmount.x, MoveAmount.y, 0f);
		BackgroundMiddle.position += new Vector3(MoveAmount.x * 0.5f, MoveAmount.y * 0.5f, 0f);
		BackgroundNearby.position += new Vector3(MoveAmount.x * 0.2f, MoveAmount.y * 0.2f, 0f);

		lastPos = transform.position;
	}
}
