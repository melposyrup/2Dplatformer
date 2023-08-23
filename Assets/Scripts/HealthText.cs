using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
	public Vector3 moveSpeed = new Vector3(0, 75, 0); //Pixel per second
	public float timeToFade = 1.5f;
	private float timeElapsed;
	private Color startColor;


	RectTransform textTransform;
	TextMeshProUGUI textMeshPro;
	private void Awake()
	{
		textTransform = GetComponent<RectTransform>();
		textMeshPro = GetComponent<TextMeshProUGUI>();
		startColor = textMeshPro.color;
	}



	private void Update()
	{
		textTransform.position += moveSpeed * Time.deltaTime;

		timeElapsed += Time.deltaTime;

		if (timeElapsed < timeToFade)
		{
			float fadeAlpha = startColor.a * (1 - timeElapsed / timeToFade);
			textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
		}
		else
		{
			Destroy(gameObject);
		}
	}

}
