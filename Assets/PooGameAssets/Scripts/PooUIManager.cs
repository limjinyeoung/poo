using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooUIManager : MonoBehaviour {
	public static PooUIManager Instance { get; private set; }
	public TweenAlpha Popup;
	public TweenAlpha GameOverPopup;
	public GameObject HighScorePanel;
	public GameObject Count0;
	public GameObject Count1;
	public GameObject Count2;
	public GameObject Count3;
	public UILabel ScoreLabel;
	public UILabel HighScoreLabel;
	public UILabel TimeLabel;
	public float time = 0;
	public AudioClip ClickSound;
	public AudioClip BeatSound;

	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		Instance = this;
		PrintCounting();
		audioSource = GetComponent<AudioSource>();
	}

	public void PrintCounting()
	{
		Count3.SetActive(true);
		StartCoroutine(Count());
	}

	IEnumerator Count()
	{
		yield return new WaitForSeconds(1);
		Count3.SetActive(false);
		Count2.SetActive(true);
		yield return new WaitForSeconds(1);
		Count2.SetActive(false);
		Count1.SetActive(true);
		yield return new WaitForSeconds(1);
		Count1.SetActive(false);
		Count0.SetActive(true);
		yield return new WaitForSeconds(1);
		Count0.SetActive(false);
		PooGameManager.Instance.isPlaying = true;
	}

	// Update is called once per frame
	void Update()
	{
		if(PooGameManager.Instance.isPlaying)
		{
			time += Time.deltaTime;
			TimeLabel.text = "점수 : " + (int)time;
		}
	}

	public void PrintScore()
	{
		ScoreLabel.text = "점수 : " + (int)time;
		HighScoreLabel.text = "최고점수 : " + GetHighScore();
	}

	public int GetHighScore()
	{
		if (PlayerPrefs.HasKey("HighScore"))
		{
			if (PlayerPrefs.GetInt(("HighScore")) < (int)time)
			{
				PlayerPrefs.SetInt("HighScore", (int)time);
				StartCoroutine(PrintHighScore());
			}
		}
		else
		{
			PlayerPrefs.SetInt("HighScore", (int)time);
		}

		PlayerPrefs.Save();

		return PlayerPrefs.GetInt("HighScore");
	}

	IEnumerator PrintHighScore()
	{
		yield return new WaitForSeconds(2);
		HighScorePanel.SetActive(true);
		audioSource.PlayOneShot(BeatSound, 1f);
	}

	public void OnClickListenerPauseButton()
	{
		audioSource.PlayOneShot(ClickSound, 1f);
		PooGameManager.Instance.PauseGame();
	}

	public void OnClickListenerResumeButton()
	{
		audioSource.PlayOneShot(ClickSound, 1f);
		PooGameManager.Instance.ResumeGame();
	}

	public void OnClickListenerResetButton()
	{
		audioSource.PlayOneShot(ClickSound, 1f);
		PooGameManager.Instance.ResetGame();
	}

	public void OnHighScoreResetButtonListener()
	{
		audioSource.PlayOneShot(ClickSound, 1f);
		PlayerPrefs.SetInt("HighScore", 0);
		HighScoreLabel.text = "최고점수 : " + "0";
	}

	public void OnClickListenerMenuButton()
	{
		//PooGameManager.Instance.BackToMenu();
	}

	public void OpenPopUpPanel()
	{
		Popup.enabled = true;
		Popup.PlayForward();
	}

	public void ClosePopUpPanel()
	{
		Popup.PlayReverse();
	}

	public void OpenGameOverPanel()
	{
		GameOverPopup.enabled = true;
		GameOverPopup.PlayForward();
	}
}
