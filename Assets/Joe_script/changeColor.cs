using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour {
    public Color color;
	private void OnTriggerEnter(Collider other)
	{
        if(other.tag == "PlayerLight"){
            other.GetComponent<MeshRenderer>().material.SetColor("_Color", color);;
        }
	}
}
