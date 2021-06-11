using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRock : MonoBehaviour
{
	public GameObject smallRock;
	public float speed = 6.0f;
	public int score = 5;
	public float rotateSpeed = 45f;

	private void OnEnable()
	{
		rotateSpeed = Random.Range(-45f, 45f);
	}

	private void Start()
	{
		Vector3 euler = new Vector3();
		euler.z = Random.Range(0.0f, 360.0f);
		transform.eulerAngles = euler;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position += transform.right * speed * Time.deltaTime;
		smallRock.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
		GameManager.instance.WarpAroundWorld(transform);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Laser"))
		{
			other.gameObject.SetActive(false);
			gameObject.SetActive(false);
			Destroy(this);
			GameManager.instance.SetScoreText(score);
		}
	}
}
