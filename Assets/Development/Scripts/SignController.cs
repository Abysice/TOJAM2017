using UnityEngine;
using System.Collections;

public class SignController : MonoBehaviour {

	private bool m_up;
	private float timer = 1.0f;
	private Vector2 direction;
	// Update is called once per frame
	void Update () {
		GameObject player = Managers.GetInstance ().GetPlayerManager ().GetPlayer ();
		if (player) {
			if (player.GetComponent<PlayerAbilities> ().held_book == Enums.BookTypes.Null) {
				gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			} else {
				gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			}
		} else {
			Debug.Log ("well fuck");
		}


		if (timer < 0) {
			if (direction == Vector2.up) {
				direction = Vector2.down;
			} else {
				direction = Vector2.up;
			}

			timer = 1.0f;
		}

		Vector2 pos = transform.position;
		pos = pos + (direction * 0.3f * Time.deltaTime);
		transform.position = pos;
		timer -= Time.deltaTime;

	}
}
