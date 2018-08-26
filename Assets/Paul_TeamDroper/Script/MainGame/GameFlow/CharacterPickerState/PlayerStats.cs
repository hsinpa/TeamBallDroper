using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
	public MeshRenderer HatMesh;


	public void SetTeam(hole p_teamHole) {
		if (HatMesh != null) {
			Material HatMaterial = new Material(HatMesh.material);
			HatMaterial.SetColor("_EmissionColor", p_teamHole.teamColor);
			HatMesh.material = HatMaterial;
		}
	}
	
}
