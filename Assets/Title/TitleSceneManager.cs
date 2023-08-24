using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
	public GameObject player;
	Rigidbody2D playerRb;
	PlayerInput playerInput;
	PlayerManager playerManager;
	Animator playerAnimator;
	Transform playerTransform;

	public GameObject detection;
	BoxCollider2D detectionCol;
	DetectionZone detectionZone;

	private void Awake()
	{
		Screen.SetResolution(1280, 720, FullScreenMode.Windowed);

		player = GameObject.FindWithTag("Player");
		playerRb = player.GetComponent<Rigidbody2D>();
		playerInput = player.GetComponent<PlayerInput>();
		playerManager = player.GetComponent<PlayerManager>();
		playerAnimator = player.GetComponent<Animator>();
		playerTransform = player.GetComponent<Transform>(); ;

		detection = GameObject.FindWithTag("DetectionZone");
		detectionCol = detection.GetComponent<BoxCollider2D>();
		detectionZone = detection.GetComponent<DetectionZone>();
	}


	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			Debug.Log("Enter key was pressed!");
			if (detectionCol) { detectionCol.isTrigger = true; }
			if (playerManager) { playerManager.enabled = false; }
			if (playerInput) { playerInput.enabled = false; }

			StartCoroutine(movePlayer());
		}
	}

	private System.Collections.IEnumerator movePlayer()
	{
		if (playerAnimator) { playerAnimator.SetBool(AnimStrings.isWalking, true); }
		while (true)
		{
			if (playerRb)
			{
				playerRb.velocity = new Vector2(5, playerRb.velocity.y);
				if (playerTransform)
				{
					if (playerTransform.localScale.x < 0)
					{ playerTransform.localScale *= new Vector2(-1, 1); }
				}
			}
			yield return null;
		}
	}

	public void SceneChange()
	{
		SceneManager.LoadScene("PlayScene");
	}

}
