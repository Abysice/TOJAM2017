using UnityEngine;
using System.Collections;

public class ShittyButton : MonoBehaviour {

	public void RestartTheGame() {
		Managers.GetInstance ().GetGameStateManager ().ChangeGameState (Enums.GameStateNames.GS_01_MENU);
	}
}
