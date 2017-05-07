using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAbilities : MonoBehaviour {


	private GameStateManager m_mgr;
	private PlayerController m_pcon;
	private Enums.BookTypes held_book;
	private bool m_droppable;
	private float shushDistance;

	private Dictionary<string, Enums.BookTypes> dict = new Dictionary<string, Enums.BookTypes> {
		{ "NonFiction", Enums.BookTypes.NonFiction },
		{ "Horror", Enums.BookTypes.Horror },
		{ "Fantasy", Enums.BookTypes.Fantasy },
		{ "SciFi", Enums.BookTypes.SciFi },
		{ "Romance" , Enums.BookTypes.Romance},
		{ "Childrens" , Enums.BookTypes.Childrens},
		{ "Mystery" , Enums.BookTypes.Mystery},
		{ "Classics" , Enums.BookTypes.Classics},
		{ "Art" , Enums.BookTypes.Art},
	};	

	// Use this for initialization
	void Start () {
		m_mgr = Managers.GetInstance ().GetGameStateManager();
		m_pcon = gameObject.GetComponent<PlayerController> ();
		held_book = Enums.BookTypes.Null;
		m_droppable = false;
		shushDistance = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_mgr.CurrentState != Enums.GameStateNames.GS_03_INPLAY) {
			return;
		}	



		if (Input.GetKeyDown (KeyCode.Space) && held_book == Enums.BookTypes.Null) {
			Debug.Log ("Activated ability");
			RaycastHit2D hit = Physics2D.Raycast (transform.position, m_pcon.GetDirection (), 1.0f);
			if (hit != null) {
				if (hit.collider != null && hit.collider.tag == "Shelf") {
					if (dict.ContainsKey (hit.collider.name)) {
						held_book = dict [hit.collider.name];
						Debug.Log ("I am holding a " + hit.collider.name + " book");
					} else {
						Debug.LogWarning ("BAD NAME ON SHELF");
					}
				}
			}
		} else if (Input.GetKeyDown (KeyCode.Space) && held_book != Enums.BookTypes.Null && m_droppable) {
			Managers.GetInstance ().GetNPCManager ().GetProperCustomer (held_book);
			//set book back to null
			held_book = Enums.BookTypes.Null;
			Debug.Log ("Need to trigger the dude to walk away");
		}

		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			Vector3 dir = new Vector3 (transform.position.x + shushDistance, transform.position.y + shushDistance, 0);
			RaycastHit hit;
			Debug.DrawLine (transform.position, dir, Color.red);
			if (Physics.Raycast (transform.position, dir, out hit) && hit.collider.tag == "SeatNPC") {
				Debug.Log ("It hit");
				hit.collider.GetComponent<SeatNPCController> ().Shush ();
			}
		}
	}



	public void DropBook(){
		m_droppable = true;
	}

	public void NoDropBook() {
		m_droppable = false;
	}
}
 