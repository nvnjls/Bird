using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public static int S_Score = 0;

	public Text scoreText;
	public Text endScoreText;
	public Text highScoreText;
	//public Text OutInText;
	public GameObject GetOut;
	public GameObject GetIn;
	public GameObject gameOverPanel;

	void Update()
	{
		scoreText.text = " Score : " + S_Score.ToString();
	}

	public void GameOver()
	{	gameOverPanel.SetActive(true);
		endScoreText.text = S_Score.ToString();
		highScoreText.text = "Best : " + PlayerPrefs.GetInt("HIGH",0).ToString();
	}
	public void Out()
	{
		GetIn.SetActive(false);
		GetOut.SetActive(true);
	}

	public void In()
	{
		GetIn.SetActive(true);
		GetOut.SetActive(false);
	}


}
