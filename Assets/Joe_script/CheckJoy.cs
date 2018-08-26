using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckJoy : MonoBehaviour {
    public List<PlayerMove> players;
    public int p = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0+p; i < 6; i++ ){
            if (Input.GetButtonDown("joystick" + i + "Fire"))
            {
                players[0].num = i;
                players[0].gameObject.SetActive(true);
                p++;
                break;
            }

        }
	}
}
