using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutSpriteRenderer : MonoBehaviour
{
	public float fadeDuration = 1f;

	Animator animator;
	SpriteRenderer spriteRenderer;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	private void Start()
	{
		spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (animator) { animator.enabled = false; }
			StartCoroutine(FadeRemove());
		}
	}

	private System.Collections.IEnumerator FadeRemove()
	{
		float fadeTime = fadeDuration;
		float timeElapsed = 0f;

		Color startColor = spriteRenderer.color;
		Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

		while (timeElapsed < fadeTime)
		{
			float t = timeElapsed / fadeTime;
			spriteRenderer.color = Color.Lerp(startColor, endColor, t);
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		spriteRenderer.color = endColor;
	}
}
