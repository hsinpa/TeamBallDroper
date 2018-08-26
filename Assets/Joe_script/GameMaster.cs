using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CliffLeeCL;

public class GameMaster : MonoBehaviour {
    public int Maxfraction = 15;
    public Color target {
        get {
            return teamDroperManager._ballAllocator.targetColor;
        }
    }
    public hole teamA;
    public hole teamB;

    private TeamDroperManager teamDroperManager;
    public UnityEvent vt;
	// Use this for initialization
	void Start () {
        teamDroperManager = GetComponent<TeamDroperManager>();
	}

    // Update is called once per frame
    void Update()
    {
    	if (TeamDroperManager.instance._gameState != TeamDroperManager.GameState.Start) return;
        Check(teamA);
        Check(teamB);

    }


    void Check(hole p_hole){

        if (p_hole.newcolor != null) {
            float dist = Mathf.Abs( (p_hole.newcolor.r - target.r) +
                         (p_hole.newcolor.g - target.g)+
                        (p_hole.newcolor.b - target.b) );

            if (dist < 0.1f)
            {
                // Debug.Log(target);

                // Debug.Log(p_hole.team._id);
                // p_hole.fraction += 1;
                if (p_hole.team != null) {
                    teamDroperManager.AddScore( p_hole.team, 1 );
                    p_hole.R = 0;
                    p_hole.G = 0;
                    p_hole.B = 0;
                }

                // Changed();
                if(p_hole.fraction>=Maxfraction){
                    vt.Invoke();
                }
            }
        }


    }

	// private void Changed()
	// {
    //     target = new Color(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2),1);
	// }

}
