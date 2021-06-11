using System;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
	public static RockManager instance;
	public static RockManager Instance { get { return instance; } }
	public Camera cam;
	private float spaceHeight;
	private float spaceWidth;
	public GameObject largeRockPrefab;
	public GameObject smallRockInstance;
	public List<GameObject> points;
	private List<GameObject> rocks;

	private void Awake()
	{
		instance = this;
		rocks = new List<GameObject>(20);
		for (int i = 0; i <= 20; i++)
		{
			GameObject rock = Instantiate(largeRockPrefab);
			int j = UnityEngine.Random.Range(1, 4);
			rock.transform.position = points[j].transform.position;
			rock.transform.right = points[j].transform.right;
			rock.SetActive(false);
			rocks.Add(rock);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		spaceHeight = 2f * cam.orthographicSize;
		spaceWidth = spaceHeight * cam.aspect;
		spaceHeight /= 2;
		spaceWidth /= 2;
		points[0].transform.position = new Vector3(-spaceWidth+0.1f, 0, 0);
		points[1].transform.position = new Vector3(0, spaceHeight - 0.1f,0);
		points[2].transform.position = new Vector3(spaceWidth-0.1f, 0, 0);
		points[3].transform.position = new Vector3(0, -spaceHeight+0.1f,0);
	}

	float lt = 0f;
    // Update is called once per frame
    void Update()
    {
		lt += Time.deltaTime;
		lt = Mathf.Clamp(lt, 0, 6);
		if(lt == 6)
		{
			GetRock();
			lt = 0;
		}
    }

	public GameObject GetRock()
	{
		foreach(GameObject r in rocks)
		{
			if(!r.activeInHierarchy)
			{
				int rand = UnityEngine.Random.Range(1, 4);
				r.transform.position = points[rand].transform.position;
				r.transform.right = points[rand].transform.right;
				r.SetActive(true);
				return r;
			}
		}
		GameObject rock = Instantiate(largeRockPrefab);
		int j = UnityEngine.Random.Range(1, 4);
		rock.transform.position = points[j].transform.position;
		rock.transform.right = points[j].transform.right;
		rock.SetActive(true);
		rocks.Add(rock);
		return rock;
	}

	internal void DestroyAll()
	{
		foreach (GameObject r in rocks)
		{
			if (r.activeInHierarchy)
			{
				r.SetActive(false);
			}
		}
	}
}
