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
	public int linePosition;
	private bool leaving;
	private GameObject happyBubble;
	private Vector2 direction;

	// Use this for initialization
	void Start () {
		transform.position = new Vector2(0, -10);
		bookReceived = false;
		leaving = false;
		bookRequested = false;
		angry = false;
		speed = 3f;
		happiness = Random.Range (4, 10);
		timer = 10f;
		int maxBooks = Managers.GetInstance ().GetLibraryManager ().GetBookShelfCount();
		desiredBook = (Enums.BookTypes)Random.Range (1, maxBooks);
		requestBook ();

		spawnBookBubble(desiredBook);
		updateHappiness ("VeryHappy");
		direction = Vector2.up;
	}
	
	// Update is called once per frame
	void Update () {
		if (Managers.GetInstance ().GetGameStateManager ().CurrentState != Enums.GameStateNames.GS_03_INPLAY) {
			return;
		}
		if (!angry && !bookReceived) {
			walkToDesk ();
		}

		if (bookReceived) {
			leaveLibrary();
		}

		if (timer <= 0 && !bookReceived) {
			happiness--;
			timer = 10f;
			if (happiness < 2) {
				updateHappiness ("Angry");
			} else if (happiness < 4) {
				updateHappiness ("Sad");
			} else if (happiness < 6) {
				updateHappiness ("Neutral");
			} else if (happiness < 7) {
				updateHappiness ("Happy");
			}
		}

		if (happiness <= 0) {
			leaveLibrary();
			angry = true;
			if (!leaving) {
			Managers.GetInstance ().GetLibraryManager ().ReduceCurrency ();
			}
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
		leaving = true;
		Vector2 pos = transform.position;
		if (pos.x < 1) {
			pos.x += speed * Time.deltaTime;
			transform.position = Vector3.Lerp (transform.position, pos, 0.5f);
			direction = Vector2.right;
		} else if(pos.y >= -10) {
			pos.y -= speed * Time.deltaTime;
			transform.position = Vector3.Lerp (transform.position, pos, 0.5f);
			direction = Vector2.down;
		}
		if (pos.y <= -10) {
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

	public void GotBook() {
		bookReceived = true;
	}

	private void spawnBookBubble(Enums.BookTypes genre) {
		GameObject book = null;
		if (genre.ToString () == "Art") {
			book = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().ArtBubble) as GameObject;
		} else if (genre.ToString () == "Classics") {
			book = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().ClassicsBubble) as GameObject;
		} else if (genre.ToString () == "SciFi") {
			book = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().SciFiBubble) as GameObject;
		} else if (genre.ToString () == "Horror") {
			book = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().HorrorBubble) as GameObject;
		} else if (genre.ToString () == "Fantasy") {
			book = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().FantasyBubble) as GameObject;
		} else if (genre.ToString () == "Children") {
			book = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().ChildrenBubble) as GameObject;
		} else if (genre.ToString () == "NonFiction") {
			book = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().NonFictionBubble) as GameObject;
		} else if (genre.ToString () == "Romance") {
			book = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().RomanceBubble) as GameObject;
		} else if (genre.ToString () == "Tragedy") {
			book = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().TragedyBubble) as GameObject;
		} else if (genre.ToString () == "Mystery") {
			book = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().MysteryBubble) as GameObject;
		} else {
			Debug.Log ("Could not find genre " + genre);
		}

		book.transform.parent = transform;
	}

	private void updateHappiness(string happyLevel) {
		if (happyLevel == "VeryHappy") {
			happyBubble = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().VeryHappy, transform.position + new Vector3(0.7f,0.7f,0), Quaternion.identity) as GameObject;
		} else if (happyLevel == "Happy") {
			Destroy (happyBubble);
			happyBubble = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().Happy, transform.position + new Vector3(0.7f,0.7f,0), Quaternion.identity) as GameObject;
		}else if (happyLevel == "Neutral") {
			Destroy (happyBubble);
			happyBubble = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().Neutral, transform.position + new Vector3(0.7f,0.7f,0), Quaternion.identity) as GameObject;
		}else if (happyLevel == "Sad") {
			Destroy (happyBubble);
			happyBubble = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().Sad, transform.position + new Vector3(0.7f,0.7f,0), Quaternion.identity) as GameObject;
		}else if (happyLevel == "Angry") {
			Destroy (happyBubble);
			happyBubble = GameObject.Instantiate (Managers.GetInstance ().GetGameProperties ().Angry, transform.position + new Vector3(0.7f,0.7f,0), Quaternion.identity) as GameObject;
		}
		happyBubble.transform.parent = transform;
	}

	public Vector2 GetDirection () {
		return direction;
	}
}
	
