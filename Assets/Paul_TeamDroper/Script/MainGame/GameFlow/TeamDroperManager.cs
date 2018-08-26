using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CliffLeeCL;
public class TeamDroperManager : MonoBehaviour {
	static TeamDroperManager _instance;

	#region MainConnector
		public BallAllocator _ballAllocator;
		public ScoreHandler _scoreHandler;
		public TeamHolder _teamHolder;
		public CameraManager _camera;
	#endregion

	#region Public Parameter
		public enum GameState {
			Prepare,
			PickTeam,
			Start,
			End
		}
		public GameState _gameState;

		public hole[] Holes;
		public Transform teamHolderTransform;
		public CanvasGroup endgameUI;

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

		//Set Camera ZoomInPosition
		_camera.SetUp(100, 13);
		GameStart();
	}

	public void GameStart() {
		_gameState = GameState.PickTeam;
	
		_ballAllocator.SetUp(this);
		_scoreHandler.SetUp(Time.time, 5);
		_teamHolder.SetUp(teamHolderTransform);


		for(int i = 0; i < Holes.Length; i++) {
			_scoreHandler.SetScoreToUI(Holes[i].team, 0, false);
		}

		CanvasGroupSwitcher(endgameUI, false);
		_camera._zoomState = CameraManager.ZoomState.ZoomIn;
	}

	public void GameOver(string p_end_game_message) {
		_gameState = GameState.End;

		endgameUI.transform.Find("field").GetComponent<UnityEngine.UI.Text>().text = p_end_game_message;
		CanvasGroupSwitcher(endgameUI, true);
		AudioManager.Instance.PlaySound(AudioManager.AudioName.Laugh, 1);
	}

	public void AddScore(TeamHolder.Team p_team, int p_score) {
		if (_gameState != GameState.Start) return;
		_ballAllocator.SetTargetGoalBall();
		_scoreHandler.SetScoreToUI(p_team, p_score, true);

		AudioManager.Instance.PlaySound(AudioManager.AudioName.ColorCorrect, 1);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space))  {
			GameOver("HELLO WORLD");
			// _camera._zoomState = (_camera._zoomState == CameraManager.ZoomState.ZoomOut) ? CameraManager.ZoomState.ZoomIn : CameraManager.ZoomState.ZoomOut;
		}
	}

	private void CanvasGroupSwitcher(CanvasGroup p_canvasgroup, bool isEnable ) {
		p_canvasgroup.alpha = (isEnable) ? 1 : 0;
		p_canvasgroup.blocksRaycasts = isEnable;
		p_canvasgroup.interactable = isEnable;
	}

}
