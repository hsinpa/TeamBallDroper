using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        chack(teamA);
        chack(teamB);

    }


    void chack(hole p_hole){
        if (p_hole.newcolor == target)
        {
            // Debug.Log(p_hole.team._id);
            // p_hole.fraction += 1;
            Debug.Log("p_hole.team, 1");
            if (p_hole.team != null)
                teamDroperManager.AddScore( p_hole.team, 1 );

            // Changed();
            if(p_hole.fraction>=Maxfraction){
                vt.Invoke();
            }
        }
    }

	// private void Changed()
	// {
    //     target = new Color(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2),1);
	// }

}
