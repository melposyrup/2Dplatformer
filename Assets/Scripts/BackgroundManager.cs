using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	public GameObject mainCamera;

	private void Awake()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

	}
	private void Start()
	{
		Vector3 cameraPos = mainCamera.GetComponent<Transform>().position;
		transform.position = new Vector3(cameraPos.x, cameraPos.y, 0f);
	}
}
