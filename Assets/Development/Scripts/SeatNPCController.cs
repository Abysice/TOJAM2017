
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

		private Vector3 seat;

		// Use this for initialization
		void Start () {
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
					
				if (timeToTalk <= 0 && !talking) {
						talking = true;
						talk();
					}

				if (timeToLive <= 0) {
						leaveLibrary ();
						leaving = true;
					}
					
				if (sitting) {
						timeToTalk -= Time.deltaTime;
						timeToLive -= Time.deltaTime;
					}
			}

		public void walkToSeat() {
				if (!hasSeat) {
						seat = Managers.GetInstance ().GetSeatManager ().TakeSeat ();
						hasSeat = true;
					}

				float step = speed * Time.deltaTime;
				transform.position = Vector2.MoveTowards (transform.position, seat, step);

				if (transform.position == seat) {
						sitting = true;
					}
			}

		public void leaveLibrary(){
				Managers.GetInstance ().GetSeatManager ().ReleaseSeat (seat);
				float step = speed * Time.deltaTime;
				transform.position = Vector2.MoveTowards (transform.position, new Vector2(0,-5), step);
				if (transform.position.y <= -5f) {
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
					}
			}

		public bool isDestroy() {
				return destroy;
			}

		private void talk() {
				gameObject.GetComponent<Renderer> ().material.color = Color.red;
			}
	}