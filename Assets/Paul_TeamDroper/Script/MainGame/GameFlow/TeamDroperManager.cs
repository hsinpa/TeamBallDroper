using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamDroperManager : MonoBehaviour {
	static TeamDroperManager _instance;

	#region MainConnector
		BallAllocator _ballAllocator;
		ScoreHandler _scoreHandler;
		TeamHolder _teamHolder;
	#endregion

	#region Public Parameter
		public enum GameState {
			Prepare,
			Start,
			End
		}
		public GameState _gameState;
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

		GameStart();
	}

	private void GameStart() {
		_ballAllocator.SetUp(this);

		_gameState = GameState.Start;
	}

	public void AddScore(TeamHolder.Team p_team, float p_score) {

		_ballAllocator.SetTargetGoalBall();

	}

	private void Update() {
		if (Input.GetKey(KeyCode.Space)) 
			AddScore(null, 33);
	}

}
