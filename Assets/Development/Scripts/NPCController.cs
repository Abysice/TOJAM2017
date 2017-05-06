using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {
	private bool bookReceived;
	private bool bookRequested;
	private bool inLine;
	private int placeInLine;

	// Use this for initialization
	void Start () {
		bookReceived = false;
		bookRequested = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!bookRequested) {
			walkToDesk ();
		}

		if (bookReceived) {
			leaveLibrary();
		}

		if (bookRequested && Input.GetKey ("space")) {
			bookReceived = true;
		}
	}

	public void walkToDesk() {
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.up, .5f);
	
		if (pos.y <= 1 && !hit.collider) {
			pos.y += 1f * Time.deltaTime;
			transform.position = Vector3.Lerp (transform.position, pos, 0.5f);
		} else {
			Debug.Log (hit.collider.name);
		}

		if (pos.y >= 1) {
			requestBook ();
		}
	}

	public void requestBook() {
		Debug.Log ("I want THAT book");
		bookRequested = true;

	}

	public void leaveLibrary() {
		Vector2 pos = transform.position;
		if (inLine) {
			inLine = false;
			Managers.GetInstance ().GetNPCManager ().DecreaseLine ();
		}
		if (pos.x < 1) {
			pos.x += 1f * Time.deltaTime;
			transform.position = Vector3.Lerp (transform.position, pos, 0.5f);
		} else if(pos.y >= -5) {
			pos.y -= 1f * Time.deltaTime;
			transform.position = Vector3.Lerp (transform.position, pos, 0.5f);
		}
		if (pos.y <= -5) {
			Managers.GetInstance ().GetNPCManager ().RemoveNPC ();
			Destroy (gameObject);	
		}
	}
		
}
