using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeRock : MonoBehaviour
{
	public GameObject smallRock;
	public GameObject largeRock;
	public float speed = 3.0f;
	public int score = 10;
	private float rotateSpeed = 45f;

	private void OnEnable()
	{
		Vector3 euler = new Vector3();
		euler.z = Random.Range(0.0f, 360.0f);
		transform.eulerAngles = euler;
		rotateSpeed = Random.Range(-45f, 45f);	
	}

	// Update is called once per frame
	void Update()
	{
		transform.position += transform.right * speed * Time.deltaTime;
		largeRock.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
		GameManager.instance.WarpAroundWorld(transform);
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Laser"))
		{
			other.gameObject.SetActive(false);
			for(int i=0; i < 2; i++)
			{
				GameObject prefabInstance = Instantiate(smallRock);
				prefabInstance.transform.position = this.transform.position;
				prefabInstance.transform.right = transform.right;
			}
			gameObject.SetActive(false);
			GameManager.instance.SetScoreText(score);
		}

	}
}

