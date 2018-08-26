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
		timeLeave = 0;
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

		Image sliderUI = canvas.transform.Find("score_bar/mask/score_value").GetComponent<Image>();

		if (isIncremental)
			p_team.score += p_score;
		else 
			p_team.score = p_score;

		sliderUI.fillAmount = ((float)p_team.score / maxScore );

		if (p_team.score / (float)maxScore >= 1) {
			//Game Over this team win;
			string redColorCode = "#e14a36ff";
			TeamDroperManager.instance.GameOver("<color="+redColorCode+"Team " + p_team._id +">\nWin!!");
		}
	}

	void LateUpdate() {
		if (startTime < 0) return; 
		if (timeLeave < 0 && TeamDroperManager.instance._gameState == TeamDroperManager.GameState.Start) {
			TeamHolder.Team highestTeam = TeamDroperManager.instance._teamHolder.FindHighestTeam();
			string teamName = "No TEAM";
			if (highestTeam != null)
				teamName = " Team " + highestTeam._id;

			TeamDroperManager.instance.GameOver("Time ups" +", "+ teamName + " Win!!");
			return;
		}

		if (timeLeave < 0 && TeamDroperManager.instance._gameState == TeamDroperManager.GameState.PickTeam) {
			TeamDroperManager.instance._camera._zoomState = CameraManager.ZoomState.ZoomOut;
			
			//Pick team time end
			SetUp(Time.time, 1);
			TeamDroperManager.instance._gameState = TeamDroperManager.GameState.Start;
			TeamDroperManager.instance._teamHolder.SetUp();
		}

		if (TeamDroperManager.instance._gameState != TeamDroperManager.GameState.End)
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
