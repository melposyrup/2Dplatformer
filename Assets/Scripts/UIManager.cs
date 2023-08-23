using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public GameObject damageTextPrefab;
	public GameObject healthTextPrefab;

	public Canvas gameCanvus;

	private void Awake()
	{
		gameCanvus = FindObjectOfType<Canvas>();

	}

	private void OnEnable()
	{
		CharacterEvents.characterDamaged += CharacterTookDamage;
		CharacterEvents.characterHealed += CharacterHealed;
	}

	private void OnDisable()
	{
		CharacterEvents.characterDamaged -= CharacterTookDamage;
		CharacterEvents.characterHealed -= CharacterHealed;
	}

	public void CharacterTookDamage(GameObject character, int damageReceived)
	{
		// Create text at character hit
		Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

		TMP_Text tempText =
			Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvus.transform).GetComponent<TMP_Text>();

		tempText.text = damageReceived.ToString();

	}

	public void CharacterHealed(GameObject character, int healthRestored)
	{
		// Create text at character healed

		Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

		TMP_Text tempText =
			Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvus.transform).GetComponent<TMP_Text>();

		tempText.text = healthRestored.ToString();


	}

}
