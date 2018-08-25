using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ｍove : MonoBehaviour {
    int num = 1;
    Rigidbody rb;
    bool take;
    public float moveSpeed=5;
    public Rigidbody target;

    public float throwSpeed = 100;

    public hand ahand;
	// Use this for initialization
	void Start () {
        ahand = GetComponentInChildren<hand>();
	}
	
	// Update is called once per frame
	void Update () {
        if (take)
        { 
        }else{
            target = ahand.rb;
        }


        float x = Input.GetAxis("joystick" + num + "moveX")*-1;
        float y = Input.GetAxis("joystick" + num + "moveY")*-1;
        if (Math.Abs(x) > 0 || Math.Abs(y) > 0)
        {
            
            transform.rotation = Quaternion.LookRotation(new Vector3(x, 0, y));
            transform.position += new Vector3(x, 0, y) * Time.deltaTime*moveSpeed;
        }



        if(Input.GetButtonDown("joystick"+num+"Fire")){
            if(take){
                target.isKinematic = false;
                target.transform.parent = null;
                target.AddForce(transform.forward*throwSpeed);
                take = false;
            }else{
                if (target)
                {
                    take = true;
                    target.transform.parent = transform;
                    target.isKinematic = true;
                }
            }
        }
        /*if(x>0){
            
        }else if(x<0){
            
        }

        if(y>0){
            
        }else if(y<0){
            
        }*/


	}
}
