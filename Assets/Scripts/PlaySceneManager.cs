using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using System;

public class PlaySceneManager : MonoBehaviour
{
	PlayerInput CameraInput;

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

	public GameObject openingDetection;
	public GameObject endingDetection;
	Transform endingDetectionPos;

	public GameObject healthBar;
	public GameObject thanks;


	private void Awake()
	{
		Screen.SetResolution(1280, 720, FullScreenMode.Windowed);

		CameraInput = GetComponent<PlayerInput>();

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

		endingDetectionPos = endingDetection.GetComponent<Transform>();

		this.GetComponent<GameClearManager>().enabled = false;
	}
	private void Start()
	{
		CameraInput.enabled = false;
		fadeLayerFading.SetAlpha(1f);
		fadeLayerFading.StartFadeOut();
		loadingFading.SetAlpha(0f);
		gameOverFading.SetAlpha(0f);
		SoundManager.Instance.PlayBGM(BGMSoundData.BGM.PlayScene);
		thanks.SetActive(false);
		StartCoroutine(movePlayer());
	}

	/*
	 * EndingAnimation
	 */
	public void EndingAnimation()
	{
		StartCoroutine(EndingAnim());
	}

	private System.Collections.IEnumerator EndingAnim()
	{
		healthBar.SetActive(false);

		// player wait for a while
		if (playerManager) { playerManager.enabled = false; }
		if (playerInput) { playerInput.enabled = false; }

		// Fadeout BGM,  turn on se
		SoundManager.Instance.FadeOutBGMbySeconds(BGMSoundData.BGM.PlayScene, 5f);
		SoundManager.Instance.PlaySE(SESoundData.SE.Ending);

		// stop Cinemachine, set follow to null
		gameObject.GetComponent<CinemachineBrain>().enabled = false;

		yield return new WaitForSeconds(1.0f);

		// move camera 
		StartCoroutine(moveCamera());

		yield return null;
	}

	private System.Collections.IEnumerator moveCamera()
	{
		Transform pos = gameObject.GetComponent<Transform>();
		float cameraMoveSpeed = 4f;
		while (pos.position.x < endingDetectionPos.position.x)
		{
			pos.position = new Vector3(pos.position.x + cameraMoveSpeed * Time.deltaTime,
				pos.position.y, pos.position.z);

			yield return null;
		}
		yield return new WaitForSeconds(1.0f);

		// runPlayer
		StartCoroutine(runPlayer());
	}

	private System.Collections.IEnumerator runPlayer()
	{
		if (playerAnimator)
		{
			playerAnimator.SetBool(AnimStrings.isWalking, true);
			playerAnimator.SetBool(AnimStrings.isRunning, true);
		}
		while (playerTransform.position.x < endingDetectionPos.position.x)
		{
			if (playerRb)
			{
				playerRb.velocity = new Vector2(8f, playerRb.velocity.y);
				if (playerTransform)
				{
					if (playerTransform.localScale.x < 0)
					{ playerTransform.localScale *= new Vector2(-1, 1); }
				}
			}
			yield return null;
		}

		if (playerAnimator) { playerAnimator.SetBool(AnimStrings.isWalking, false); }


		// fade in (white color) duration2s
		fadeLayerFading.SetColor(new Color(1f, 1f, 1f, 0f));
		fadeLayerFading.StartFadeInAndOut(3f);

		// move scene to game clear
		yield return new WaitForSeconds(3.0f);

		// enable Thanks component
		thanks.SetActive(true);

		this.GetComponent<GameClearManager>().enabled = true;

		// press Enter back to TitleScene
		CameraInput.enabled = true;
	}

	/*
	 * SavePoint
	 */

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
		yield return new WaitForSeconds(2f);
		fadeLayerFading.StartFadeInAndOut();
		yield return new WaitForSeconds(1f);
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

	/*
	 * GameOver Animation
	 */

	private IEnumerator GameOver()
	{
		gameOverFading.StartFadeIn();
		SoundManager.Instance.StopBGM(BGMSoundData.BGM.PlayScene);
		SoundManager.Instance.PlaySE(SESoundData.SE.GameOver);
		yield return new WaitForSeconds(2f);
		fadeLayerFading.StartFadeIn();
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("TitleScene");

	}

	/*
	 * OpeningAnimation
	 */
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

	/*
	 * AirWall Events
	 */
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

	/*
	 * Exit
	 */
	public void Exit(InputAction.CallbackContext context)
	{
		if (context.started)
		{

#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
			Debug.Log(this.name + ":" + this.GetType() + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif

#if (UNITY_EDITOR)
			UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
			Application.Quit();
#elif (UNITY_WEBGL)
			SceneManager.LoadScene("QuitScene");
#endif
		}
	}
}
