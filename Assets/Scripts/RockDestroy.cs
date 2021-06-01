using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDestroy : MonoBehaviour
{
	public bool getLarge = false;
	public float shieldTime = 1f;
	public float speed = 2f;
	float scaleX = 1f;
	float scaleY = 1f;
	// Start is called before the first frame update
	void Start()
    {
		this.gameObject.SetActive(false);
		scaleX = this.transform.localScale.x;
		scaleY = this.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
		if (getLarge)
		{
			scaleX += Time.deltaTime * speed;
			scaleY += Time.deltaTime * speed;
			this.transform.localScale = new Vector3(scaleX, scaleY, 1);
		}
	}

	public void ActivateShieldDestroy(Vector3 position)
	{
		transform.position = position;
		this.gameObject.SetActive(true);
		getLarge = true;
		StartCoroutine(DisableShieldDestroy());
	}

	IEnumerator DisableShieldDestroy()
	{
		yield return new WaitForSeconds(shieldTime);
		getLarge = false;
		scaleX = 1;
		scaleY = 1;
		this.transform.localScale = new Vector3(scaleX, scaleY, 1);
		this.gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Rock"))
		{
			collision.gameObject.SetActive(false);
			GameManager.instance.SetScoreText(5);
		}
	}
}
