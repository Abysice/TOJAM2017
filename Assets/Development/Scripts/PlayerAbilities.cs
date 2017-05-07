using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAbilities : MonoBehaviour {


	private GameStateManager m_mgr;
	private PlayerController m_pcon;
	public  Enums.BookTypes held_book;
	private bool m_droppable;
	public float shushDistance;
	private bool shushing;
	private float timer;



	private Dictionary<string, Enums.BookTypes> dict = new Dictionary<string, Enums.BookTypes> {
		{ "NonFiction", Enums.BookTypes.NonFiction },
		{ "Horror", Enums.BookTypes.Horror },
		{ "Fantasy", Enums.BookTypes.Fantasy },
		{ "SciFi", Enums.BookTypes.SciFi },
		{ "Romance" , Enums.BookTypes.Romance},
		{ "Children" , Enums.BookTypes.Children},
		{ "Mystery" , Enums.BookTypes.Mystery},
		{ "Classics" , Enums.BookTypes.Classics},
		{ "Art" , Enums.BookTypes.Art},
		{ "Tragedy", Enums.BookTypes.Tragedy},
	};	

	// Use this for initialization
	void Start () {
		m_mgr = Managers.GetInstance ().GetGameStateManager();
		m_pcon = gameObject.GetComponent<PlayerController> ();
		held_book = Enums.BookTypes.Null;
		m_droppable = false;
		shushDistance = 3;
		shushing = false;
		timer = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Managers.GetInstance ().GetGameStateManager().CurrentState != Enums.GameStateNames.GS_03_INPLAY) {
			return;
		}	
		if (shushing && timer <= 0) {
			for(int i = 0; i < transform.GetChildCount(); i++)
			{
				Transform child = transform.GetChild(i);
				if (child.name == "ChatPrefab") {
					child.gameObject.SetActive(false);
					shushing = false;
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Space) && held_book == Enums.BookTypes.Null) {
			Vector3 pos = transform.position;
			Vector2 offset = gameObject.GetComponent<BoxCollider2D> ().offset;
			Vector3 temp = new Vector3 (offset.x, offset.x);	
			pos = pos + temp;
			RaycastHit2D hit = Physics2D.Raycast (new Vector2(pos.x, pos.y), m_pcon.GetDirection (), 2.5f);
			if (hit != null) {
				if (hit.collider != null && hit.collider.tag == "Shelf") {
					if (dict.ContainsKey (hit.collider.name)) {
						held_book = dict [hit.collider.name];
					} else {
						Debug.LogWarning ("BAD NAME ON SHELF");
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Space) && held_book != Enums.BookTypes.Null && m_droppable) {
	
			LineNPCController cunt = Managers.GetInstance ().GetNPCManager ().GetProperCustomer (held_book);
			if (cunt) {
				cunt.leaveLibrary ();
				int bookValue = (int)held_book * 20;
				Managers.GetInstance ().GetLibraryManager ().AddCurrency (bookValue);
				cunt.GotBook ();
			}

			//set book back to null
			held_book = Enums.BookTypes.Null;
			Debug.Log("BOOK RESET SHIT");

		}

		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {	
			for(int i = 0; i < transform.GetChildCount(); i++)
			{
				Transform child = transform.GetChild(i);
				if (child.name == "ChatPrefab") {
					child.gameObject.SetActive(true);
					shushing = true;
					timer = 1;
				}
			}
			foreach (SeatNPCController npc in Managers.GetInstance().GetNPCManager().GetSeatNPCList()) {
				float distance = Vector3.Distance (transform.position, npc.transform.position);
				if (distance < shushDistance) {					
					npc.GetComponent<SeatNPCController> ().Shush ();
				}
			}
		}
		if (shushing) {
			timer -= Time.deltaTime;
		}
			
	}
	public void DropBook(){
		m_droppable = true;
		Debug.Log ("can drop the book");
	}

	public void NoDropBook() {
		m_droppable = false;
	}


}
 