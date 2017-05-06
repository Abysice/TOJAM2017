using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour {

	private static Managers m_instance;

	private GameStateManager m_gamestatemanager;


	//Accessors
	public static Managers GetInstance()
	{

		return m_instance;
	}

	public GameStateManager GetGameStateManager(){
		return m_gamestatemanager;
	}


	//Public Variables
	public void Awake()
	{
		m_instance = this;

	}
	public void Start() {
		//Create managers here
		m_gamestatemanager = gameObject.AddComponent<GameStateManager>();

		//preferably call init after
		m_gamestatemanager.Init();
	}

}
