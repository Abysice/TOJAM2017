using UnityEngine;
using System.Collections;

public class GameStateLeaving : GameStateBase {

	public GameStateLeaving(GameStateManager p_gameStateManager)
	{
		m_gameStateManager = p_gameStateManager;
	}


	public override void EnterState(Enums.GameStateNames p_prevState)
	{
		//spawn menu here
		Debug.Log("Cleaning up state");
	}

	public override void UpdateState()
	{
		//add some loading screen shenanigans before this
		m_gameStateManager.ChangeGameState(Enums.GameStateNames.GS_01_MENU);
	}

	public override void ExitState(Enums.GameStateNames p_nextState)
	{

	}
}
