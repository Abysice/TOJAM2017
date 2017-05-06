using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour {

	private static Managers m_instance;

	private GameStateManager m_gamestatemanager;
	private LibraryManager m_libmanager;

	//Accessors
	public static Managers GetInstance() {
		return m_instance;
	}

	public GameStateManager GetGameStateManager() {
		return m_gamestatemanager;
	}

	public LibraryManager GetLibraryManager() {
		return m_libmanager;
	}

	//Public Variables
	public void Awake()	{
		m_instance = this;
	}

	public void Start() {
		//Create managers here
		m_gamestatemanager = gameObject.AddComponent<GameStateManager>();
		m_libmanager = gameObject.AddComponent<LibraryManager> ();

		//preferably call init after
		m_gamestatemanager.Init();
	}

}
