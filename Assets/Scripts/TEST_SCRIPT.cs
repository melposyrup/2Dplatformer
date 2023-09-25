using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TEST_SCRIPT : MonoBehaviour
{
	public GameObject player;

	private void Awake()
	{
		player = GameObject.FindWithTag("Player");
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			player.GetComponent<Transform>().position = new Vector3(160.0f, 3.0f, 0.0f);
		}
	}

}
