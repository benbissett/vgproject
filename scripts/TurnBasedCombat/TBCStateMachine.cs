using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBCStateMachine : MonoBehaviour {

    public enum BattleStates {
        START,
        PLAYERCHOICE,
        PLAYERANIMATE,
        ENEMYCHOICE,
        CALCDAMAGE,
        ENEMYANIMATE,
        LOSE,
        WIN
    }

    private BattleStates currentState;

	// Use this for initialization
	void Start () {
        currentState = BattleStates.START;
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case (BattleStates.START):
                break;
            case (BattleStates.PLAYERCHOICE):
                break;
            case (BattleStates.ENEMYCHOICE):
                break;
            case (BattleStates.CALCDAMAGE):
                break;
            case (BattleStates.LOSE):
                break;
            case (BattleStates.WIN):
                break;
        }
	}

    private void OnGUI()
    {
        if(GUILayout.Button("Next State"))
        {

        }
    }
}
