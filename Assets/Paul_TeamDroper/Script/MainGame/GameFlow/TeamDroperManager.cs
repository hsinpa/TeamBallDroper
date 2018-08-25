using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamDroperManager : MonoBehaviour {
	static TeamDroperManager _instance;

	#region MainConnector
		public BallAllocator _ballAllocator;
		public ScoreHandler _scoreHandler;
		public TeamHolder _teamHolder;
	#endregion

	#region Public Parameter
		public enum GameState {
			Prepare,
			Start,
			End
		}
		public GameState _gameState;

		public hole[] Holes;
		public Transform teamHolder;
	#endregion

	public static TeamDroperManager instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<TeamDroperManager>();
			}
			return _instance;
		}
	}

	private void Start() {
		_gameState = GameState.Prepare;
		_ballAllocator = transform.Find("game_flow/main").GetComponent<BallAllocator>();
		_scoreHandler = transform.Find("game_flow/main").GetComponent<ScoreHandler>();
		_teamHolder = transform.Find("game_flow/main").GetComponent<TeamHolder>();

		//Set team Hard code
		for(int i = 0; i < Holes.Length; i++) {
			Holes[i].team = new TeamHolder.Team(Holes[i].name.Substring(Holes[i].name.Length-1, 1),
				 new GameObject[] {
				// teamHolder.GetChild((i*2)).gameObject, 
				// teamHolder.GetChild( (i*2) + 1).gameObject
			}, Holes[i].teamColor );


		}

		GameStart();
	}

	private void GameStart() {
		_ballAllocator.SetUp(this);
		_scoreHandler.SetUp(Time.time, 120);

		for(int i = 0; i < Holes.Length; i++) {
			_scoreHandler.SetScoreToUI(Holes[i].team, 0, false);
		}
		_gameState = GameState.Start;
	}

	public void GameOver(string p_end_game_message) {
		_gameState = GameState.End;
		Debug.Log("Game Over");
	}

	public void AddScore(TeamHolder.Team p_team, int p_score) {
		if (_gameState != GameState.Start) return;
		_ballAllocator.SetTargetGoalBall();
		_scoreHandler.SetScoreToUI(p_team, p_score, true);
	}

	private void Update() {
		if (Input.GetKey(KeyCode.Space)) 
			AddScore(Holes[0].team, 1);
	}

}
