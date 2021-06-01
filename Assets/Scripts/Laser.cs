using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	public float speed = 8.0f;
	public float lifeDuration = 2.0f;
	public int damage = 5;

	private float lifeTimer;
	// Start is called before the first frame update

	private bool shotByPlayer;
	public bool ShotByPlayer
	{
		get { return shotByPlayer; }
		set { shotByPlayer = value; }
	}

	void OnEnable()
	{
		lifeTimer = lifeDuration;
	}

	// Update is called once per frame
	void Update()
	{
		// make the bullet move
		transform.position += transform.right * speed * Time.deltaTime;
		lifeTimer -= Time.deltaTime;
		if (lifeTimer <= 0f)
		{
			gameObject.SetActive(false);
		}
		GameManager.instance.WarpAroundWorld(transform);
	}
}
