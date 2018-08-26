using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStat : MonoBehaviour {
	public Color color {
		get {
			return _color;
		}
		set {
			_color = value;
			MeshRenderer reshRenderer = GetComponent<MeshRenderer>();
			reshRenderer.material.SetColor("_Color", _color);
		}
	}

	private Color _color;

	
}
