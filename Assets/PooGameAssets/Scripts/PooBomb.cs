using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooBomb : MonoBehaviour {
	public GameObject bomb;
	public float Speed;
	public GameObject Explosion;
	public static PooBomb Instance { get; private set; }
	
	float accel = 0f;

	// Use this for initialization
	void Start () {
		Instance = this;
		this.transform.position = new Vector3(Random.Range(-Screen.width / 100, Screen.width / 100), 11f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (PooGameManager.Instance.isPlaying)
		{
			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - Speed*accel, 0);
			accel += 0.001f;
		}
	}

	public static void GenerateBomb()
	{
		GameObject go = Instantiate(PooPrefabManager.Instance.Bomb);
		go.SetActive(true);
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
			GameObject anim = Instantiate(PooPrefabManager.Instance.Explosion);
			anim.transform.position = this.transform.position + new Vector3(0, 0.2f, 0);
			anim.SetActive(true);
			Destroy(this.gameObject);

	}
}
