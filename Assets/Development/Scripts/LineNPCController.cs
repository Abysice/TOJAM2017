using UnityEngine;
using System.Collections;

public class LineNPCController : MonoBehaviour {
	private bool bookReceived;
	private bool bookRequested;
	private float speed;
	private int happiness;
	private float timer;
	private bool angry;
	private bool destroy;
	private Enums.BookTypes desiredBook;

	// Use this for initialization
	void Start () {
		transform.position = new Vector2(0, -10);
		bookReceived = false;
		bookRequested = false;
		angry = false;
		speed = 3f;
		happiness = Random.Range (4, 8);
		timer = 10f;
		desiredBook = (Enums.BookTypes)Random.Range (0, 9);
		requestBook ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!angry) {
			walkToDesk ();
		}

		if (bookReceived) {
			leaveLibrary();
		}

		if (bookRequested && Input.GetKey(KeyCode.Return)) {
			bookReceived = true;
		}

		if (timer <= 0) {
			happiness--;
			timer = 10f;
		}

		if (happiness <= 0) {
			leaveLibrary();
			angry = true;
		}
		//Debug.Log (happiness);
		timer -= Time.deltaTime;
	}

	public void walkToDesk() {
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.up, .5f);
	
		if (pos.y <= 1 && !hit.collider) {
			pos.y += speed * Time.deltaTime;
			transform.position = Vector3.Lerp (transform.position, pos, 0.5f);
		}
	}

	public void requestBook() {
		Debug.Log ("I want a " + desiredBook + " book");
		bookRequested = true;

	}

	public void leaveLibrary() {
		Vector2 pos = transform.position;
		if (pos.x < 1) {
			pos.x += speed * Time.deltaTime;
			transform.position = Vector3.Lerp (transform.position, pos, 0.5f);
		} else if(pos.y >= -5) {
			pos.y -= speed * Time.deltaTime;
			transform.position = Vector3.Lerp (transform.position, pos, 0.5f);
		}
		if (pos.y <= -5) {
			Managers.GetInstance ().GetNPCManager ().RemoveNPC ();
			Managers.GetInstance ().GetNPCManager ().SubtractLine ();
			destroy = true;
		}
	}

	public int GetHappiness() {
		return happiness;
	}

	public bool isAngry() {
		return angry;
	}

	public bool isDestroy() {
		return destroy;
	}

	public Enums.BookTypes GetDesiredBook() {
		return desiredBook;
	}
}
	
