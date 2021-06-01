using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPoolingManager : MonoBehaviour
{
	private static LaserPoolingManager instance;
	public static LaserPoolingManager Instance { get { return instance; } }
	public GameObject laserPrefab;
	private List<GameObject> lasers;

	private void Awake()
	{
		instance = this;
		lasers = new List<GameObject>(20);
		for (int i = 0; i <= 20; i++)
		{
			GameObject prefabInstance = Instantiate(laserPrefab);
			prefabInstance.transform.SetParent(transform);
			prefabInstance.SetActive(false);
			lasers.Add(prefabInstance);
		}
	}

	public GameObject GetBullet(bool shotByPlayer)
	{
		foreach (GameObject laser in lasers)
		{
			if (!laser.activeInHierarchy)
			{
				laser.SetActive(true);
				laser.GetComponent<Laser>().ShotByPlayer = shotByPlayer;
				return laser;
			}
		}
		GameObject prefabInstance = Instantiate(laserPrefab);
		prefabInstance.transform.SetParent(transform);
		prefabInstance.GetComponent<Laser>().ShotByPlayer = shotByPlayer;
		lasers.Add(prefabInstance);

		return prefabInstance;
	}
}
