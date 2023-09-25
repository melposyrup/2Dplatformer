using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class GameClearManager : MonoBehaviour
{
	public static event Action AnyKeyPressed; // Define the event

	private InputAction anyKeyAction;

	// Start listening for the event
	private void OnEnable()
	{
		AnyKeyPressed += LoadTitleScene; // Subscribe to the event

		// Initialize Input Action for Any Key
		anyKeyAction = new InputAction(binding: "<Keyboard>/anyKey");
		anyKeyAction.performed += _ => AnyKeyPressed?.Invoke(); // Trigger the event when any key is pressed
		anyKeyAction.Enable();
	}

	// Stop listening for the event
	private void OnDisable()
	{
		AnyKeyPressed -= LoadTitleScene; // Unsubscribe from the event
		anyKeyAction.Disable();
	}

	// Method to load the title scene
	private void LoadTitleScene()
	{
		SceneManager.LoadScene("TitleScene"); // Replace with your Title Scene name
	}
}
