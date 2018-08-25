using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hole : MonoBehaviour {
    public int fraction;
    public TeamHolder.Team team;
    GameMaster gameMaster;
    public Color newcolor;
    public float R;
    public float G;
    public float B;
    public MeshRenderer meshRenderer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        newcolor = new Color(R, G, B, 1);
        meshRenderer.material.SetColor("_Color",newcolor);
	}

	private void OnTriggerEnter(Collider other)
	{
        Color color = other.GetComponent<MeshRenderer>().material.GetColor("_Color");
        if(R>0){
            R -= color.r;
        }else{
            R += color.r;
        }

        if(G>0){
            G -= color.g;
        }
        else
        {
            G += color.g;
        }

        if(B>0){
            B -= color.b;
        }
        else
        {
            B += color.b;
        }
	}
}
