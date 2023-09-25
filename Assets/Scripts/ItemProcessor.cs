using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemProcessor : MonoBehaviour
{
	public static ItemProcessor Instance; // Singleton

	public GameObject coinPrefab;
	public string fileName = "coin_coordinates.txt";

	// Awake to initialize Singleton
	//private void Awake()
	//{
	//	if (Instance == null)
	//	{
	//		Instance = this;
	//		DontDestroyOnLoad(gameObject);
	//	}
	//	else
	//	{
	//		Destroy(gameObject);
	//	}
	//}

	// Export coordinates to a file
	public void ExportCoordinates()
	{
		GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
		List<string> coordinatesList = new List<string>();

		foreach (GameObject coin in coins)
		{
			Vector3 pos = coin.transform.position;
			string coordinate = $"{pos.x},{pos.y},{pos.z}";
			coordinatesList.Add(coordinate);
		}

		File.WriteAllLines(fileName, coordinatesList.ToArray());
		Debug.Log("ExportCoordinates() Succeed.");
	}

	// Import coordinates from a file
	public void ImportCoordinates()
	{
		GameObject coinParent = GameObject.Find("CoinInstance"); // Find the parent GameObject

		if (coinParent == null)
		{
			Debug.LogWarning("Parent GameObject 'CoinInstance' not found. Creating one.");
			coinParent = new GameObject("CoinInstance");
		}

		if (File.Exists(fileName))
		{
			string[] coordinatesList = File.ReadAllLines(fileName);
			foreach (string coordinate in coordinatesList)
			{
				string[] parts = coordinate.Split(',');
				Vector3 pos = new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));
				GameObject newCoin = Instantiate(coinPrefab, pos, Quaternion.identity);
				newCoin.transform.SetParent(coinParent.transform);  // Set the parent to 'CoinInstance'
			}
			Debug.Log("ImportCoordinates() Succeed.");
		}
		else
		{
			Debug.LogWarning("File does not exist");
		}
	}

	private void Start()
	{

		ImportCoordinates();

	}
}
