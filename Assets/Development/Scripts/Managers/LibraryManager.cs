using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LibraryManager : MonoBehaviour {

	private GameObject m_level;

	private List<GameObject> m_bookshelves = new List<GameObject>();
	private List<Vector3> m_spawnPoints = new List<Vector3> ();

	private int Currency;
	private GameStateManager m_mgr;

	public GameObject GetLevelObject() {
		return m_level;
	}

	// Use this for initialization
	void Start () {
		Currency = 0;
		m_mgr = Managers.GetInstance ().GetGameStateManager ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (m_mgr.CurrentState != Enums.GameStateNames.GS_03_INPLAY) {
			return;
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
		Debug.Log ("1 - " + m_level);
	}

	public GameObject GetLevelObjects() {
		Debug.Log (m_level);
		return m_level;
	}


}
