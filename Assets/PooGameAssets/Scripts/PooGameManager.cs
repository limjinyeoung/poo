using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PooGameManager : MonoBehaviour {
	public float regenTime;
	public static PooGameManager Instance { get; private set; }
	public bool isPlaying;
	public AudioClip BGMSound;
	public float UpgradeRegenSpeed;

	float upgradeRegenTime;
	float count = 0.0f;
	private AudioSource bgmSource;
    public static bool Loaded;
    // Use this for initialization
    void Start () {
		Time.timeScale = 1f;
		Instance = this;
		isPlaying = false;
		bgmSource = GetComponent<AudioSource>();
		bgmSource.PlayOneShot(BGMSound, 1f);
		upgradeRegenTime = UpgradeRegenSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlaying)
		{
			count += Time.deltaTime;
			if (count >= regenTime)
			{
				count -= regenTime;
				PooBomb.GenerateBomb();
			}

			if (PooUIManager.Instance.time > upgradeRegenTime)
			{
				upgradeRegenTime += UpgradeRegenSpeed;
				regenTime *= 0.9f;
			}
		}
	}

	public void GameOver()
	{
		PooUIManager.Instance.OpenGameOverPanel();
		PooUIManager.Instance.PrintScore();
		bgmSource.Stop();
		isPlaying = false;
	}

	public void PauseGame()
	{
		PooUIManager.Instance.OpenPopUpPanel();
		isPlaying = false;
	}

	public void ResumeGame()
	{
		PooUIManager.Instance.ClosePopUpPanel();
		isPlaying = true;
	}

	public void ResetGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene("MenuScene");
	}
}
