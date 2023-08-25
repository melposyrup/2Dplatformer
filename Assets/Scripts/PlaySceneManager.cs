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
	FadeInImage gameOverFadeIn;

	public GameObject LoadingPrefab;
	FadeInImage LoadingFadeIn;

	public float delay = 3f;

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

		gameOverFadeIn = gameOverPrefab.GetComponent<FadeInImage>();
		LoadingFadeIn = LoadingPrefab.GetComponent<FadeInImage>();

	}

	public void CheckSaveData(GameObject lastSavePoint)
	{
		if (lastSavePoint)
		{
			LoadingFadeIn.StartFadeIn();
			StartCoroutine(LoadSave(lastSavePoint));
		}
		else
		{
			gameOverFadeIn.StartFadeIn();
			StartCoroutine(GameOver());
		}

	}
	private IEnumerator LoadSave(GameObject lastSavePoint)
	{
		yield return new WaitForSeconds(delay);

		if (lastSavePoint)
		{
			Vector3 pos = lastSavePoint.GetComponent<Transform>().position;
			playerDamageable.Health = 100;
			playerDamageable.IsAlive=true;
			playerTransform.position = pos;
			playerInput.enabled = true;
		}
		LoadingFadeIn.ResetColor();

	}

	private IEnumerator GameOver()
	{
		yield return new WaitForSeconds(delay);

		SceneManager.LoadScene("TitleScene");

	}


}
