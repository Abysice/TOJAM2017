using UnityEngine;
using System.Collections;

public class DeskTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("OMG STUFF HAPPENED");
		if (other.tag == "Player") {
			other.gameObject.GetComponent<PlayerAbilities> ().DropBook ();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<PlayerAbilities> ().NoDropBook ();
		}
	}
}
