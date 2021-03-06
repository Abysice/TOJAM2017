﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LibraryManager : MonoBehaviour {

	private GameObject m_level;

	private List<GameObject> m_bookshelves = new List<GameObject>();
	private List<Vector3> m_spawnPoints = new List<Vector3> ();

	private int m_currency;
	private GameStateManager m_mgr;

	private GameObject m_textui;
	private GUIText m_text;
	public GameObject GetLevelObject() {
		return m_level;
	}

	public int  GetCurrency() {
		return m_currency;
	}

	// Use this for initialization
	void Start () {
		m_currency = 200;
		m_mgr = Managers.GetInstance ().GetGameStateManager ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}

		if (m_mgr.CurrentState != Enums.GameStateNames.GS_03_INPLAY) {
			return;
		}

		//m_text.text = m_currency.ToString();

		if (m_currency <= 0) {
			//Game Over
			m_mgr.ChangeGameState(Enums.GameStateNames.GS_04_LEAVING);
		}
	}

	public void SpawnLevel(){
		//spawn the level
		Debug.Log("Spawnning  level");
		m_level = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().LevelPrefab);
	
		//get the book spawn points
		for(int i = 0; i < m_level.transform.GetChildCount(); i++)
		{
			Transform child = m_level.transform.GetChild(i);
			if (child.tag == "ShelfSpawn") {
				m_spawnPoints.Add (child.position);
			}
		}

		//spawn bookshelves
		int j= 0; //THIS IS BAD
		foreach (Vector3 pos in m_spawnPoints) {
			GameObject temp = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().BookShelves [j], pos, Quaternion.identity) as GameObject;
			temp.name = Managers.GetInstance ().GetGameProperties ().BookShelves [j].name;
			m_bookshelves.Add (temp);
			j++;

		}

		//spawn texts

		m_textui =  GameObject.Instantiate(Managers.GetInstance().GetGameProperties().TextCanvas);
	
		for(int i = 0; i < m_textui.transform.GetChildCount(); i++)
		{
			Transform child = m_textui.transform.GetChild(i);
			if (child.name == "Text") {
				m_text = child.GetComponent<GUIText> ();
			}
		}

		Debug.Log ("1 - " + m_level);
	}

	public GameObject GetLevelObjects() {
		Debug.Log (m_level);
		return m_level;
	}

	public void AddCurrency(int amount) {
		m_currency += amount;
		Debug.Log (m_currency);
	}

	public void ReduceCurrency() {
		m_currency -= 100;
		Debug.Log (m_currency);
	}

	public void TickCurrency() {
		m_currency -= 1;
		Debug.Log (m_currency);
	}
	public int GetBookShelfCount() {
		return m_bookshelves.Count;
	}
}
