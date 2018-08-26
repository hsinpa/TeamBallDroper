using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand : MonoBehaviour {
    public Rigidbody rb;
	private void OnTriggerEnter(Collider other)
	{
        if(other.tag == "object"){
            rb = other.GetComponent<Rigidbody>();
        }
	}

	private void OnTriggerExit(Collider other)
	{
        rb = null;
	}
}
