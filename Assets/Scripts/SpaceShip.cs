using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
	public float maxSpeed = 2f;
	public float velocity = 0f;

	public GameObject fire;
	public GameObject shield;
	public GameObject warp;
	public GameObject shootPoint;
	public GameObject explosion;
	public float rotationSpeed = 120f;
	public Camera cam;
	public GameObject gamePanel;
	float spaceWidth;
	float spaceHeight;

	float width;
	float height;

	bool doDamage = true;
	int health = 100;
	public void SetHealth()
	{
		health += 35;
		health = Mathf.Clamp(health, 0, 100);
	}
	private bool isDestroyed = false;
	private bool fireButton = false;
	private bool rightButton = false;
	private bool upButton = false;
	private bool leftButton = false;

	// Start is called before the first frame update
	void Start()
    {
		spaceHeight = 2f * cam.orthographicSize;
		spaceWidth = spaceHeight * cam.aspect;
		spaceHeight /= 2;
		spaceWidth /= 2;
		width = GetComponent<Renderer>().bounds.size.x;
		height = GetComponent<Renderer>().bounds.size.y;
		RockManager.Instance.GetRock();
		gamePanel.SetActive(false);
    }

	public float time = 0f;
	public GameObject RockDestroy;
    // Update is called once per frame
    void Update()
    {
		if (isDestroyed) return;
		
		time += Time.deltaTime;
		if((Input.GetKey(KeyCode.Space) || fireButton) && time > 0.3f)
		{
			StartCoroutine(LaserFire());
			time = 0f;
		}

		UpdateMovement(Input.GetKey(KeyCode.UpArrow) || upButton);
		UpdateDirection(Input.GetKey(KeyCode.RightArrow) || rightButton, Input.GetKey(KeyCode.LeftArrow) || leftButton);
		if (transform.position.x < -spaceWidth)
		{
			StartCoroutine(WarpAnimation());
			transform.position = Vector3.right * spaceWidth;
		}
		if(transform.position.x > spaceWidth)
		{
			StartCoroutine(WarpAnimation());
			transform.position = Vector3.right * -spaceWidth;
		}
		if(transform.position.y < -spaceHeight)
		{
			StartCoroutine(WarpAnimation());
			transform.position = Vector3.up * spaceHeight;
		}
		if(transform.position.y > spaceHeight)
		{
			StartCoroutine(WarpAnimation());
			transform.position = Vector3.up * -spaceHeight;
		}
	}

	IEnumerator LaserFire()
	{
		GameObject laserObject = LaserPoolingManager.Instance.GetBullet(true);
		laserObject.transform.position = shootPoint.transform.position + shootPoint.transform.right;
		laserObject.transform.right = shootPoint.transform.right;
		yield return new WaitForSeconds(0f);
	}

	IEnumerator WarpAnimation()
	{
		warp.SetActive(true);
		warp.transform.position = transform.position;
		warp.GetComponent<Animator>().Play("warp");
		yield return new WaitForSeconds(1);
		warp.SetActive(false);
	}

	void UpdateMovement(bool isPressed)
	{
		if (isPressed) velocity += Time.deltaTime;
		else velocity -= Time.deltaTime / 2;
		velocity = Mathf.Clamp(velocity, 0, maxSpeed);
		transform.position += transform.right * velocity * Time.deltaTime;
		fire.SetActive(isPressed);
	}

	void UpdateDirection(bool right, bool left)
	{
		float rotationDirection = 0;
		if (right) rotationDirection = -rotationSpeed;
		if (left) rotationDirection = rotationSpeed;
		
		Quaternion rotation = new Quaternion(0, 0, rotationDirection * Time.deltaTime ,1);
		transform.Rotate(new Vector3(0,0,1) * rotationDirection * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Rock"))
		{
			health -= 35;
			health = Mathf.Clamp(health, 0, 100);
			shield.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, health/100f);
			if(health == 0)
			{
				isDestroyed = true;
				gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0f);
				explosion.SetActive(true);
				explosion.GetComponent<Animator>().Play("explosion");
				fire.SetActive(false);
				GameManager.instance.SetMessageText("LOSE:(");
				gamePanel.SetActive(true);
			}
			GameManager.instance.SetHealthScore(health);
		}

		if (collision.gameObject.CompareTag("Health"))
		{
			SetHealth();
			GameManager.instance.SetHealthScore(health);
			collision.gameObject.SetActive(false);
		}

		if(collision.gameObject.CompareTag("RockDestroy"))
		{
			collision.gameObject.SetActive(false);
			RockDestroy.GetComponent<RockDestroy>().ActivateShieldDestroy(transform.position);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Rock"))
		{
			doDamage = true;
		}
	}

	public void RetryButton()
	{
		health = 100;
		shield.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
		explosion.SetActive(false);
		fire.SetActive(true);
		RockManager.Instance.gameObject.SetActive(true);
		RockManager.Instance.DestroyAll();
		RockDestroyManager.Instance.DestroyAll();
		foreach(SmallRock smallRock in FindObjectsOfType<SmallRock>())
		{
			Destroy(smallRock.gameObject);
		}
		HealthManager.Instance.DestroyAll();
		RockManager.Instance.GetRock();
		GameManager.instance.totalScore = 0;
		GameManager.instance.SetScoreText(0);
		GameManager.instance.SetHealthScore(health);
		gamePanel.SetActive(false);	
		isDestroyed = false;
	}

	public void FireButtonEnter()
	{
		fireButton = true;
	}

	public void FireButtonExit()
	{
		fireButton = false;
	}

	public void UpButtonEnter()
	{
		upButton = true;
	}

	public void UpButtonExit()
	{
		upButton = false;
	}

	public void LeftButtonEnter()
	{
		leftButton = true;
	}

	public void LeftButtonExit()
	{
		leftButton = false;
	}

	public void RightButtonEnter()
	{
		rightButton = true;
	}

	public void RightButtonExit()
	{
		rightButton = false;
	}
}
