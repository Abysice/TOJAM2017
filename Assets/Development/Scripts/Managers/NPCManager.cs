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
	private int seatCount;
	private List<LineNPCController> m_linenpcs = new List<LineNPCController>();
	private List<SeatNPCController> m_seatnpcs = new List<SeatNPCController>();
	private int lineLength;


	// Use this for initialization
	void Start () {
		NPCCount = 0;
		startTime = 10;
		timer = 0;
		speedTimer = 10;
		lineCount = 0;
		seatCount = 0;
		lineLength = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Managers.GetInstance ().GetGameStateManager ().CurrentState != Enums.GameStateNames.GS_03_INPLAY) {
			return;
		}
		if (timer <= 0) {
			int npcType = Random.Range (0,10);
			if (npcType < 6 && lineCount < 7) {
				SpawnLineNPC ();
			} else if (seatCount < 12) {
				SpawnSeatNPC ();
			}
			timer = startTime;
		}
		timer -= Time.deltaTime;

		if (speedTimer <= 0 && startTime > 2) {
			startTime--;
			speedTimer = 20;
		}
		LineNPCController destroyNPC = null;
		foreach (LineNPCController npc in m_linenpcs) {
			if (npc.isDestroy()) {
				destroyNPC = npc;
				Destroy (npc.gameObject);
			}
		}
		if (destroyNPC) {
			m_linenpcs.Remove (destroyNPC);
		}

		SeatNPCController destroySeatNPC = null;
		foreach (SeatNPCController npc in m_seatnpcs) {
			if (npc.isDestroy()) {
				destroySeatNPC = npc;
				Destroy (npc.gameObject);
			}
		}
		if (destroySeatNPC) {
			m_seatnpcs.Remove (destroySeatNPC);
		}
	}

	public void SpawnLineNPC() {
		NPC = Managers.GetInstance().GetGameProperties().LineNPCPrefab;
		GameObject npc = GameObject.Instantiate (NPC);
		m_linenpcs.Add (npc.GetComponent<LineNPCController>());
		NPCCount++;
		lineCount++;
		npc.GetComponent<LineNPCController> ().linePosition = lineLength;
		lineLength++;
	}

	public void SpawnSeatNPC() {
		NPC = Managers.GetInstance().GetGameProperties().SeatNPCPrefab;
		GameObject npc = GameObject.Instantiate (NPC);
		m_seatnpcs.Add (npc.GetComponent<SeatNPCController>());
		NPCCount++;
		seatCount++;
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

	public LineNPCController GetProperCustomer(Enums.BookTypes book) {
		int leastHappy = 100;
		LineNPCController npcToGive = null;
		if (m_linenpcs.Count < 1) {
			return null;
		}
		foreach (LineNPCController npc in m_linenpcs) {
			if (npc.GetDesiredBook() == book && npc.GetHappiness() < leastHappy) {
				leastHappy = npc.GetHappiness();
				npcToGive = npc;
			}
		}
		return npcToGive;
	}

	public List<SeatNPCController> GetSeatNPCList() {
		return m_seatnpcs;
	}

	public void LeaveLine() {
		foreach (LineNPCController npc in m_linenpcs) {
			npc.linePosition--;
		}			
		lineLength--;
	}
}
