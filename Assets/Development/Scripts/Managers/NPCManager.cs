using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour {
	private GameObject NPC;
	private int NPCCount;
	private float timer;
	private float startTime;
	private float speedTimer;
	private int lineCount;
	private List<NPCController> m_linenpcs = new List<NPCController>();

	// Use this for initialization
	void Start () {
		NPCCount = 0;
		startTime = 10;
		timer = 10;
		speedTimer = 20;
		lineCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0 && lineCount < 7) {
			SpawnLineNPC ();
			timer = startTime;
		}
		timer -= Time.deltaTime;

		if (speedTimer <= 0 && startTime > 3) {
			startTime--;
			speedTimer = 20;
		}
		NPCController destroyNPC = null;
		foreach (NPCController npc in m_linenpcs) {
			if (npc.isDestroy()) {
				destroyNPC = npc;
				Destroy (npc.gameObject);
			}
		}
		if (destroyNPC) {
			m_linenpcs.Remove (destroyNPC);
		}
	}

	public void SpawnLineNPC() {
		NPC = Managers.GetInstance().GetGameProperties().NPCPrefab;
		GameObject npc = GameObject.Instantiate (NPC);
		m_linenpcs.Add (npc.GetComponent<NPCController>());
		NPCCount++;
		lineCount++;
	}

	public void RemoveNPC() {
		NPCCount--;
	}

	public int GetNPCCount() {
		return NPCCount;
	}

	public int GetLineCount() {
		return lineCount;
	}

	public void SubtractLine() {
		lineCount--;
	}
}
