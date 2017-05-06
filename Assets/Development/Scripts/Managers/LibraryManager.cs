using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LibraryManager : MonoBehaviour {

	private GameObject m_level;

	private List<GameObject> m_bookshelves = new List<GameObject>();
	private List<Vector3> m_spawnPoints = new List<Vector3> ();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void SpawnLevel(){
		//spawn the level
		m_level = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().LevelPrefab);
	
		//get the book spawn points
		for(int i = 0; i < m_level.transform.GetChildCount(); i++)
		{
			Transform child = m_level.transform.GetChild(i);
			if (child.name == "SPAWN") {
				m_spawnPoints.Add (child.position);
			}
		}

		//spawn bookshelves
		foreach (Vector3 pos in m_spawnPoints) {
			GameObject temp = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().BookShelves [0], pos, Quaternion.identity) as GameObject;
			temp.name = Managers.GetInstance ().GetGameProperties ().BookShelves [0].name;
			m_bookshelves.Add (temp);

		}
	
	}


}
