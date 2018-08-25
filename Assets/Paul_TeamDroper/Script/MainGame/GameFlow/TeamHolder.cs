using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TeamHolder : MonoBehaviour {
	List<Team> teams = new List<Team>();

	void SetUp(List<Team> p_teams) {
		teams = p_teams;
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
