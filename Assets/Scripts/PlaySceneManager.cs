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

	public float delay = 2f;

	public GameObject openingDetection;


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
		StartCoroutine(movePlayer());
		fadeLayerFading.StartFadeOut();
		loadingFading.SetAlpha(0f);
		gameOverFading.SetAlpha(0f);
		SoundManager.Instance.PlayBGM(BGMSoundData.BGM.PlayScene);

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
		loadingFading.StartFadeIn();
		yield return new WaitForSeconds(delay);
		fadeLayerFading.StartFadeInAndOut();
		yield return new WaitForSeconds(1.0f);
		loadingFading.StartFadeOut();

		if (lastSavePoint)
		{
			Vector3 pos = lastSavePoint.GetComponent<Transform>().position;
			playerDamageable.Health = 100;
			playerDamageable.IsAlive = true;
			playerTransform.position = pos;
			playerInput.enabled = true;
		}
		SoundManager.Instance.PlaySE(SESoundData.SE.Enter);
	}

	private IEnumerator GameOver()
	{
		gameOverFading.StartFadeIn();
		SoundManager.Instance.StopBGM(BGMSoundData.BGM.PlayScene);
		SoundManager.Instance.PlaySE(SESoundData.SE.GameOver);
		yield return new WaitForSeconds(delay);
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
	}

}
