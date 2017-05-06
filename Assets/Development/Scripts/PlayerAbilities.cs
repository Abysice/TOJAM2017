using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {


	private GameStateManager m_mgr;
	private PlayerController m_pcon;
	// Use this for initialization
	void Start () {
		m_mgr = Managers.GetInstance ().GetGameStateManager();
		m_pcon = gameObject.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_mgr.CurrentState != Enums.GameStateNames.GS_03_INPLAY) {
			return;
		}	
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log ("Activated ability");
			RaycastHit2D hit = Physics2D.Raycast (transform.position, m_pcon.GetDirection (), 1.0f);
			if (hit != null) {
				if (hit.collider != null && hit.collider.tag == "Shelf") {
					Debug.Log ("hit me a shelf");
				}
			}
		}
	}
}
 