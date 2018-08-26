using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CliffLeeCL;
public class hole : MonoBehaviour {
    public int fraction;
    [SerializeField]
    public TeamHolder.Team team;
    GameMaster gameMaster;
    public Color newcolor;
    public Color teamColor;

    public float R;
    public float G;
    public float B;
    public MeshRenderer meshRenderer;

    public Transform particleTransform;


	// Update is called once per frame
	void Update () {
        newcolor = new Color(R, G, B, 1);
        meshRenderer.material.SetColor("_EmissionColor",newcolor);
	}

    public void EnableParticle(bool isEnable) {
        if (particleTransform !=null)
            particleTransform.gameObject.SetActive(isEnable);
    }

	private void OnTriggerEnter(Collider other)
	{

    	AudioManager.Instance.PlaySound(AudioManager.AudioName.Splash, 1);

        BallStat ballState = other.GetComponent<BallStat>();
        if (ballState == null ) return;
        Color color = ballState.color;

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
        GameObject.Destroy( other.gameObject );
	}
}
