using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	public GameObject player;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		transform.position = player.transform.position;
	}
}
