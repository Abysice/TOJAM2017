using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	private GameObject Player;
	private GameObject SceneCamera;

	// Use this for initialization
	void Start () {
		Player = null;
		SceneCamera = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnPlayer() {
		if (Player != null) {
			Destroy (Player);
		}
		Player = Managers.GetInstance().GetGameProperties().PlayerPrefab;
		Player = GameObject.Instantiate (Player);
	}

	public void SpawnCamera() {
		if (SceneCamera != null) {
			Destroy (SceneCamera);
		}
		SceneCamera = Managers.GetInstance().GetGameProperties().CameraPrefab;
		GameObject.Instantiate (SceneCamera);
	}


	public GameObject GetPlayer() {
		return Player;
	}

	public GameObject GetCamera() {
		return SceneCamera;
	}
}
