using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 10f;
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

		transform.position = Vector3.Lerp(startPos, pos, lerpVal);
		Debug.Log (direction);

	}

	public Vector2 GetDirection () {
		return direction;
	}

	private void setDirection (Vector2 dir) {
		direction = dir;
	}
}
