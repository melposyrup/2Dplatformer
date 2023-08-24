using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInImage : MonoBehaviour
{
	public float fadeDuration = 1f;

	Image image;
	private void Awake()
	{
		image = GetComponent<Image>();
	}
	private void Start()
	{
		image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
	}

	public void StartFadeRemove()
	{
		StartCoroutine(FadeRemove());
	}

	private System.Collections.IEnumerator FadeRemove()
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

}
