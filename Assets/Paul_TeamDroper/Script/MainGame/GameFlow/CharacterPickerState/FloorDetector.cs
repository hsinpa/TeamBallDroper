using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour {
	public hole _hole;
	private Material material;

	void Awake() {
		material = new Material(GetComponent<MeshRenderer>().material);
		GetComponent<MeshRenderer>().material= material;
	}

	public void RenewFloorColor() {
		if (TeamDroperManager.instance._gameState == TeamDroperManager.GameState.PickTeam) {
			material.SetColor("_EmissionColor", _hole.teamColor);
		} else {
			material.SetColor("_EmissionColor", Color.black);
		}
	}

	public void OnCollisionEnter(Collision p_collider) {
		if (p_collider.gameObject.layer == GeneralFlag.charactertLayer && _hole != null &&
			TeamDroperManager.instance._gameState == TeamDroperManager.GameState.PickTeam) {
			//Assign team and color
			PlayerStats playerStats = p_collider.gameObject.GetComponent<PlayerStats>();
			if (playerStats != null)
				playerStats.SetTeam(_hole);

		} 
	}
	
}
