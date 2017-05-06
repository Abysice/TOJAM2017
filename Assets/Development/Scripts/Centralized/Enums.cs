using UnityEngine;
using System.Collections;

public class Enums : MonoBehaviour {

	public enum GameStateNames
	{
		GS_00_NULL = -1,
		GS_01_MENU = 0,
		GS_02_LOADING = 1, 
		GS_03_INPLAY,
		GS_04_LEAVING

	};

	public enum BookTypes {
		Null = -1,
		NonFiction = 0,
		Horror = 1,
		Fantasy,
		SciFi,
		Romance,
		Childrens,
		Mystery,
		Classics,
		Art,
	};
}
