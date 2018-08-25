using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamHolder : MonoBehaviour {
	List<Team> teams = new List<Team>();

	void SetUp() {

	}

	public Team FindTeam(Player p_player) {
		return null;
	}

	public class Team {
		public string _id;
		public GameObject[] players;

		public Team(string pID, GameObject[] p_players) {
			_id = pID;
			players = p_players;
		}
	}

	public class Player {
		public string _id;
		public Team _team;
		public GameObject gameObject;
	}
}
