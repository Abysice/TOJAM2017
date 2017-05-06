using UnityEngine;
using System.Collections;

public class NPCManager : MonoBehaviour {
	private GameObject NPC;
	private int NPCCount;
	private int NPCInLine;

	// Use this for initialization
	void Start () {
		NPCCount = 0;
		NPCInLine = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnNPC() {
		NPC = Managers.GetInstance().GetGameProperties().NPCPrefab;
		GameObject.Instantiate (NPC);
		NPCCount++;
	}

	public void RemoveNPC() {
		NPCCount--;
	}

	public int GetNPCCount() {
		return NPCCount;
	}

	public void IncreaseLine() {
		NPCInLine++;
		Debug.Log ("line" + NPCInLine);
	}

	public void DecreaseLine() {
		NPCInLine--;
	}

	public int GetNPCLineCount() {
		return NPCInLine;
	}
}
