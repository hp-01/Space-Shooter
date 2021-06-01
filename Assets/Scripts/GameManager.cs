using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public GameObject space;
	public Camera cam;
	public GameObject gamePanel;
	float spaceWidth;
	float spaceHeight;
	public int totalScore = 0;
	public Text scoreText;

	public void SetScoreText(int score)
	{
		totalScore += score;
		scoreText.text = "" + totalScore;
	}

	public int totalHealth = 100;
	public Text healthScore;
	public void SetHealthScore(int health)
	{
		totalHealth = health;
		healthScore.text = "" + totalHealth;
	}

	public Text messageText;
	public void SetMessageText(string msg)
	{
		messageText.text = msg;
	}
	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			instance = this;
			Destroy(this);
		}
	}

	private void Start()
	{
		spaceHeight = 2f * cam.orthographicSize;
		spaceWidth = spaceHeight * cam.aspect;
		space.GetComponent<SpriteRenderer>().size = new Vector2(spaceWidth, spaceHeight);
		spaceHeight /= 2;
		spaceWidth /= 2;
	}

	private float healthTimer;
	private void Update()
	{
		if (FindObjectsOfType<LargeRock>().Length <= 0 && FindObjectsOfType<SmallRock>().Length <= 0)
		{
			SetMessageText("WON!!");
			gamePanel.SetActive(true);
			RockManager.Instance.gameObject.SetActive(false);
		}
	}

	public void WarpAroundWorld(Transform t)
	{
		if (t.position.x < -spaceWidth)
		{
			t.position = Vector3.right * spaceWidth;
		}
		if (t.position.x > spaceWidth)
		{
			t.position = Vector3.right * -spaceWidth;
		}
		if (t.position.y < -spaceHeight)
		{
			t.position = Vector3.up * spaceHeight;
		}
		if (t.position.y > spaceHeight)
		{
			t.position = Vector3.up * -spaceHeight;
		}
	}
}
