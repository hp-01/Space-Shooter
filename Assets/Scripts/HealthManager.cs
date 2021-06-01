using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	public static HealthManager instance;
	public static HealthManager Instance { get { return instance; } }
	public Camera cam;
	private float spaceHeight;
	private float spaceWidth;
	public GameObject healthPrefab;
	private List<GameObject> healths;


	private void Awake()
	{
		instance = this;
		healths = new List<GameObject>(5);
	}

	// Start is called before the first frame update
	void Start()
	{
		spaceHeight = 2f * cam.orthographicSize;
		spaceWidth = spaceHeight * cam.aspect;
		spaceHeight /= 2;
		spaceWidth /= 2;

		for (int i = 0; i <= 5; i++)
		{
			GameObject health = Instantiate(healthPrefab);
			health.SetActive(false);
			healths.Add(health);
		}
	}

	float lt = 0f;
	// Update is called once per frame
	void Update()
	{
		lt += Time.deltaTime;
		lt = Mathf.Clamp(lt, 0, 10);
		if (lt == 10)
		{
			GetRock();
			lt = 0;
		}
	}

	public GameObject GetRock()
	{
		foreach (GameObject r in healths)
		{
			if (!r.activeInHierarchy)
			{
				float w = UnityEngine.Random.Range(-spaceWidth, spaceWidth);
				float h = UnityEngine.Random.Range(-spaceHeight, spaceHeight);
				r.transform.position = new Vector3(w, h, 0);
				r.SetActive(true);
				return r;
			}
		}
		return null;
	}

	internal void DestroyAll()
	{
		foreach (GameObject r in healths)
		{
			if (r.activeInHierarchy)
			{
				r.SetActive(false);
			}
		}
	}
}
