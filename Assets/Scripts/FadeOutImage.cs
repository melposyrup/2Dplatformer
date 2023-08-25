using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutImage : MonoBehaviour
{
	public float fadeDuration = 1f;

	Image image;
	private void Awake()
	{
		image = GetComponent<Image>();
	}
	private void Start()
	{
		image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
		StartCoroutine(AlphaDecrease());
	}

	public void StartFadeOut()
	{
		StartCoroutine(AlphaDecrease());
	}


	private System.Collections.IEnumerator AlphaDecrease()
	{
		float fadeTime = fadeDuration;
		float timeElapsed = 0f;

		Color startColor = image.color;
		Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

		while (timeElapsed < fadeTime)
		{
			float t = timeElapsed / fadeTime;
			image.color = Color.Lerp(startColor, endColor, t);
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		image.color = endColor;
	}


}
