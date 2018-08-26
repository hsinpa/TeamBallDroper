using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAllocator : MonoBehaviour {
	public Ball_STP ballStp;
	private List<GameObject> ballstats = new List<GameObject>();
	#region Inspector parameter
		public Transform playgroundHolder;
		public Transform goalColorObject;
		public float gate_open_period = 4;
		public int maxGenerateBallPerPeriod = 10;
	#endregion
	private float _gatePeriodTime = 0;

	private TeamDroperManager teamDroper;
	public Color targetColor;

	public void SetUp(TeamDroperManager p_teamDroper) {
		teamDroper = p_teamDroper;
		
		if (playgroundHolder == null)
			playgroundHolder = this.transform;

		SetTargetGoalBall();
		Clear();
	}

	// Update is called once per frame
	void Update () {
		if (TeamDroperManager.instance._gameState != TeamDroperManager.GameState.Start) return;

		if (Time.time > _gatePeriodTime) {
			_gatePeriodTime = Time.time + gate_open_period;
			GenerateBallInGroup();
			//ReleaseBall(true);
		}

		RemoveNotNecessaryBall();
	}

	private void RemoveNotNecessaryBall() {
		int ballcount = ballstats.Count;
		for(int i = ballcount -1; i >= 0; i--) {
			if (ballstats[i] == null) {
				ballstats.RemoveAt(i);
				return;
			}
			if (ballstats[i].transform.position.y < playgroundHolder.transform.position.y - 45) {
				GameObject.Destroy( ballstats[i].gameObject ); 
				ballstats.RemoveAt(i);
			}
		}
	}

	public void SetTargetGoalBall() {
		targetColor = GetRandomColor(true);
		Material targetBallMaterial = goalColorObject.GetComponent<MeshRenderer>().material;
		targetBallMaterial.SetColor("_EmissionColor", targetColor);
	}

	private void GenerateBallInGroup() {
		if (ballStp == null) return;
		for (int i = 0; i < maxGenerateBallPerPeriod; i ++) {
			GenerateBallPerTime(i);
		}
	}

	private GameObject GenerateBallPerTime(int p_index) {
		Color blendColor = GetRandomColor(false);
		
		GameObject gObject = Instantiate(ballStp.generalPrefab);
		Material gMaterial = new Material(ballStp.generalMaterial);
		MeshRenderer reshRenderer = gObject.GetComponent<MeshRenderer>();

		//Assign Random position
		Vector3 boundSize = reshRenderer.bounds.size;
		float yPos = p_index * gObject.transform.localScale.y + (playgroundHolder.position.y *0.9f);
		float xzOffSet = boundSize.x * 1.1f;
		float xPos = Random.Range(playgroundHolder.transform.position.x + xzOffSet,  playgroundHolder.transform.position.x - xzOffSet ),
			zPos = Random.Range(playgroundHolder.transform.position.z + xzOffSet,  playgroundHolder.transform.position.z - xzOffSet );
		
		gObject.transform.SetParent(playgroundHolder);
		gObject.transform.position = new Vector3(xPos, yPos, zPos);
		// gObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		//End 
		gObject.GetComponent<Rigidbody>().AddForce(Vector3.down);
		gMaterial.SetColor("_EmissionColor", blendColor);
		gObject.GetComponent<BallStat>().color = blendColor;
		reshRenderer.material = gMaterial;

		ballstats.Add(gObject);

		return gObject;
	}

	private Color GetRandomColor(bool blendColor) {
		Color randomColorA = ballStp.baseColors[Random.Range(0, ballStp.baseColors.Length)],
				randomColorB = ballStp.baseColors[Random.Range(0, ballStp.baseColors.Length)];
		return (blendColor) ? BlendColor(randomColorA, randomColorB) : randomColorA;
	}

	private Color BlendColor(Color a, Color b) {
		return (a + b) / 2;
	}

	private void Clear() {
		for(int i = 0; i < playgroundHolder.childCount; i++) {
			GameObject.Destroy( playgroundHolder.GetChild(i).gameObject );
		}
	}
}
