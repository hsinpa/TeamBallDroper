using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TeamHolder : MonoBehaviour {
	[SerializeField]
	List<Team> teams = new List<Team>();

	[SerializeField]
	FloorDetector[] _floorDetector;

	Vector3[] teamOriginalPos;
	Transform _tranTeamHolder;

	public void SetUp(Transform p_tranTeamHolder = null) {
		for(int i = 0; i < _floorDetector.Length; i++) {
			_floorDetector[i].RenewFloorColor();
		}

		//Reset team
		if (p_tranTeamHolder != null)
			_tranTeamHolder = p_tranTeamHolder;

		if (_tranTeamHolder != null && p_tranTeamHolder != null) {			
			if (teamOriginalPos == null) {
				teamOriginalPos = new Vector3[_tranTeamHolder.childCount];
				for (int i = 0; i < _tranTeamHolder.childCount; i++) {
					teamOriginalPos[i] = _tranTeamHolder.GetChild(i).position;
				}
			} else {
				for (int i = 0; i < _tranTeamHolder.childCount; i++) {
					_tranTeamHolder.GetChild(i).position = teamOriginalPos[i];
				}

			}
		}

	}

	public Team FindTeam(Player p_player) {
		return null;
	}

	//Highest score
	public Team FindHighestTeam() {
		if (teams.Count <= 0) return null;
		List<Team> sortTeam = teams.OrderByDescending(x=>x.score).ToList();
		return sortTeam[0];
	}

	
	// Update is called once per frame
	void Update () {
		if (TeamDroperManager.instance._gameState == TeamDroperManager.GameState.PickTeam) {
			
		}
	}


	public class Team {
		public string _id;
		public GameObject[] players;
		public int score;
		public Color teamColor;
		public Team(string pID, GameObject[] p_players, Color p_teamColor) {
			_id = pID;
			players = p_players;
			score = 0;
			teamColor = p_teamColor;
		}
	}

	public class Player {
		public string _id;
		public Team _team;
		public GameObject gameObject;
	}
}
