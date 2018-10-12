using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PooPlayer : MonoBehaviour
{
	public enum Direction { RIGHT, LEFT }
	public float speed;
	public Sprite[] runAnim;

	float destination;
	int animIndex;
	SpriteRenderer sprites;
	float animCount;
    public UILabel LabelBLE;

    // Use this for initialization
    public void Awake()
    {

    }
    void Start()
	{
		animIndex = 0;
		sprites = GetComponent<SpriteRenderer>();
		animCount = 0.0f;
    }

	// Update is called once per frame
    void MoveLeft(){
        if (!PooGameManager.Instance.isPlaying) return;
        if (this.transform.position.x > -3.5)
            SetDirection(Direction.LEFT);

    }
    void MoveRight(){
        if (!PooGameManager.Instance.isPlaying) return;
        if (this.transform.position.x < 3.5)
            SetDirection(Direction.RIGHT);

    }
	void Update()
	{
        if (PooGameManager.Instance.isPlaying)
		{
            if (Input.GetKey("left")) MoveLeft();
            if (Input.GetKey("right")) MoveRight();
			
            this.transform.position += new Vector3((destination - transform.position.x) * 0.1f*speed, 0.0f, 0.0f);

			animCount += Time.deltaTime;
			if (animCount > 0.2f)
			{
				animCount -= 0.2f;
				animIndex++;
				if (animIndex >= runAnim.Length)
				{
					animIndex = 0;
				}
				sprites.sprite = runAnim[animIndex];
			}
		}
	}

	public void SetDirection(Direction direction)
	{
		switch (direction)
		{
			case Direction.RIGHT:
				sprites.flipX = false;
				destination = this.transform.position.x + 0.5f;
				break;
			case Direction.LEFT:
				sprites.flipX = true;
				destination = this.transform.position.x - 0.5f;
				break;
			default:
				Debug.LogError("Direction Input Error");
				break;
		}
	}
	public void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.transform.tag == "Bomb")
		{
			PooGameManager.Instance.GameOver();
		}
	}
}
