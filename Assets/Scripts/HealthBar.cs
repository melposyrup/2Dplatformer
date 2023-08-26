using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{


	public Slider healthSlider;
	public TMP_Text healthBarText;

	Damageable playerDamagebale;

	private void Awake()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		playerDamagebale = player.GetComponent<Damageable>();

		if (player == null) { Debug.Log("No 'Player' tag found in the scene."); }
	}


	private void Start()
	{

		healthSlider.value = CalculateSliderPercentage(playerDamagebale.Health, playerDamagebale.MaxHealth);
		healthBarText.text = "HP:" + playerDamagebale.Health + "/" + playerDamagebale.MaxHealth;
	}



	private float CalculateSliderPercentage(float currentHealth, float maxHealth)
	{
		return currentHealth / maxHealth;
	}

	private void OnPlayerHealthChanged(int newHealth, int maxHealth)
	{
		healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
		healthBarText.text = "HP" + newHealth + "/" + maxHealth;
	}

	// Binding to Player damageable.cs UnityEvent
	private void OnEnable()
	{
		playerDamagebale.healthChanged.AddListener(OnPlayerHealthChanged);
	}

	private void OnDisable()
	{
		playerDamagebale.healthChanged.RemoveListener(OnPlayerHealthChanged);
	}

}
