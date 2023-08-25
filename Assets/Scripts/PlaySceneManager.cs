using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour
{
	public GameObject player;
	Rigidbody2D playerRb;
	PlayerInput playerInput;
	PlayerManager playerManager;
	Animator playerAnimator;
	Transform playerTransform;
	Damageable playerDamageable;

	public GameObject gameOverPrefab;
	FadingAnim gameOverFading;

	public GameObject loadingPrefab;
	FadingAnim loadingFading;

	public GameObject fadeLayerPrefab;
	FadingAnim fadeLayerFading;

	public float delay = 3f;

	public GameObject openingDetection;
	public GameObject leftAirWall;

	private void Awake()
	{
		Screen.SetResolution(1280, 720, FullScreenMode.Windowed);

		player = GameObject.FindWithTag("Player");
		playerRb = player.GetComponent<Rigidbody2D>();
		playerInput = player.GetComponent<PlayerInput>();
		playerManager = player.GetComponent<PlayerManager>();
		playerAnimator = player.GetComponent<Animator>();
		playerTransform = player.GetComponent<Transform>();
		playerDamageable = player.GetComponent<Damageable>();

		gameOverFading = gameOverPrefab.GetComponent<FadingAnim>();
		loadingFading = loadingPrefab.GetComponent<FadingAnim>();
		fadeLayerFading = fadeLayerPrefab.GetComponent<FadingAnim>();
	}
	private void Start()
	{
		if (leftAirWall) { leftAirWall.GetComponent<BoxCollider2D>().isTrigger = true; }
		StartCoroutine(movePlayer());
		fadeLayerFading.StartFadeOut();
		loadingFading.ResetAlpha(0f);
		gameOverFading.ResetAlpha(0f);
	}

	public void CheckSaveData(GameObject lastSavePoint)
	{
		if (lastSavePoint)
		{
			loadingFading.StartFadeIn();
			StartCoroutine(LoadSave(lastSavePoint));
		}
		else
		{
			gameOverFading.StartFadeIn();
			StartCoroutine(GameOver());
		}

	}
	private IEnumerator LoadSave(GameObject lastSavePoint)
	{
		yield return new WaitForSeconds(delay);
		loadingFading.StartFadeInAndOut();
		fadeLayerFading.StartFadeInAndOut();
		yield return new WaitForSeconds(1.0f);
		if (lastSavePoint)
		{
			Vector3 pos = lastSavePoint.GetComponent<Transform>().position;
			playerDamageable.Health = 100;
			playerDamageable.IsAlive = true;
			playerTransform.position = pos;
			playerInput.enabled = true;
		}

	}

	private IEnumerator GameOver()
	{
		yield return new WaitForSeconds(delay);
		gameOverFading.StartFadeIn();
		fadeLayerFading.StartFadeIn();
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene("TitleScene");

	}

	private System.Collections.IEnumerator movePlayer()
	{
		if (playerManager) { playerManager.enabled = false; }
		if (playerInput) { playerInput.enabled = false; }
		if (playerAnimator) { playerAnimator.SetBool(AnimStrings.isWalking, true); }
		while (true)
		{
			if (playerRb)
			{
				playerRb.velocity = new Vector2(4, playerRb.velocity.y);
				if (playerTransform)
				{
					if (playerTransform.localScale.x < 0)
					{ playerTransform.localScale *= new Vector2(-1, 1); }
				}
			}
			yield return null;
		}


	}

	public void PlayerStartUp()
	{
		if (playerManager) { playerManager.enabled = true; }
		if (playerInput) { playerInput.enabled = true; }
		if (playerAnimator) { playerAnimator.SetBool(AnimStrings.isWalking, false); }
	}

	public void ComponentInactive()
	{
		if (openingDetection) { openingDetection.GetComponent<BoxCollider2D>().enabled = false; }
		if (leftAirWall) { leftAirWall.GetComponent<BoxCollider2D>().isTrigger = false; }
	}

}
