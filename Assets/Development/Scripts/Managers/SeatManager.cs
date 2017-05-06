using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeatManager : MonoBehaviour {
	private GameObject m_level;
	private List<Vector3> m_freeseats = new List<Vector3> ();

	// Use this for initialization
	void Start () {
		m_level = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().LevelPrefab);

		//get the seat spots
		for(int i = 0; i < m_level.transform.GetChildCount(); i++)
		{
			Transform child = m_level.transform.GetChild(i);
			if (child.tag == "Seat") {
				m_freeseats.Add (child.position);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	public Vector3 TakeSeat() {
		Vector3 seatPos = m_freeseats [0]; 
		m_freeseats.Remove (seatPos);
		return seatPos;
	}

	public void ReleaseSeat(Vector3 seat) {
		m_freeseats.Add (seat);
	}
}
