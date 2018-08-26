using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CliffLeeCL;

public class PlayerMove : MonoBehaviour {
    public int num = 1;
    Rigidbody rb;
    bool take;
    public float moveSpeed=5;
    public Rigidbody target;

    public float throwSpeed = 100;

    public hand ahand;

    public UnityEvent throwSroud;
	// Use this for initialization
	void Start () {
        ahand = GetComponentInChildren<hand>();
        rb = GetComponent<Rigidbody>();
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
            //transform.position += new Vector3(x, 0, y) * Time.deltaTime*moveSpeed;
            rb.velocity = new Vector3(x, 0, y) * Time.deltaTime * moveSpeed;
            AudioManager.Instance.PlaySound(AudioManager.AudioName.Footstep, 0.2f);

        }



        if(Input.GetButtonDown("joystick"+num+"Fire")){
            if(take){
                target.GetComponent<Collider>().enabled = true;
                target.isKinematic = false;
                target.transform.parent = null;
                target.AddForce((transform.forward+transform.up)*throwSpeed);
                take = false;
                AudioManager.Instance.PlaySound(AudioManager.AudioName.DropItem, 0.9f);

            }else{
                if (target)
                {
                    take = true;
                    target.GetComponent<Collider>().enabled = false;
                    target.transform.position = ahand.transform.position; 
                    target.transform.parent = transform;
                    target.isKinematic = true;
                    AudioManager.Instance.PlaySound(AudioManager.AudioName.PickUp, 0.8f);
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
