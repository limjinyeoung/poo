using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooPlayerDust : MonoBehaviour {
	SpriteRenderer dustSprite;
	Animator dustAnim;
	bool isPlaying = false;
	// Use this for initialization

	void Start () {
		dustSprite = GetComponent<SpriteRenderer>();
		dustAnim = GetComponent<Animator>();
		dustAnim.speed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (PooGameManager.Instance.isPlaying&&!isPlaying)
		{
			isPlaying = true;
			dustAnim.speed = 1.0f;
		}
		else if(PooGameManager.Instance.isPlaying && isPlaying)
		{
			if (Input.GetKeyDown("left")) dustSprite.flipX = false;
			if (Input.GetKeyDown("right")) dustSprite.flipX = true;
		}
		else
		{
			isPlaying = false;
			dustAnim.speed = 0.0f;
		}
	}
}
