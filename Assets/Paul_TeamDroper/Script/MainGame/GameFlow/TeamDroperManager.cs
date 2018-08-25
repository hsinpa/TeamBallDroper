using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamDroperManager : MonoBehaviour {
	static TeamDroperManager _instance;

	#region MainConnector
		public BallAllocator _ballAllocator;
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
			Holes[i].team = new TeamHolder.Team(Holes[i].name, new GameObject[] {
				teamHolder.GetChild((i*2)).gameObject, 
				teamHolder.GetChild( (i*2) + 1).gameObject
			} );
		}

		GameStart();
	}

	private void GameStart() {
		_ballAllocator.SetUp(this);

		_gameState = GameState.Start;
	}

	public void AddScore(TeamHolder.Team p_team, float p_score) {
		_ballAllocator.SetTargetGoalBall();
		// ScoreHandler(;)
	}

	private void Update() {
		if (Input.GetKey(KeyCode.Space)) 
			AddScore(null, 33);
	}

}
