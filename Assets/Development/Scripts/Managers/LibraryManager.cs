using UnityEngine;
using System.Collections;

public class LibraryManager : MonoBehaviour {

	private GameObject m_level;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}




	public void SpawnLevel(){
		m_level = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().LevelPrefab);

	}


}
