using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 5f;
	private Vector2 direction;
	public float lerpVal = .9f;
	private Vector3 m_movementVec;

	// Use this for initialization
	void Start () {
		direction = Vector2.down;
	}

	// Update is called once per frame
	void Update () {
		m_movementVec = Vector3.zero;


		if (Input.GetKey ("u")) {
			Managers.GetInstance ().GetNPCManager ().SpawnLineNPC ();
		}
		if (Input.GetKey ("up") || Input.GetKey("w")) {
			m_movementVec += Vector3.up;
		}
		if (Input.GetKey ("down") || Input.GetKey("s")) {
			m_movementVec += Vector3.down;
		}
		if (Input.GetKey ("right") || Input.GetKey("d")) {
			m_movementVec += Vector3.right;
		}
		if (Input.GetKey ("left") || Input.GetKey("a")) {
			m_movementVec += Vector3.left;
		}


	}

	void FixedUpdate() {

		Vector3 pos = transform.position;
		Vector3 startPos = pos;
	

		pos = pos + (m_movementVec.normalized * speed * Time.deltaTime);
		if (m_movementVec != Vector3.zero) {
			setDirection (m_movementVec);
		}
		gameObject.GetComponent<Rigidbody2D> ().MovePosition (pos);

	}

	public Vector2 GetDirection () {
		return direction;
	}

	private void setDirection (Vector2 dir) {
		direction = dir;
	}
}
