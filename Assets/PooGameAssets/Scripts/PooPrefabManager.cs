using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooPrefabManager : MonoBehaviour
{
	public static PooPrefabManager Instance { private set; get; }
	public GameObject Bomb;
	public GameObject Explosion;

	public void Awake()
	{
		Instance = this;
	}
}
