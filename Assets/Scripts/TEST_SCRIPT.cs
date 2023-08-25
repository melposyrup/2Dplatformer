using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_SCRIPT : MonoBehaviour
{
	public GameObject loadingPrefab;
	FadingAnim loadingFading;

	public GameObject fadeLayerPrefab;
	FadingAnim fadeLayerFading;

	private void Awake()
	{

		loadingFading = loadingPrefab.GetComponent<FadingAnim>();
		fadeLayerFading = fadeLayerPrefab.GetComponent<FadingAnim>();

	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			SoundManager.Instance.PlaySE(SESoundData.SE.Enter);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			loadingFading.StartFadeInAndOut();



		}
		if (Input.GetKeyDown(KeyCode.S))
		{

			fadeLayerFading.StartFadeInAndOut();

		}


	}
}
