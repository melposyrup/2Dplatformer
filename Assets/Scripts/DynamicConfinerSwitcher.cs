using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicConfinerSwitcher : MonoBehaviour
{
	public List<Collider2D> confiningShapes;
	private CinemachineConfiner confiner;
	private int currentIndex = 0; 

	void Start()
	{
		confiner = GetComponent<CinemachineConfiner>();
		if (confiningShapes.Count > 0)
		{
			confiner.m_BoundingShape2D = confiningShapes[currentIndex];
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			currentIndex = (currentIndex + 1) % confiningShapes.Count;
			confiner.m_BoundingShape2D = confiningShapes[currentIndex];
		}
	}
}

