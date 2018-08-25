using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreHandler : MonoBehaviour {
	public Canvas[] teamCanvas;
	private int maxScore = 10;
	public float startTime, timeLeave, maxTime;
	public Text timer;

	public void SetUp(float p_startTime, float p_maxTime) {
		startTime = p_startTime;
		maxTime = p_maxTime;
	}

	private Canvas FindTeamCanvas(TeamHolder.Team p_team) {
		foreach(Canvas canvas in teamCanvas) {
			string id = (canvas.name.Substring(canvas.name.Length - 1, 1));
			if (id == p_team._id) return canvas;
		};
		return null;
	}

	public void SetScoreToUI(TeamHolder.Team p_team, int p_score, bool isIncremental = true) {
		Canvas canvas = FindTeamCanvas( p_team );
		if (canvas == null) return;

		Text scoreText = canvas.transform.Find("field").GetComponent<Text>();
		Slider sliderUI = canvas.transform.Find("score_bar").GetComponent<Slider>();

		if (isIncremental)
			p_team.score += p_score;
		else 
			p_team.score = p_score;
		scoreText.text = p_team.score + "";

		sliderUI.value = ((float)p_team.score / maxScore );

		if (p_team.score / maxScore >= 1) {
			//Game Over this team win;
			TeamDroperManager.instance.GameOver("Team " + p_team._id +" Win!!");
		}
	}

	void Update() {
		if (startTime < 0) return; 
		if (timeLeave <= 0) {
			TeamHolder.Team highestTeam = TeamDroperManager.instance._teamHolder.FindHighestTeam();
			TeamDroperManager.instance.GameOver("Time ups" +", Team " + highestTeam._id +" Win!!");
			return;
		}

		UpdateTimer();
	}

	private void UpdateTimer() {
		float passTime = Time.time - startTime;
		timeLeave = maxTime - passTime;
		
		float minutes =  Mathf.Floor(timeLeave / 60);
		int second = Mathf.FloorToInt(timeLeave % 60);

		timer.text = minutes + " : " + (( second < 10 ) ? ("0"+second) : second.ToString());
	}

}
