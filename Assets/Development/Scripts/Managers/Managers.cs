using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour {

	private static Managers m_instance;

	private GameStateManager m_gamestatemanager;
	private LibraryManager m_libmanager;
	private GameProperties m_properties;
	private PlayerManager m_playermanager;
	private NPCManager m_npcmanager;
	private SeatManager m_seatmanager;

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

	public GameProperties GetGameProperties(){
		return m_properties;
	}

	public PlayerManager GetPlayerManager() {
		return m_playermanager;
	}

	public NPCManager GetNPCManager() {
		return m_npcmanager;
	}

	public SeatManager GetSeatManager() {
		return m_seatmanager;
	}

	//Public Variables
	public void Awake()	{
		m_instance = this;
	}

	public void Start() {
		//Create managers here
		m_properties = gameObject.GetComponent<GameProperties>();
		m_gamestatemanager = gameObject.AddComponent<GameStateManager>();
		m_libmanager = gameObject.AddComponent<LibraryManager> ();
		m_playermanager = gameObject.AddComponent<PlayerManager> ();
		m_npcmanager = gameObject.AddComponent<NPCManager> ();
		m_seatmanager = gameObject.AddComponent<SeatManager> ();

		//preferably call init after
		m_gamestatemanager.Init();
	}

}
