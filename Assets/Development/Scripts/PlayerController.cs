using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 5f;
	private Vector2 direction;
	public float lerpVal = .9f;

	// Use this for initialization
	void Start () {
		direction = Vector2.down;
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		Vector3 startPos = pos;
		Vector3 movementVector = Vector3.zero;
		if (Input.GetKey ("u")) {
			Managers.GetInstance ().GetNPCManager ().SpawnNPC ();
		}

		if (Input.GetKey ("up") || Input.GetKey("w")) {
			movementVector += Vector3.up;
		}
		if (Input.GetKey ("down") || Input.GetKey("s")) {
			movementVector += Vector3.down;
		}
		if (Input.GetKey ("right") || Input.GetKey("d")) {
			movementVector += Vector3.right;
		}
		if (Input.GetKey ("left") || Input.GetKey("a")) {
			movementVector += Vector3.left;
		}

		pos = pos + (movementVector.normalized * speed * Time.deltaTime);
		if (movementVector != Vector3.zero) {
			setDirection (movementVector);
		}
		RaycastHit2D hitTL = Physics2D.Raycast (transform.position + new Vector3(.5f,.5f,0f), movementVector, .24f);
		RaycastHit2D hitTR = Physics2D.Raycast (transform.position + new Vector3(.5f,-.5f,0f), movementVector, .24f);
		RaycastHit2D hitBL = Physics2D.Raycast (transform.position + new Vector3(-.5f,.5f,0f), movementVector, .24f);
		RaycastHit2D hitBR = Physics2D.Raycast (transform.position + new Vector3(-.5f,-.5f,0f), movementVector, .24f);
		if (hitTL.collider == null && hitTR.collider == null && hitBL.collider == null && hitBR.collider == null) {			
			transform.position = Vector3.Lerp (startPos, pos, lerpVal);
		} else {
			//Debug.Log (hit.collider.name);
		}

	}

	public Vector2 GetDirection () {
		return direction;
	}

	private void setDirection (Vector2 dir) {
		direction = dir;
	}
}
