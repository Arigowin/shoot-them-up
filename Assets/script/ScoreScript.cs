using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

	public Text scoreText;
	public int score = 2147483647;

	void Start ()
	{
		score = 0;

		SetScoreText ();
	}

	void SetScoreText ()
	{
		scoreText.text = "Score : " + score.ToString ();
	}

	public void ScoreAdd ()
	{
		score++;
		SetScoreText ();
		Spawner nbEnemy = GetComponent<Spawner> ();
		if (score / 10 == nbEnemy.spawnCount)
			nbEnemy.spawnCount++;
		if (score % 10 == 0)
			GetComponent<LevelScript> ().LevelAdd ();
	}
}
