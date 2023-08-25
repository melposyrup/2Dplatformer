using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingAnim : MonoBehaviour
{
	public float fadeDuration = 1f;

	Image image;
	private void Awake()
	{
		image = GetComponent<Image>();
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

	public void StartFadeIn()
	{
		StartCoroutine(AlphaIncrease());
	}

	private System.Collections.IEnumerator AlphaIncrease()
	{
		float fadeTime = fadeDuration;
		float timeElapsed = 0f;

		Color startColor = image.color;
		Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

		while (timeElapsed < fadeTime)
		{
			float t = timeElapsed / fadeTime;
			image.color = Color.Lerp(startColor, endColor, t);
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		image.color = endColor;
	}

	public void ResetAlpha(float value)
	{
		image.color = new Color(image.color.r, image.color.g, image.color.b, value);
	}

	public void StartFadeInAndOut()
	{
		StartCoroutine(AlphaIncreaseThenDecrease());
	}
	private System.Collections.IEnumerator AlphaIncreaseThenDecrease()
	{
		float fadeTime = fadeDuration;
		float timeElapsed = 0f;

		Color startColor = image.color;
		Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

		while (timeElapsed < fadeTime)
		{
			float t = timeElapsed / fadeTime;
			image.color = Color.Lerp(startColor, endColor, t);
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		image.color = endColor;


		timeElapsed = 0f;

		while (timeElapsed < fadeTime)
		{
			float t = timeElapsed / fadeTime;
			image.color = Color.Lerp(endColor, startColor, t);
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		image.color = startColor;
	}

}
