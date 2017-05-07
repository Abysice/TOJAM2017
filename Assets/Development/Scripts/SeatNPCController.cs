
using UnityEngine;
using System.Collections;

public class SeatNPCController : MonoBehaviour {

	private float speed;
	private float timeToLive;

	private float timeToTalk;

	private bool destroy;
	private bool sitting;
	private bool leaving;
	private bool talking;
	private bool hasSeat;
	private GameObject talkBubble;

	private Vector3 seat;

	// Use this for initialization
	void Start () {
		transform.position = new Vector2 (0, -10);
		speed = 3f;
		timeToTalk = Random.Range (4, 8);
		timeToLive = Random.Range (15, 25);
		sitting = false;
		leaving = false;
		talking = false;
		hasSeat = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!sitting && !leaving) {
			walkToSeat ();
		}

		if (timeToTalk <= 0 && !talking && !leaving) {
			talking = true;
			talk();
		}

		if (timeToLive <= 0) {
			Shush ();
			leaveLibrary ();
			leaving = true;
		}
					
		if (sitting) {
			timeToTalk -= Time.deltaTime;
			timeToLive -= Time.deltaTime;
		}

		if (talking) {
			if (Random.Range (0, 10) > 8) {
				Managers.GetInstance ().GetLibraryManager ().TickCurrency ();
			}
		}
	}

	public void walkToSeat() {
		if (!hasSeat) {
			seat = Managers.GetInstance ().GetSeatManager ().TakeSeat ();
			hasSeat = true;
		}

		float step = speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards (transform.position, seat, step);

		if (transform.position.x == seat.x && transform.position.y == seat.y) {
			sitting = true;
		}
	}

	public void leaveLibrary(){
		Managers.GetInstance ().GetSeatManager ().ReleaseSeat (seat);
		float step = speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards (transform.position, new Vector2(0,-10), step);
		if (transform.position.y <= -10f) {
				destroy = true;
			}
	}

	public bool isTalking() {
		return talking;
	}

	public void Shush() {
		if (talking) {
				talking = false;
				timeToTalk = Random.Range (4, 8);
				gameObject.GetComponent<Renderer> ().material.color = Color.grey;
				Destroy (talkBubble);
			}
	}

	public bool isDestroy() {
		return destroy;
	}

	private void talk() {
		gameObject.GetComponent<Renderer> ().material.color = Color.red;
		talkBubble = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().TalkBubble, transform.position + new Vector3(-1,0.7f,0), Quaternion.identity) as GameObject;
		talkBubble.transform.parent = transform;
	}
}